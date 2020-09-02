
using UnityEngine;

public class Level1 : MonoBehaviour
{
    [SerializeField] private Sprite wall, floor, win, rock, rock3;
    private LoadLevel LV;
    private void Awake()
    {
        LoadLevel.onBoardLoaded += LoadPuzzle;
        LV = GameObject.Find("LevelManager").GetComponent<LoadLevel>();
    }

    void LoadPuzzle()
    {
        #region line 1
        LV.pos[0][0].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[0][0].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[1][0].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[1][0].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[2][0].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[2][0].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[3][0].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[3][0].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[4][0].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[4][0].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[5][0].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[5][0].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[6][0].GetComponent<SpriteRenderer>().sprite = win; LV.pos[6][0].GetComponent<BlockType>().type = BlockType.Type.Win;
        LV.pos[7][0].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[7][0].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[8][0].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[8][0].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[9][0].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[9][0].GetComponent<BlockType>().type = BlockType.Type.Wall;
        #endregion

        #region line 2
        LV.pos[0][1].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[0][1].GetComponent<BlockType>().type = BlockType.Type.Obstacle; 
        LV.pos[1][1].GetComponent<SpriteRenderer>().sprite = rock3; LV.pos[1][1].GetComponent<BlockType>().type = BlockType.Type.Obstacle; LV.pos[1][1].GetComponent<BlockType>().life = 3;
        LV.pos[2][1].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[2][1].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[3][1].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[3][1].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[4][1].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[4][1].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[5][1].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[5][1].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[6][1].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[6][1].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[7][1].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[7][1].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[8][1].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[8][1].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[9][1].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[9][1].GetComponent<BlockType>().type = BlockType.Type.Floor;
        #endregion

        #region line 3
        LV.pos[0][2].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[0][2].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[1][2].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[1][2].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[2][2].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[2][2].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[3][2].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[3][2].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[4][2].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[4][2].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[5][2].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[5][2].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[6][2].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[6][2].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[7][2].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[7][2].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[8][2].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[8][2].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[9][2].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[9][2].GetComponent<BlockType>().type = BlockType.Type.Floor;
        #endregion

        #region line 4
        LV.pos[0][3].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[0][3].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[1][3].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[1][3].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[2][3].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[2][3].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[3][3].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[3][3].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[4][3].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[4][3].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[5][3].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[5][3].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[6][3].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[6][3].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[7][3].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[7][3].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[8][3].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[8][3].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[9][3].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[9][3].GetComponent<BlockType>().type = BlockType.Type.Wall;
        #endregion

        #region line 5
        LV.pos[0][4].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[0][4].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[1][4].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[1][4].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[2][4].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[2][4].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[3][4].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[3][4].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[4][4].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[4][4].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[5][4].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[5][4].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[6][4].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[6][4].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[7][4].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[7][4].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[8][4].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[8][4].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[9][4].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[9][4].GetComponent<BlockType>().type = BlockType.Type.Wall;
        #endregion

        #region line 6
        LV.pos[0][5].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[0][5].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[1][5].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[1][5].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[2][5].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[2][5].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[3][5].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[3][5].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[4][5].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[4][5].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[5][5].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[5][5].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[6][5].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[6][5].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[7][5].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[7][5].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[8][5].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[8][5].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[9][5].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[9][5].GetComponent<BlockType>().type = BlockType.Type.Wall;
        #endregion

        #region line 7
        LV.pos[0][6].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[0][6].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[1][6].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[1][6].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[2][6].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[2][6].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[3][6].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[3][6].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[4][6].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[4][6].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[5][6].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[5][6].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[6][6].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[6][6].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[7][6].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[7][6].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[8][6].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[8][6].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[9][6].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[9][6].GetComponent<BlockType>().type = BlockType.Type.Wall;
        #endregion

        #region line 8
        LV.pos[0][7].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[0][7].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[1][7].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[1][7].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[2][7].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[2][7].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[3][7].GetComponent<SpriteRenderer>().sprite = rock; LV.pos[3][7].GetComponent<BlockType>().type = BlockType.Type.Obstacle;
        LV.pos[4][7].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[4][7].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[5][7].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[5][7].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[6][7].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[6][7].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[7][7].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[7][7].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[8][7].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[8][7].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[9][7].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[9][7].GetComponent<BlockType>().type = BlockType.Type.Wall;
        #endregion

        #region line 9
        LV.pos[0][8].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[0][8].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[1][8].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[1][8].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[2][8].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[2][8].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[3][8].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[3][8].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[4][8].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[4][8].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[5][8].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[5][8].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[6][8].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[6][8].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[7][8].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[7][8].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[8][8].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[8][8].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[9][8].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[9][8].GetComponent<BlockType>().type = BlockType.Type.Wall;
        #endregion

        #region line 10
        LV.pos[0][9].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[0][9].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[1][9].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[1][9].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[2][9].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[2][9].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[3][9].GetComponent<SpriteRenderer>().sprite = floor; LV.pos[3][9].GetComponent<BlockType>().type = BlockType.Type.Floor;
        LV.pos[4][9].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[4][9].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[5][9].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[5][9].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[6][9].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[6][9].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[7][9].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[7][9].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[8][9].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[8][9].GetComponent<BlockType>().type = BlockType.Type.Wall;
        LV.pos[9][9].GetComponent<SpriteRenderer>().sprite = wall; LV.pos[9][9].GetComponent<BlockType>().type = BlockType.Type.Wall;
        #endregion
    }

    private void OnDestroy()
    {
        LoadLevel.onBoardLoaded -= LoadPuzzle;
    }
}
