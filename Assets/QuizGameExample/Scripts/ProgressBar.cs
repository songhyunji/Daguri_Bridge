using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ProgressBar : MonoBehaviour {
    public Image foreGround;

	void Start () {

	}

    public void StartMotion()
    {
        foreGround.fillAmount = 1f;

        DOTween.To(() => foreGround.fillAmount, x => foreGround.fillAmount = x, 0f, 10f).SetEase(Ease.Linear).SetId("TimeProgressBar");
    }

    public void StopMotion()
    {
        DOTween.Kill("TimeProgressBar");
    }

	void Update () {
	    
	}
}
