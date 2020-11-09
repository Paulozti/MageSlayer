using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour
{
    public GameObject col1, col2, col3, col4, col5, col6, col7;
    public GameObject img1, img2, img3, img4, img5, img6, img7;
    public GameObject transictionLeft, transictionRight;
    public GameObject levelSelectText, MenuText;
    private SaveData save = new SaveData();
    private bool canInteract = false;
    void Start()
    {
        StartCoroutine(OpenTransiction());
        try
        {
            save = save.LoadData();
        }
        catch
        {
            Debug.Log("Arquivo de save não existe.");
            save.SaveDataToFile(save);
        }
        CheckSave();
    }

    IEnumerator OpenTransiction()
    {
        LeanTween.moveX(transictionLeft, -12.03f, 1f);
        LeanTween.moveX(transictionRight, 20.30f, 1f);
        yield return new WaitForSeconds(1f);
        levelSelectText.SetActive(true);
        MenuText.SetActive(true);
        canInteract = true;
    }

    public void LoadLevel(string level)
    {
        if (canInteract)
        {
            if (CheckIfCompleted(level))
            {
                canInteract = false;
                StartCoroutine(LoadScene(level));
            }
        }
        
    }

    private bool CheckIfCompleted(string level)
    {
        bool rtrn = false;
        if(level == "Level1")
        {
            if (save.level1)
                rtrn = true;
        }
        else if (level == "Level2")
        {
            if (save.level2)
                rtrn = true;
        }
        else if (level == "Level3")
        {
            if (save.level3)
                rtrn = true;
        }
        else if (level == "Level4")
        {
            if (save.level4)
                rtrn = true;
        }
        else if (level == "Level5")
        {
            if (save.level5)
                rtrn = true;
        }
        else if (level == "Level6")
        {
            if (save.level6)
                rtrn = true;
        }
        else if (level == "Level7")
        {
            if (save.level7)
                rtrn = true;
        }
        return rtrn;
    }

    IEnumerator LoadScene(string level)
    {
        levelSelectText.SetActive(false);
        MenuText.SetActive(false);
        LeanTween.moveX(transictionLeft, 4.020003f, 1f);
        LeanTween.moveX(transictionRight, 4.020003f, 1f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void Home()
    {
        if (canInteract)
        {
            canInteract = false;
            StartCoroutine(LoadScene("Menu"));
        }
    }
    private void CheckSave()
    {

        if (save.level1)
        {
            img1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level1");
            if (save.collectable1)
            {
                col1.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (save.level2)
        {
            img2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level2");
            if (save.collectable2)
            {
                col2.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (save.level3)
        {
            img3.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level3");
            if (save.collectable3)
            {
                col3.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (save.level4)
        {
            img4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level4");
            if (save.collectable4)
            {
                col4.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (save.level5)
        {
            img5.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level5");
            if (save.collectable5)
            {
                col5.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (save.level6)
        {
            img6.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level6");
            if (save.collectable6)
            {
                col6.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (save.level7)
        {
            img7.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Level7");
            if (save.collectable7)
            {
                col7.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

}
