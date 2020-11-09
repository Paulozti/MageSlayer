using Cinemachine;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public delegate void BoardLoaded();
    public static event BoardLoaded onBoardLoaded;
    public GameObject transiction_right, transiction_left, pause;
    public GameObject canvas;
    public SpriteRenderer interfaceShow;
    public CinemachineVirtualCamera cineCamera;
    public static bool canPlay = false;
    public int actionPoints = 24;
    public static int world = 0, level = 0;
    public bool orbDestroyed = false;
    public bool didPlayerWin = false;
    public bool gotCollectable = false;
    public DialogueManager dialogueManager;
    private bool waitingForReset = false;
    private Text actionPointsText, worldText, levelText;
    private GameObject pauseDarkening, pauseBanner, collectable, nextLevelButton;
    private bool canInteract = false, continueButtonAvailable = false, gameEnded = false;
    public static bool bossLevel = false;
    public bool gameStarted = false;


    [SerializeField] private GameObject blockPrefab;
    //array of arrays 10x10
    public GameObject[][] pos = new GameObject[10][] {new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], }; 
    int posX = 0;
    int posY = 0;
    void Start()
    {
        pauseDarkening = GameObject.Find("PauseDark");
        pauseBanner = GameObject.Find("Pause");
        collectable = GameObject.Find("EndCollectable");
        collectable.SetActive(false);
        nextLevelButton = GameObject.Find("NextLevelButton");
        nextLevelButton.SetActive(false);
        pauseDarkening.SetActive(false);
        pauseBanner.SetActive(false);
        GameObject map = new GameObject();
        map.name = "Map";
        for(int i = 0; i < 10; i++)
        {
            posY = 0;
            for(int f = 0; f < 10; f++)
            {
                GameObject obj = Instantiate(blockPrefab);
                obj.transform.position += new Vector3(posX, posY, 0);
                pos[i][f] = obj;
                posY--;
                obj.transform.SetParent(map.transform);
            }
            posX++;
        }
        onBoardLoaded?.Invoke();
        PlayerMovement.onPlayerMove += UpdateGame;
        PlayerAttack.onPlayerAttack += UpdateGame;
        dialogueManager.level = level;
        dialogueManager.StartDialog();
    }

    private void Update()
    {
        if (!dialogueManager.dialogueFinished)
            return;
        if (waitingForReset)
        {
            if (Input.GetButtonDown("Attack"))
            {
                Unpause();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(canPlay && canInteract)
                PauseGame();
            else if(!canPlay)
                Unpause();
        }
    }

    private void UpdateGame()
    {
        if (bossLevel)
        {
            if (didPlayerWin)
            {

            }
            else
            {
                return;
            }
        }
        actionPoints--;
        actionPointsText.text = actionPoints.ToString();
        if (didPlayerWin)
        {
            Win();
        }
        else if(actionPoints <= 0)
        {
            LoseGame();
        }
    }

    public void Win()
    {
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        canPlay = false;
        LeanTween.moveX(transiction_left, 4.020003f, 1f);
        LeanTween.moveX(transiction_right, 4.020003f, 1f);
        StartCoroutine(ZoonOut());
        yield return new WaitForSeconds(1f);
        pauseBanner.SetActive(true);
        pause.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Victory");
        LeanTween.moveY(pause, -0.2f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        collectable.SetActive(true);
        if (gotCollectable)
        {
            LeanTween.alpha(collectable, 1f, 0.5f);
            LeanTween.rotate(collectable, new Vector3(0, 0, 0), 0.5f);
        }
        else
        {
            LeanTween.rotate(collectable, new Vector3(0, 0, 0), 0f);
            collectable.GetComponent<SpriteRenderer>().color = Color.black;
            LeanTween.alpha(collectable, 1f, 0.5f);
        }
        gameEnded = true;
        canInteract = true;
        nextLevelButton.SetActive(true);
    }

    public void StartGame()
    {
        gameStarted = true;
        StartCoroutine(ZoonIn());
        LeanTween.moveX(transiction_left, -12.03f, 1f);
        LeanTween.moveX(transiction_right, 20.30f, 1f);
        StartCoroutine(ShowCanvas());
    }

    private void OnDestroy()
    {
        PlayerMovement.onPlayerMove -= UpdateGame;
        PlayerAttack.onPlayerAttack -= UpdateGame;
    }

    IEnumerator ShowCanvas()
    {
        yield return new WaitForSeconds(0.5f);
        canvas.SetActive(true);
        actionPointsText = GameObject.Find("ActionPointsText").GetComponent<Text>();
        worldText = GameObject.Find("WorldText").GetComponent<Text>();
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        if (bossLevel)
        {
            actionPointsText.text = "";
        }
        else
        {
            actionPointsText.text = actionPoints.ToString();
        }
        worldText.text = world.ToString();
        levelText.text = level.ToString();
        yield return new WaitForSeconds(1.5f);
        canPlay = true;
        canInteract = true;
    }

    IEnumerator ZoonIn()
    {
        yield return new WaitForSeconds(1f);
        while (cineCamera.m_Lens.OrthographicSize >= 7.65f)
        {
            yield return new WaitForSeconds(0.01f);
            cineCamera.m_Lens.OrthographicSize = cineCamera.m_Lens.OrthographicSize - 0.05f;
        }
        actionPointsText.color = new Color(actionPointsText.color.r, actionPointsText.color.g, actionPointsText.color.b, 1f);
        worldText.color = new Color(worldText.color.r, worldText.color.g, worldText.color.b, 1f);
        levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, 1f);
    }

    IEnumerator ZoonOut()
    {
        actionPointsText.color = new Color(actionPointsText.color.r, actionPointsText.color.g, actionPointsText.color.b, 0f);
        worldText.color = new Color(worldText.color.r, worldText.color.g, worldText.color.b, 0f);
        levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, 0f);
        while (cineCamera.m_Lens.OrthographicSize <= 8.8f)
        {
            yield return new WaitForSeconds(0.01f);
            cineCamera.m_Lens.OrthographicSize = cineCamera.m_Lens.OrthographicSize + 0.05f;
        }
    }

    public void PauseGame()
    {
        if (canInteract)
        {
            if (!canPlay)
            {
                Time.timeScale = 1f;
                LeanTween.moveY(pause, 7.80f, 0.5f);
                LeanTween.alphaCanvas(pauseDarkening.GetComponent<CanvasGroup>(), 0f, 0.5f);
                StartCoroutine(disableDarkPause());
                interfaceShow.sprite = Resources.Load<Sprite>("Interface");
                canInteract = true;
            }
            else
            {
                Time.timeScale = 0f;
                continueButtonAvailable = true;
                pause.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pause");
                canPlay = false;
                pauseBanner.SetActive(true);
                pauseDarkening.SetActive(true);
                LeanTween.alphaCanvas(pauseDarkening.GetComponent<CanvasGroup>(), 0.75f, 0.5f).setIgnoreTimeScale(true);
                LeanTween.moveY(pause, -0.2f, 0.5f).setIgnoreTimeScale(true);
                interfaceShow.sprite = Resources.Load<Sprite>("Interface_pause");
            }
        }
    }

    public void HomeButton()
    {
        if (canInteract)
        {
            canInteract = false;
            canPlay = false;
            StartCoroutine(GoBackToMenu());
        }
    }

    private IEnumerator GoBackToMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(ZoonOut());
        LeanTween.moveY(pause, 7.80f, 0.5f);
        LeanTween.alphaCanvas(pauseDarkening.GetComponent<CanvasGroup>(), 0f, 0.5f);
        LeanTween.moveX(transiction_left, 4.020003f, 1f).setIgnoreTimeScale(true);
        LeanTween.moveX(transiction_right, 4.020003f, 1f).setIgnoreTimeScale(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void NextLevel()
    {
        if (canInteract)
        {
            if (gameEnded)
            {
                canInteract = false;
                StartCoroutine(LoadNextLevel());
                
            }
        }
    }

    private IEnumerator LoadNextLevel()
    {
        LeanTween.alpha(collectable, 0f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        LeanTween.moveY(pause, 7.80f, 0.5f);
        yield return new WaitForSeconds(0.6f);
        string levelToLoad = "Level" + (level + 1);
        SaveFile();
        if(levelToLoad == "Level8")
        {
            SceneManager.LoadScene("Win", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
        }

    }

    private void SaveFile()
    {
        SaveData save = new SaveData();
        try
        {
            save = save.LoadData();
        }
        catch
        {
            save.SaveDataToFile(save);
        }

        switch (level)
        {
            case 1:
                save.level1 = true;
                if (gotCollectable)
                    save.collectable1 = true;
                break;
            case 2:
                save.level2 = true;
                if (gotCollectable)
                    save.collectable2 = true;
                break;
            case 3:
                save.level3 = true;
                if (gotCollectable)
                    save.collectable3 = true;
                break;
            case 4:
                save.level4 = true;
                if (gotCollectable)
                    save.collectable4 = true;
                break;
            case 5:
                save.level5 = true;
                if (gotCollectable)
                    save.collectable5 = true;
                break;
            case 6:
                save.level6 = true;
                if (gotCollectable)
                    save.collectable6 = true;
                break;
            case 7:
                save.level7 = true;
                if (gotCollectable)
                    save.collectable7 = true;
                break;
        }

        save.SaveDataToFile(save);
    }
    
    private IEnumerator disableDarkPause()
    {
        yield return new WaitForSeconds(0.5f);
        pauseDarkening.SetActive(false);
        pauseBanner.SetActive(false);
        if (waitingForReset)
        {
            waitingForReset = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
        else
        {
            canPlay = true;
        }
    }

    public void ResetInterface()
    {
        if (canInteract)
        {
            canInteract = false;
            interfaceShow.sprite = Resources.Load<Sprite>("Interface_reset");
            StartCoroutine(ResetGame());
        }
    }

    public void LoseGame()
    {
        waitingForReset = true;
        continueButtonAvailable = true;
        StartCoroutine(ResetGame());
    }

    private IEnumerator ResetGame()
    {
        canPlay = false;
        LeanTween.moveX(transiction_left, 4.020003f, 1f);
        LeanTween.moveX(transiction_right, 4.020003f, 1f);
        StartCoroutine(ZoonOut());
        yield return new WaitForSeconds(1f);
        interfaceShow.sprite = Resources.Load<Sprite>("Interface");
        if (waitingForReset)
        {
            pause.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Retry");
            pauseBanner.SetActive(true);
            LeanTween.alphaCanvas(pauseDarkening.GetComponent<CanvasGroup>(), 0.75f, 0.5f);
            LeanTween.moveY(pause, -0.2f, 0.5f);
            canInteract = true;
        }
        else
        {
            canInteract = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
        
    }

    public void Unpause()
    {
        if (continueButtonAvailable)
        {
            continueButtonAvailable = false;
            canInteract = true;
            PauseGame();
        }
    }

    public void ExitGame()
    {
        if (continueButtonAvailable)
        {
            Debug.LogWarning("Exiting");
            Application.Quit();
        }
            
    }
}
