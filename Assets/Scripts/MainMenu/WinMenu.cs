using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public CanvasGroup buttonMenu, buttonExit, msg;
    public GameObject transictionRight, transictionLeft;
    
    void Start()
    {
        StartCoroutine(StartWinMenu());
    }

    IEnumerator StartWinMenu()
    {
        LeanTween.moveX(transictionLeft, -12.03f, 1f);
        LeanTween.moveX(transictionRight, 20.30f, 1f);
        yield return new WaitForSeconds(1f);
        LeanTween.alphaCanvas(buttonMenu, 1f, 1f);
        LeanTween.alphaCanvas(buttonExit, 1f, 1f);
        LeanTween.alphaCanvas(msg, 1f, 1f);
    }

    public void Menu()
    {
        StartCoroutine(LoadNextScene(false));
    }

    public void Exiting()
    {
        StartCoroutine(LoadNextScene(true));
    }

    IEnumerator LoadNextScene(bool exiting)
    {
        LeanTween.alphaCanvas(buttonMenu, 0f, 0.5f);
        LeanTween.alphaCanvas(buttonExit, 0f, 0.5f);
        LeanTween.alphaCanvas(msg, 0f, 0.5f);
        LeanTween.moveX(transictionLeft, 4.020003f, 1f);
        LeanTween.moveX(transictionRight, 4.020003f, 1f);
        yield return new WaitForSeconds(1f);
        if (exiting)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
        
    }
}
