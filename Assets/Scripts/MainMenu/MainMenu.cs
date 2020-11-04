using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject transiction_left, transiction_right, placa, maluca, panel, startTick, regionTick, optionTick, leaveTick;

    private void Start()
    {
        LeanTween.moveY(placa, -0.3f, 1f);
        LeanTween.moveY(maluca, 0.06f, 2f).setEaseInCubic();
    }
    public void startGame()
    {
        startTick.SetActive(true);
        LeanTween.moveX(transiction_left, 1.43e-05f, 2.5f).setEaseInOutCubic();
        LeanTween.moveX(transiction_right, 1.43e-05f, 2.5f).setEaseInOutCubic();
        StartCoroutine(waitAndStart());
       
    }
    public void EnterMenu()
    {
        panel.SetActive(false);
        LeanTween.moveY(placa, 5.95f, 1f);
        LeanTween.moveY(maluca, -4.77f, 1.5f).setEaseOutCubic();
        LeanTween.moveX(transiction_left, -9.23f, 4f).setEaseInOutCubic();
        LeanTween.moveX(transiction_right, 9.23f, 4f).setEaseInOutCubic();
    }
    IEnumerator waitAndStart()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Level1");
    }

    public void EnterRegion()
    {
        regionTick.SetActive(true);
    }

    public void EnterOption()
    {
        optionTick.SetActive(true);
    }

    public void LeaveGame()
    {
        leaveTick.SetActive(true);
        Application.Quit();
    }
}
