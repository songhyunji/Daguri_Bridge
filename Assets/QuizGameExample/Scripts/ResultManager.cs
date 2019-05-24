using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour {

	void Start () {
        StartCoroutine(DelayActoin(6f, () =>
        {
            SceneManager.LoadScene("ReadyGame");
        }));
 	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
    public IEnumerator DelayActoin(float dtime, System.Action callback)
    {
        yield return new WaitForSeconds(dtime);
        callback();
    }
}
