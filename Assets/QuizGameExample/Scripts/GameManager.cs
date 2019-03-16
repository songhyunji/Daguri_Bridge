using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour {
    public TextAsset quizDataAsset;
    public bool isDemo = true;

    public GameObject goodEffect, badEffect, soulEffect, happyEffect, hitEffect;
    public Transform msgObject;
    public HpManager friendHpMan, enemyHpMan;
    public ProgressBar timerSlider;
    public int timeTotal = 10;
    bool isTimeStop = false;

    public Dictionary<string, AudioClip> audioClips;
    public Dictionary<string, AudioSource> audioSources;

    Vector3 friendPos, enemyPos, friendHpPos, enemyHpPos, shieldPos;
    Transform friendHpGroup, enemyHpGroup, shieldGroup;

    Transform questionTf;
    Transform[] answerTfs;
    Vector3[] answerPosList;
    Text questionLabel, questionNoLabel, timeLabel;
    Text[] answerLabels;

    public Image readySprite;

    Image[] answerMarkers;

    Image[] msgSprites;

    List<QuizData> quizList;
    int quizTotal;
    int quizIndex = 0;

    [HideInInspector]
    public int quizLength = 0;

    GameObject ballPrefab;
    Transform pitcher;
    public Vector3 pitcherPos;
    Animator pitcherAnimator;
    Transform batter;
    public Vector3 batterPos;
    Animator batterAnimator;
    public GameObject ball, ball2;

    bool quizOn = true;

    public Transform fxCam;
    public Transform cam;
    Quaternion camRotation;
    Vector3 camPosition;

    Vector3 batPosition;
    Vector3 startPos;

    bool isCorrect = false;

    int scoreA = 0, scoreB = 0;

    void Start()
    {
        if (cam)
        {
            camRotation = cam.rotation;
            camPosition = cam.position;
            cam.transform.LookAt(cam.transform.position - cam.transform.up + cam.transform.forward);
        }
        ballPrefab = Resources.Load("Prefabs/Baseball", typeof(GameObject)) as GameObject;
        pitcher = GameObject.Find("Pitcher").transform;
        pitcherAnimator = pitcher.GetComponent<Animator>();
        batter = GameObject.Find("Batter").transform;
        batterAnimator = batter.GetComponent<Animator>();
        startPos = pitcher.transform.position + Vector3.up * 1.2f - Vector3.right * 0.5f;

        InitSounds();
        InitGame();
        HideGame();
        StartCoroutine(DelayActoin(3f, () =>
        {
            StartGame();
        }));

    }

    void InitSounds()
    {
        audioClips = new Dictionary<string,AudioClip>();
        audioSources = new Dictionary<string,AudioSource>();
        Object[] clips = Resources.LoadAll<AudioClip>("Sounds");
        foreach (AudioClip clip in clips)
        {
            audioClips.Add(clip.name, clip);
            audioSources.Add(clip.name, GetComponent<AudioSource>());
        }
    }

    void PlaySoundCrowd()
    {
        AudioSource.PlayClipAtPoint(audioClips["crowd1"], fxCam.position);
    }

    void PlaySoundSwingGood()
    {
        AudioSource.PlayClipAtPoint(audioClips["swing4"], fxCam.position);
    }
    void PlaySoundSwingBad()
    {
        AudioSource.PlayClipAtPoint(audioClips["swing1"], fxCam.position);
    }

    void HideGame()
    {
        ClearQuiz();
        Vector3 pos = friendPos;
        pos = enemyPos;
        pos = friendHpPos;
        pos = enemyHpPos;
        shieldGroup.localScale = new Vector3(3f, 3f, 1f);
        pos = shieldPos;
        shieldGroup.localPosition = new Vector3(pos.x, 0f, pos.z);

        pitcher.localPosition = Vector3.right * 100f;
        batter.localPosition = Vector3.right * 100f;

        foreach (Image sprite in msgSprites)
            sprite.color = new Color(1f, 1f, 1f, 0f);
    }

    void DrawMessage(int type)
    {
        type = Mathf.Clamp(type, 0, 4);
        msgObject.localScale = Vector3.one;
        Image sprite = msgSprites[type];

        DOTween.To(() => sprite.color, x => sprite.color = x, Color.white, 0.3f);
        DOTween.To(() => sprite.color, x => sprite.color = x, new Color(1f, 1f, 1f, 0f), 0.3f).SetDelay(1f);
        msgObject.DOScale(new Vector3(2f, 2f, 1f), 0.3f);
        msgObject.DOScale(new Vector3(1.6f, 1.6f, 1f), 0.2f).SetDelay(0.3f);
    }

    public void StartGame()
    {
        IntroGame();
        DrawQuiz();
        PlaySoundCrowd();
    }

    void DrawQuiz()
    {
        HideQuiz();
        StartCoroutine(DelayActoin(1f, () =>
        {
            SetQuiz();
            ShowQuiz();
        }));
    }

    void InitGame()
    {
        pitcherPos = pitcher.localPosition;
        batterPos = batter.localPosition;
        friendHpMan.InitHp();
        enemyHpMan.InitHp();
        questionTf = GameObject.Find("Question").transform;
        questionLabel = questionTf.GetComponentInChildren<Text>();
        questionNoLabel = questionTf.GetComponentsInChildren<Text>()[1];
        timeLabel = GameObject.Find("LabelTime").GetComponent<Text>();
        answerLabels = new Text[4];
        answerMarkers = new Image[4];
        answerTfs = new Transform[4];
        answerPosList = new Vector3[4];
        int i = 0;
        foreach (Transform tf in GameObject.Find("Answers").transform)
        {
            answerTfs[i] = tf;
            answerPosList[i] = tf.localPosition;
            answerLabels[i] = tf.GetComponentInChildren<Text>();
            answerMarkers[i] = tf.Find("AnswerMaker").GetComponent<Image>();
            i++;
        }
        QuizInit();

        msgSprites = new Image[5];
        i = 0;
        foreach (Transform tf in msgObject)
            msgSprites[i++] = tf.GetComponentInChildren<Image>();

        shieldGroup = GameObject.Find("ShieldGroup").transform;
        shieldPos = shieldGroup.localPosition;
        friendPos = batterAnimator.transform.localPosition;
        enemyPos = pitcherAnimator.transform.localPosition;
        readySprite.color = new Color(1f, 1f, 1f, 0f);
        DOTween.To(() => readySprite.color, x => readySprite.color = x, Color.white, 0.4f).SetDelay(0.4f);
    }

    void IntroGame()
    {
        Vector3 pos = friendPos;

        pos = shieldPos;
        shieldGroup.localPosition = new Vector3(pos.x, 0f, pos.z);
        shieldGroup.DOLocalMove(pos, 1f).SetDelay(1f);

        shieldGroup.localScale = new Vector3(3f, 3f, 1f);
        shieldGroup.DOScale(new Vector3(2f, 2f, 1f), 1f);
       
        DOTween.To(() => readySprite.color, x => readySprite.color = x, new Color(1f, 1f, 1f, 0f), 1.5f).SetDelay(0.5f);

        pitcher.localPosition = pitcherPos;
        batter.localPosition = batterPos;
    }

    void OnFriendStop()
    {
        batterAnimator.CrossFade("Idle", 0.2f);
    }
    void OnEnemyStop()
    {
        pitcherAnimator.CrossFade("Idle", 0.2f);
    }

    void ClearQuiz()
    {
        questionTf.localScale = new Vector3(0f, 1f, 1f);
        int t = -1;
        for (int i = 0; i < 4; i++)
        {
            Transform tf = answerTfs[i];
            if (i == quizList[quizIndex].correct)
                tf.localPosition = new Vector3(0f, -1400f, tf.localPosition.z);
            else
                tf.localPosition = new Vector3(1200f * t, tf.localPosition.y, tf.localPosition.z);
            t *= -1;
        }
    }

    void HideQuiz()
    {
        questionTf.DOScale(new Vector3(0f, 1f, 1f), 0.5f);

        int t = -1;
        for (int i = 0; i < 4; i++)
        {
            Transform tf = answerTfs[i];
            if (i == quizList[quizIndex].correct)
                tf.DOLocalMove(new Vector3(0f, -1400f, tf.localPosition.z), 0.5f);
            else
                tf.DOLocalMove(new Vector3(1200f * t, tf.localPosition.y, tf.localPosition.z), 0.5f);
            t *= -1;
        }
        foreach (Image sp in answerMarkers) sp.enabled = false;
    }

    void HideQuiz2()
    {
        questionTf.DOScale(new Vector3(0f, 1f, 1f), 0.5f);

        int i = -1;
        int j = 0;
        foreach (Transform tf in answerTfs)
        {
            if (j != quizList[quizIndex].correct)
            {
                tf.DOLocalMove(new Vector3(1200f * i, tf.localPosition.y, tf.localPosition.z), 0.5f);
            }
            else
            {
                tf.DOLocalMove(new Vector3(tf.localPosition.x, -760f, tf.localPosition.z), 0.5f);
            }
            i *= -1;
            j++;
        }
        foreach (Image sp in answerMarkers) sp.enabled = false;
    }

    void TypeQuiz()
    {
        questionLabel.text = quizList[quizIndex].question.Substring(0, quizLength);
    }

    void ShowQuiz()
    {
        questionTf.DOScale(new Vector3(1f, 1f, 1f), 0.5f);

        int i = 1;
        foreach (Transform tf in answerTfs)
        {
            tf.localPosition = answerPosList[i - 1] + 1200f * ((i % 2) * 2 - 1) * Vector3.left;
            tf.DOLocalMove(new Vector3(0f, tf.localPosition.y, tf.localPosition.z), 0.5f).SetDelay(0.3f * i++);
        }
        quizOn = true;

        quizLength = 0;

        DOTween.To(() => this.quizLength, x => this.quizLength = x, quizList[quizIndex].question.Length, 1f).SetEase(Ease.Linear).OnUpdate(TypeQuiz);

    }

    string QuizMakeString(string str) 
    {
        return str;
    }

    public IEnumerator DelayTimer()
    {
        yield return new WaitForSeconds(1f);
        timeCount = Mathf.Clamp(timeCount - 1, 0, timeTotal);
        timeLabel.text = timeCount.ToString();
        if (!isTimeStop && timeCount>0) StartCoroutine("DelayTimer");
        if (!isTimeStop && timeCount < 1) ClickAnswer(-1);
    }


    void SetQuiz()
    {
        quizIndex = Random.Range(0, quizTotal) % quizTotal;
        timeCount = timeTotal;
        timerSlider.StartMotion();
        isTimeStop = false;
        timeLabel.text = timeCount.ToString();
        StopCoroutine("DelayTimer");
        StartCoroutine("DelayTimer");
        QuizData item = quizList[quizIndex];
        answerLabels[0].text = QuizMakeString(item.answer1);
        answerLabels[1].text = QuizMakeString(item.answer2);
        answerLabels[2].text = QuizMakeString(item.answer3);
        answerLabels[3].text = QuizMakeString(item.answer4);
#if UNITY_EDITOR
        Debug.Log("Answer : " + (item.correct+1));
#endif
        questionLabel.text = item.question;
        questionNoLabel.text = "Q." + (questionNo+1).ToString("00");
        questionNo++;
    }

    int timeCount = 0;
    int questionNo = 0;

    void QuizInit()
    {
        questionNo = 0;
        quizList = new List<QuizData>();
        string[] stringList = quizDataAsset.text.Trim().Split('\n');
        foreach (string str in stringList)
        {
            string s = str.Trim();
            string[] sList = s.Split('\t');
            QuizData qdata = new QuizData();
            qdata.id = int.Parse(sList[0].Trim());
            qdata.question = sList[1].Trim();
            qdata.answer1 = sList[2].Trim();
            qdata.answer2 = sList[3].Trim();
            qdata.answer3 = sList[4].Trim();
            qdata.answer4 = sList[5].Trim();
            qdata.correct = int.Parse(sList[6].Trim()) - 1;
            Debug.Log(qdata.id + ":" + qdata.correct);
            quizList.Add(qdata);
        }
        quizTotal = quizList.Count;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        OnUpdateEffect();
	}

    void OnDonePitch()
    {
        Destroy(ball);
        if (isCorrect) OnBat();
    }
    int camAniSeq = 0;
    void OnPitch()
    {
        Debug.Log("isCorrect : " + isCorrect);
        batPosition = new Vector3(0f, Random.Range(-0.1f, 0.1f)-0.4f, 0f);
        Vector3 pos = batPosition;
        if (!isCorrect) pos = batPosition - Vector3.forward * 4f + Vector3.up * 0.1f;
        Vector3 midPos = (startPos - batPosition)/2f + Vector3.up * 0.1f;
        ball = Instantiate(ballPrefab, startPos, Quaternion.identity) as GameObject;
        Vector3[] path = new Vector3[] { startPos, midPos, pos };
        ball.transform.localPosition = startPos;

        ball.transform.DOLocalPath(path, 1f, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(OnDonePitch);

        if (cam && isCorrect)
        {
            switch (camAniSeq){
                case 2:
                    cam.transform.DOLocalMove(Vector3.right * 8, 0.5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuad);
                    break;
                case 1:
                    cam.transform.DOLocalMove(Vector3.up * 8 + Vector3.right * 8, 0.5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuad);
                    break;
                case 3:
                    cam.transform.DOLocalMove(Vector3.up * 8 + Vector3.right * 8, 0.5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuad);
                    break;
                default:
                    cam.transform.DOLocalMove(Vector3.right * 8, 0.5f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuad);
                    break;
            }
            camAniSeq = (camAniSeq + 1) % 4;
        }
    }

    void OnDoneEffect()
    {
        Destroy(ball2);
    }

    void OnUpdateEffect()
    {
        if (!cam) return;
        if (ball)
        {
            Vector3 targetPoint = ball.transform.position - cam.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint);
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, targetRotation, Time.deltaTime * 20.0f);
        }
        else if (ball2)
        {
            Vector3 targetPoint = ball2.transform.position - cam.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint);
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, targetRotation, Time.deltaTime * 2.0f);
        }
        else
        {
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, camRotation, Time.deltaTime * 5f);
        }
    }

    void OnBat()
    {
        Vector3 pos = batPosition;
        Debug.Log("batPosition : " + batPosition);
        Instantiate(hitEffect, pos, Quaternion.identity);
        ball2 = Instantiate(ballPrefab, pos, Quaternion.identity) as GameObject;
        ball2.transform.position = pos;
        Vector3[] path = new Vector3[] { pos, pos + Vector3.forward * 50f + Vector3.up * 20f, pos + Vector3.forward * 90f + Vector3.up * 25f, pos + Vector3.forward * 110f + Vector3.up * 20f, pos + Vector3.forward * 115f };
        Quaternion r = Quaternion.Euler(0f, Random.Range(-30f, 30f), 0f);
        for (int i = 0; i < path.Length; i++)
        {
            Vector3 p = path[i];
            path[i] = r * p;
        }

        ball2.transform.DOLocalPath(path, 4f, PathType.Linear).SetEase(Ease.Linear).OnComplete(OnDoneEffect);
    }

    int msgIdx = 1;

    void GoodAction()
    {
        scoreA++;
        isCorrect = true;
        Instantiate(goodEffect);
        GameObject go = Instantiate(soulEffect) as GameObject;
        go.GetComponent<SoulEffect>().posX = -1f;
        StartCoroutine(DelayActoin(1.5f, () =>
        {
            PlaySoundSwingGood();
        }));
        StartCoroutine(DelayActoin(2f, () =>
        {
            friendHpMan.DoSaveHp(33);
        }));
        DrawMessage(msgIdx++);
        if (msgIdx > 4) msgIdx = 1;
    }

    void BadAction()
    {
        scoreB++;
        Instantiate(badEffect);
        GameObject go = Instantiate(soulEffect) as GameObject;
        go.GetComponent<SoulEffect>().posX = 1f;
        StartCoroutine(DelayActoin(1f, () =>
        {
            PlaySoundSwingBad();
        }));
        StartCoroutine(DelayActoin(2f, () =>
        {
            enemyHpMan.DoSaveHp(33);
        }));
        DrawMessage(0);
    }

    void DrawAction()
    {
        if (scoreA < 4 && scoreB < 4)
        {
            StartCoroutine(DelayActoin(3f, () =>
            {
                DrawQuiz();
            }));
        }
        else
        {
            if (scoreA >= 4)
            {
                StartCoroutine(DelayActoin(5f, () =>
                {
                    SceneManager.LoadScene("ResultWin");
                }));
            }
            else
            {
                StartCoroutine(DelayActoin(4f, () =>
                {
                    SceneManager.LoadScene("ResultLost");
                }));
            }
        }
    }

    void ClickAnswer(int no)
    {
        if (!quizOn) return;
        quizOn = false;
        QuizData item = quizList[quizIndex];

        pitcherAnimator.CrossFade("Pitch", 0.2f);

        StartCoroutine(DelayActoin(1.2f, () =>
        {
            batterAnimator.CrossFade("Bat", 0.2f);
        }));

        timerSlider.StopMotion();
        isTimeStop = true;

        HideQuiz2();
        isCorrect = false;
        if (item.correct == no) 
            GoodAction();
        else
            BadAction();
        DrawAction();
    }

    public void OnClickAnswer1()
    {
        ClickAnswer(0);
        answerMarkers[0].enabled = true;
    }
    public void OnClickAnswer2()
    {
        ClickAnswer(1);
        answerMarkers[1].enabled = true;
    }
    public void OnClickAnswer3()
    {
        ClickAnswer(2);
        answerMarkers[2].enabled = true;
    }
    public void OnClickAnswer4()
    {
        ClickAnswer(3);
        answerMarkers[3].enabled = true;
    }

    public IEnumerator DelayActoin(float dtime, System.Action callback)
    {
        yield return new WaitForSeconds(dtime);
        callback();
    }
}
