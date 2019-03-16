using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LobbyManager : MonoBehaviour {
    public Transform centerBall, guestGrid, enemyBackground;
    public GameObject enemyActor;
    public Image waitingSprite, bgBall, enemyChoice;
    public Sprite[] sprites;

	void Start () {
        enemyBackground.localPosition = new Vector3(800f, -250f, 0f);

        waitingSprite.color = new Color(1f, 1f, 1f, 1f);

        DOTween.To(() => waitingSprite.color, x => waitingSprite.color = x, new Color(1f, 1f, 1f, 0f), 0.4f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);

        bgBall.transform.localScale = Vector3.one;

        bgBall.transform.DOScale(Vector3.one * 1.04f, 0.4f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);

        StartCoroutine(DelayActoin(2f, () =>
        {
            FindGuest();
        }));
    }

    void FindGuest()
    {
        if (guestGrid == null) return;

        Sequence mySequence = DOTween.Sequence().OnComplete(OnCompleteFindGuest);

        for (int i = 0; i < 8; i++)
        {
            Vector3 pos = guestGrid.localPosition - Vector3.right * 300f * i;
            mySequence.Append(guestGrid.DOLocalMove(pos, 0.4f).SetEase(Ease.OutQuad));
            if (i == 7) break;
            mySequence.Append(guestGrid.DOLocalMove(pos, 0.6f).SetEase(Ease.OutQuad).OnComplete(OnCompleteMove));
        }
        mySequence.Play();
    }

    int idx = 0;
    void OnCompleteMove()
    {
        idx++;
        enemyChoice.enabled = true;
        enemyChoice.sprite = sprites[idx % 2];
    }

    void OnCompleteFindGuest()
    {
        if (enemyBackground == null) return;
        waitingSprite.enabled = false;
        StartCoroutine(DelayActoin(1f, () =>
        {
            enemyBackground.DOLocalMove(new Vector3(0f, -250f, 0f), 0.4f).SetEase(Ease.OutQuad).OnComplete(OnCompleteSetActor);
        }));
        if (centerBall == null) return;
        StartCoroutine(DelayActoin(1.6f, () =>
        {
            centerBall.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutQuad);
        }));
    }

    void OnCompleteSetActor()
    {
        if (enemyActor == null) return;
        enemyActor.SetActive(true);
        StartCoroutine(DelayActoin(3f, () =>
        {
            SceneManager.LoadScene("Game");
        }));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        centerBall.RotateAround(centerBall.position, Vector3.forward, -100 * Time.deltaTime);
    }

    public IEnumerator DelayActoin(float dtime, System.Action callback)
    {
        yield return new WaitForSeconds(dtime);
        callback();
    }
}
