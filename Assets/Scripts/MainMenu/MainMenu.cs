using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject transiction_left, transiction_right, placa, maluca, panel, startTick, regionTick, optionTick, leaveTick;
    private bool canInteract = false;

    private void Start()
    {
        LeanTween.moveY(placa, -0.3f, 1f);
        LeanTween.moveY(maluca, 0.06f, 1.5f).setEaseInCubic();
    }
    public void startGame()
    {
        if (canInteract)
        {
            canInteract = false;
            startTick.SetActive(true);
            StartCoroutine(waitAndStart("Level1"));
        }
    }
    public void EnterMenu()
    {
        panel.SetActive(false);
        StartCoroutine(EnterMenuIE());
    }

    IEnumerator EnterMenuIE()
    {
        LeanTween.moveY(placa, 5.95f, 1f);
        LeanTween.moveY(maluca, -4.77f, 1.5f).setEaseOutCubic();
        LeanTween.moveX(transiction_left, -9.23f, 1.5f).setEaseInOutCubic();
        LeanTween.moveX(transiction_right, 9.23f, 1.5f).setEaseInOutCubic();
        yield return new WaitForSeconds(1.5f);
        canInteract = true;
    }

    IEnumerator waitAndStart(string level)
    {
        LeanTween.moveX(transiction_left, 1.43e-05f, 1.5f).setEaseInOutCubic();
        LeanTween.moveX(transiction_right, 1.43e-05f, 1.5f).setEaseInOutCubic();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void EnterRegion()
    {
        if (canInteract)
        {
            regionTick.SetActive(true);
            canInteract = false;
            StartCoroutine(waitAndStart("LevelSelect"));
        }
    }

    public void EnterOption()
    {
        if (canInteract)
        {
            canInteract = false;
            optionTick.SetActive(true);
        }
        
    }

    public void LeaveGame()
    {
        leaveTick.SetActive(true);
        Application.Quit();
    }
}
