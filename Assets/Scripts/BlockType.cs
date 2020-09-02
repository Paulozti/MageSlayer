using UnityEngine;

public class BlockType : MonoBehaviour
{
    public enum Type{ 
        Floor,
        Wall,
        Obstacle,
        Win
    };

    public Type type = Type.Floor;

    public int life = 1;

    public void ObstacleTakeDamage()
    {
        life--;

        if(life <= 0)
        {
            // destroy animation
            type = Type.Floor;
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Floor");
        }
    }
}
