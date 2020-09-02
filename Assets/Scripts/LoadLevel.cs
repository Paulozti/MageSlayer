using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public delegate void BoardLoaded();
    public static event BoardLoaded onBoardLoaded;

    public int actionPoints = 24;

    public bool didPlayerWin = false;
    private Text actionPointsText;
    [SerializeField] private GameObject blockPrefab;
    //array of arrays 10x10
    public GameObject[][] pos = new GameObject[10][] {new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], new GameObject[10], }; 
    int posX = 0;
    int posY = 0;
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            posY = 0;
            for(int f = 0; f < 10; f++)
            {
                GameObject obj = Instantiate(blockPrefab);
                obj.transform.position += new Vector3(posX, posY, 0);
                pos[i][f] = obj;
                posY--;
            }
            posX++;
        }
        onBoardLoaded?.Invoke();
        actionPointsText = GameObject.Find("ActionPointsText").GetComponent<Text>();
        PlayerMovement.onPlayerMove += UpdateGame;
        PlayerAttack.onPlayerAttack += UpdateGame;
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
            SceneManager.LoadScene(1);
        }
    }

    private void OnDestroy()
    {
        PlayerMovement.onPlayerMove -= UpdateGame;
        PlayerAttack.onPlayerAttack -= UpdateGame;
    }
}
