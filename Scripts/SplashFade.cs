/*Developed and provided by SpeedTutor - www.speed-tutor.com*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashFade : MonoBehaviour {
    public string loadLevel;

    private AsyncOperation async;

    IEnumerator Start() {

        async = SceneManager.LoadSceneAsync(loadLevel);

        async.allowSceneActivation = false;

        yield return new WaitForSeconds(10f);

        async.allowSceneActivation = true;
        //SceneManager.LoadScene(loadLevel);

    }

    /*
    public void StartLoading() {
        StartCoroutine("load");
    }

    IEnumerator load() {
        Debug.LogWarning("ASYNC LOAD STARTED - " +
           "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
        async = Application.LoadLevelAsync(levelName);
        async.allowSceneActivation = false;
        yield return async;
    }

    public void ActivateScene() {
        async.allowSceneActivation = true;
    }
    */
}
