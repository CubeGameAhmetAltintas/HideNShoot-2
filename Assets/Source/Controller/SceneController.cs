using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class SceneController : ControllerBaseModel
{
    public static SceneController Controller;
    [SerializeField] IntEventModel onSceneLoad;
    [SerializeField] GameObject logoIntro;
    int activeScreenIndex;
    public WorkingState State;
    int loadingScene;

    public override void Initialize()
    {
        base.Initialize();
        DontDestroyOnLoad(this.gameObject);
        Controller = this;
        logoIntro.SetActive(true);
    }

    public void ChangeScene(int sceneIndex)
    {
        if (State != WorkingState.Working)
        {
            if (loadingScene == 0)
            {
                activeScreenIndex = 0;
            }

            loadingScene = sceneIndex;
        }
    }

    public void LoadMainScene()
    {
        ChangeScene(1);
    }

    public void LoadGameScene()
    {
        ChangeScene(2);
    }

    public void OnChangeScene()
    {
        StartCoroutine(LoadAsyncScene());
    }

    private void onLoadSceneComplete()
    {
        onSceneLoad.Invoke(loadingScene);
        State = WorkingState.Waiting;
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadingScene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }


        if (asyncLoad.isDone)
        {
            onLoadSceneComplete();
        }
    }
}
