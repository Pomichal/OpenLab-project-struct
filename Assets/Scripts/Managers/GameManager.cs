using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        App.gameManager = this;
    }

    void Start()
    {
        LoadScene("UIScene");

    }

    public void LoadScene(string sceneName)
    {
        // call the coroutine
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncOp.allowSceneActivation = false;
        while(!asyncOp.isDone)
        {
            if(asyncOp.progress >= 0.9f && !asyncOp.allowSceneActivation)
            {
                asyncOp.allowSceneActivation = true;
            }
            yield return null; // caka jeden frame
        }


        // todo: refactor
        App.screenManager.Show<MenuScreen>();

    }
}
