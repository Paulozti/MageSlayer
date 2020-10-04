using UnityEngine;

public class BlockType : MonoBehaviour
{
    public enum Type{ 
        Floor,
        Wall,
        MovableRock,
        Hole,
        FilledHole,
        Collectable,
        Orb,
        BrokenOrb,
        Mage,
        KilledMage,
        DestructableObstacle
    };

    public Type type = Type.Floor;

    public int life = 1;

    private Level loadedLevel;

    public bool canWalk = false;
    public bool canBeAttacked = false;

    public bool isFilledHole = false;

    private void Awake()
    {
        loadedLevel = GameObject.Find("LevelManager").GetComponent<Level>();
    }

    public void ObstacleTakeDamage()
    {
        life--;

        if(life <= 0)
        {
            // destroy animation
            switch (type)
            {
                case Type.DestructableObstacle:
                    ChangeBlockType(Type.Floor);
                    break;
                case Type.Orb:
                    ChangeBlockType(Type.BrokenOrb);
                    break;
                case Type.Mage:
                    ChangeBlockType(Type.KilledMage);
                    break;
            }
        }
    }

    public void ChangeBlockType(Type type)
    {
        switch (type)
        {
            case Type.Floor:
                if (isFilledHole)
                {
                    this.type = Type.FilledHole;
                    GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.filledHole_file);
                }
                else
                {
                    this.type = Type.Floor;
                    GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.floor_file);
                }
                canWalk = true;
                canBeAttacked = false;
                transform.name = "Floor";
                break;
            case Type.Wall:
                this.type = Type.Wall;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.wall_file);
                canWalk = false;
                canBeAttacked = false;
                transform.name = "Wall";
                break;
            case Type.MovableRock:
                this.type = Type.MovableRock;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.movableRock_file);
                canWalk = false;
                canBeAttacked = false;
                transform.name = "MovableRock";
                break;
            case Type.Hole:
                this.type = Type.Hole;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.hole_file);
                canWalk = false;
                canBeAttacked = false;
                transform.name = "Hole";
                break;
            case Type.FilledHole:
                this.type = Type.FilledHole;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.filledHole_file);
                canWalk = true;
                canBeAttacked = false;
                transform.name = "FilledHole";
                break;
            case Type.Collectable:
                this.type = Type.Collectable;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.collectable_file);
                canWalk = true;
                canBeAttacked = false;
                transform.name = "Collectable";
                break;
            case Type.Orb:
                this.type = Type.Orb;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.orb_file);
                canWalk = false;
                canBeAttacked = true;
                transform.name = "Orb";
                break;
            case Type.BrokenOrb:
                this.type = Type.BrokenOrb;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.brokenOrb_file);
                canWalk = true;
                canBeAttacked = false;
                transform.name = "BrokenOrb";
                break;
            case Type.Mage:
                this.type = Type.Mage;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.mage_file);
                canWalk = false;
                canBeAttacked = true;
                transform.name = "Mage";
                break;
            case Type.KilledMage:
                this.type = Type.KilledMage;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.killedMage_file);
                canWalk = false;
                canBeAttacked = false;
                transform.name = "KilledMage";
                break;
            case Type.DestructableObstacle:
                this.type = Type.DestructableObstacle;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.destructableObstacle_file);
                canWalk = false;
                canBeAttacked = true;
                transform.name = "DestructableObstacle";
                break;
        }
    }
}
