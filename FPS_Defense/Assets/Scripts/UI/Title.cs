using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
    public string sceneName = "GameStage";

    public static Title instance;

    private SaveNLoad theSaveNLoad;

    [SerializeField]
    private GameObject go_Canvas;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(this.gameObject);
    }

    public void ClickStart()
    {
        Debug.Log("Loading..");
        SceneManager.LoadScene(sceneName);

        Destroy(go_Canvas);
    }

    public void ClickLoad()
    {
        Debug.Log("Load");

        StartCoroutine(LoadCoroutine());

        Destroy(go_Canvas);
    }

    IEnumerator LoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (operation.isDone)
        {
            yield return null;
        }

        theSaveNLoad = FindObjectOfType<SaveNLoad>();
        theSaveNLoad.LoadData();

        Destroy(gameObject);
    }

    public void ClickExit()
    {
        Debug.Log("???? ????");
        Application.Quit();
    }
}
