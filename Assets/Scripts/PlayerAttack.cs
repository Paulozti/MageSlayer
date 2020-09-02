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
        if (Input.GetButtonDown("Attack"))
            CheckAttack();
    }

    private void CheckAttack()
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
        switch (PlayerIsLooking)
        {
            case PlayerDirection.Up:
                Attack(LV.pos[x][y - 1].GetComponent<BlockType>());
                break;
            case PlayerDirection.Down:
                Attack(LV.pos[x][y + 1].GetComponent<BlockType>());
                break;
            case PlayerDirection.Right:
                Attack(LV.pos[x + 1][y].GetComponent<BlockType>());
                break;
            case PlayerDirection.Left:
                Attack(LV.pos[x - 1][y].GetComponent<BlockType>());
                break;
        }
    }

    private void Attack(BlockType Block)
    {
        if (Block.type == BlockType.Type.Obstacle)
        {
            Block.ObstacleTakeDamage();
            onPlayerAttack?.Invoke();
            PlayerAnimation.SetTrigger("Attack");
        }
    }
}
