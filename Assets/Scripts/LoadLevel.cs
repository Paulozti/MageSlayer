using Cinemachine;
using System.Collections;
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
    public DialogueManager dialogueManager;
    private bool waitingForReset = false;
    private Text actionPointsText, worldText, levelText;
    private GameObject pauseDarkening, pauseBanner;
    private bool canInteract = false, continueButtonAvailable = false;
    
    [SerializeField] private GameObject blockPrefab;
    //array of arrays 10x10
    public GameObject[][] pos = new GameObject[10][] {new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], }; 
    int posX = 0;
    int posY = 0;
    void Start()
    {
        pauseDarkening = GameObject.Find("PauseDark");
        pauseBanner = GameObject.Find("Pause");
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
        actionPoints--;
        actionPointsText.text = actionPoints.ToString();
        if (didPlayerWin)
        {
            SceneManager.LoadScene(2);
        }
        else if(actionPoints <= 0)
        {
            waitingForReset = true;
            continueButtonAvailable = true;
            StartCoroutine(ResetGame());
        }
    }

    public void StartGame()
    {
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
        worldText.text = world.ToString();
        levelText.text = level.ToString();
        actionPointsText.text = actionPoints.ToString();
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
            canInteract = false;
            if (!canPlay)
            {
                LeanTween.moveY(pause, 7.80f, 0.5f);
                LeanTween.alphaCanvas(pauseDarkening.GetComponent<CanvasGroup>(), 0f, 0.5f);
                StartCoroutine(disableDarkPause());
                interfaceShow.sprite = Resources.Load<Sprite>("Interface");
                canInteract = true;
            }
            else
            {
                continueButtonAvailable = true;
                pause.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pause");
                canPlay = false;
                pauseBanner.SetActive(true);
                pauseDarkening.SetActive(true);
                LeanTween.alphaCanvas(pauseDarkening.GetComponent<CanvasGroup>(), 0.75f, 0.5f);
                LeanTween.moveY(pause, -0.2f, 0.5f);
                interfaceShow.sprite = Resources.Load<Sprite>("Interface_pause");
            }
        }
    }

    private IEnumerator disableDarkPause()
    {
        yield return new WaitForSeconds(0.5f);
        pauseDarkening.SetActive(false);
        pauseBanner.SetActive(false);
        if (waitingForReset)
        {
            waitingForReset = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
