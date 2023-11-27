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
        LoadScene("UIScene", false, new ShowScreenCommand<MenuScreen>());

    }

    public void LoadScene(string sceneName, bool setAsActive=false, ICommand sceneLoadedCommand = null)
    {
        // call the coroutine
        StartCoroutine(LoadSceneAsync(sceneName, setAsActive, sceneLoadedCommand));
    }

    public void UnloadScene(string sceneName, ICommand sceneUnloadedCommand=null)
    {
        StartCoroutine(UnloadSceneAsync(sceneName, sceneUnloadedCommand));
    }

    IEnumerator LoadSceneAsync(string sceneName, bool setAsActive=false, ICommand sceneLoadedCommand = null)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncOp.allowSceneActivation = false;
        while(!asyncOp.isDone)
        {
            if(asyncOp.progress >= 0.9f && !asyncOp.allowSceneActivation)
            {
                asyncOp.allowSceneActivation = true;
            }
            yield return null; // waits one frame
        }

        // scene is loaded
        if(setAsActive)
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        sceneLoadedCommand?.Execute();
    }

    IEnumerator UnloadSceneAsync(string sceneName, ICommand sceneUnloadedCommand=null)
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);
        while (!asyncUnload.isDone)
        {
            yield return null;
        }
        // scene unloaded

        sceneUnloadedCommand?.Execute();
    }
}
