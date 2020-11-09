using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private LoadLevel LV;
    private PlayerMovement PlayerMov;

    public delegate void PlayerAttacked();
    public static event PlayerAttacked onPlayerAttack;

    [SerializeField] private Animator PlayerAnimation;
    public enum PlayerDirection
    {
        Up,
        Right,
        Down,
        Left
    };

    public PlayerDirection PlayerIsLooking = PlayerDirection.Down;
    void Start()
    {
        LV = GameObject.Find("LevelManager").GetComponent<LoadLevel>();
        PlayerMov = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Attack") && LoadLevel.canPlay)
            CheckAttack();
    }


    private void CheckAttack() //Verifica se o player está tentando atacar uma posição dentro do array
    {
        if ((PlayerIsLooking == PlayerDirection.Up && PlayerMov.posY == 0)
            || (PlayerIsLooking == PlayerDirection.Left && PlayerMov.posX == 0)
                || (PlayerIsLooking == PlayerDirection.Right && PlayerMov.posX == (LV.pos.Length - 1))
                    || (PlayerIsLooking == PlayerDirection.Down && PlayerMov.posY == (LV.pos.Length - 1)))
            return;
        else
            VerifyTarget();
    }

    private void VerifyTarget()
    {
        int x = PlayerMov.posX, y = PlayerMov.posY;
        BlockType block;
        switch (PlayerIsLooking)
        {
            case PlayerDirection.Up:
                block = LV.pos[x][y - 1].GetComponent<BlockType>();
                switch (block.type)
                {
                    case BlockType.Type.MovableRock:
                    case BlockType.Type.MovableRockOnFilledHole:
                        if (CheckValidPosition(x, y - 2))
                        {
                            MoveRockToPosition(x, y - 1, x, y - 2);
                        }
                        break;
                    default:
                        if (block.canBeAttacked)
                        {
                            Attack(block);
                        }
                        break;
                }
                break;
            case PlayerDirection.Down:
                block = LV.pos[x][y + 1].GetComponent<BlockType>();
                switch (block.type)
                {
                    case BlockType.Type.MovableRock:
                    case BlockType.Type.MovableRockOnFilledHole:
                        if (CheckValidPosition(x, y + 2))
                        {
                            MoveRockToPosition(x, y + 1, x, y + 2);
                        }
                        break;
                    default:
                        if (block.canBeAttacked)
                        {
                            Attack(block);
                        }
                        break;
                }
                break;
            case PlayerDirection.Right:
                block = LV.pos[x + 1][y].GetComponent<BlockType>();
                switch (block.type)
                {
                    case BlockType.Type.MovableRock:
                    case BlockType.Type.MovableRockOnFilledHole:
                        if (CheckValidPosition(x + 2, y))
                        {
                            MoveRockToPosition(x + 1, y, x + 2, y);
                        }
                        break;
                    default:
                        if (block.canBeAttacked)
                        {
                            Attack(block);
                        }
                        break;
                }
                break;
            case PlayerDirection.Left:
                block = LV.pos[x - 1][y].GetComponent<BlockType>();
                switch (block.type)
                {
                    case BlockType.Type.MovableRock:
                    case BlockType.Type.MovableRockOnFilledHole:
                        if (CheckValidPosition(x - 2, y))
                        {
                            MoveRockToPosition(x - 1, y, x - 2, y);
                        }
                        break;
                    default:
                        if (block.canBeAttacked)
                        {
                            Attack(block);
                        }
                        break;
                }
                break;
        }
    }

    private void Attack(BlockType Block)
    {
            Block.ObstacleTakeDamage(LV.orbDestroyed);
            onPlayerAttack?.Invoke();
            PlayerAnimation.SetTrigger("Attack");
    }

    private bool CheckValidPosition(int posX, int posY) //Verifica se uma posição está dentro do array
    {
        if ((posY < 0) || (posX < 0) || (posX > (LV.pos.Length - 1)) || (posY > (LV.pos.Length - 1)))
            return false;
        else
            return true;
    }

    private void MoveRockToPosition(int xOrigin, int yOrigin, int xDestination, int yDestination)
    {
        BlockType destinationBlock = LV.pos[xDestination][yDestination].GetComponent<BlockType>();
        BlockType originBlock = LV.pos[xOrigin][yOrigin].GetComponent<BlockType>();
        if (destinationBlock.type == BlockType.Type.Floor)
        {
            if(originBlock.type == BlockType.Type.MovableRockOnFilledHole)
            {
                Destroy(originBlock.transform.GetChild(1).gameObject);
                originBlock.ChangeBlockType(BlockType.Type.FilledHole);
            }
            else
            {
                originBlock.ChangeBlockType(BlockType.Type.Floor);
                Destroy(originBlock.transform.GetChild(0).gameObject);
            }
                
            destinationBlock.ChangeBlockType(BlockType.Type.MovableRock);
            onPlayerAttack?.Invoke();
            PlayerAnimation.SetTrigger("Attack");
        }
        else if (destinationBlock.type == BlockType.Type.Hole)
        {
            originBlock.ChangeBlockType(BlockType.Type.Floor);
            Destroy(originBlock.transform.GetChild(0).gameObject);
            destinationBlock.ChangeBlockType(BlockType.Type.FilledHole);
            destinationBlock.isFilledHole = true;
            onPlayerAttack?.Invoke();
            PlayerAnimation.SetTrigger("Attack");
        }
        else if(destinationBlock.type == BlockType.Type.FilledHole)
        {
            if (originBlock.type == BlockType.Type.MovableRockOnFilledHole)
                originBlock.ChangeBlockType(BlockType.Type.FilledHole);
            else
            {
                originBlock.ChangeBlockType(BlockType.Type.Floor);
                Destroy(originBlock.transform.GetChild(0).gameObject);
            }
                
            destinationBlock.ChangeBlockType(BlockType.Type.MovableRockOnFilledHole);
            onPlayerAttack?.Invoke();
            PlayerAnimation.SetTrigger("Attack");
        }
    }
}
