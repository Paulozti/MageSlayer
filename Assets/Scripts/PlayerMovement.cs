using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private LoadLevel LV;
    private PlayerAttack Player;
    public int posX, posY;

    public delegate void PlayerMoved();
    public static event PlayerMoved onPlayerMove;

    void Start()
    {
        LV = GameObject.Find("LevelManager").GetComponent<LoadLevel>();
        Player = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        if ((Input.GetButtonDown("Vertical") || Input.GetButtonDown("Horizontal")) && LoadLevel.canPlay)
            Move();
    }

    private void Move()
    {
        //Verifica se está dentro da matriz
        if (Input.GetButtonDown("Horizontal"))
        {
            float axis = Input.GetAxisRaw("Horizontal");
            if(axis > 0)
            {
                Player.PlayerIsLooking = PlayerAttack.PlayerDirection.Right;
                if (posX == (LV.pos.Length-1))
                    return;
                else
                {
                    if (CheckMovement(posX + 1, posY))
                    {
                        posX++;
                        MoveToPosition();
                    }
                }
            }
            else
            {
                Player.PlayerIsLooking = PlayerAttack.PlayerDirection.Left;
                if (posX == 0)
                    return;
                else
                {
                    if (CheckMovement(posX - 1, posY))
                    {
                        posX--;
                        MoveToPosition();
                    }
                }
            }
        }
        else
        {
            float axis = Input.GetAxisRaw("Vertical");
            if (axis > 0)
            {
                Player.PlayerIsLooking = PlayerAttack.PlayerDirection.Up;
                if (posY == 0)
                    return;
                else
                {
                    if(CheckMovement(posX, posY - 1))
                    {
                        posY--;
                        MoveToPosition();
                    }
                }
            }
            else
            {
                Player.PlayerIsLooking = PlayerAttack.PlayerDirection.Down;
                if (posY == (LV.pos.Length - 1))
                    return;
                else
                {
                    if(CheckMovement(posX, posY + 1))
                    {
                        posY++;
                        MoveToPosition();
                    }
                }
            }
        }
    }

    private bool CheckMovement(int x, int y)
    {
        if (LV.pos[x][y].GetComponent<BlockType>().canWalk)
        {
//            if (LV.pos[x][y].GetComponent<BlockType>().type == BlockType.Type.Mage)
//                LV.didPlayerWin = true;
            if (LV.pos[x][y].GetComponent<BlockType>().type == BlockType.Type.Collectable)
                LV.pos[x][y].GetComponent<BlockType>().ChangeBlockType(BlockType.Type.Floor);
            return true;
        }
            
        return false;    
    }

    private void MoveToPosition()
    {
        // transform.position = Vector2.MoveTowards(transform.position, LV.pos[posX][posY].transform.position, 1);
        MoveToPosition(transform.position, LV.pos[posX][posY].transform.position);
        //onPlayerMove?.Invoke();
    }

    private void MoveToPosition(Vector3 initialPosition, Vector3 destination)
    {
        transform.position = Vector2.MoveTowards(initialPosition, destination, 1);
        //onPlayerMove?.Invoke();

        try
        {
            onPlayerMove();
        }
        catch(Exception e)
        {
            Debug.LogError(e);
        }
    }

}
