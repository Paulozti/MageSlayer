using UnityEngine;

public class BlockType : MonoBehaviour
{
    public enum Type{ 
        Floor,
        Wall,
        Water,
        Tree,
        MovableRock,
        MovableRockOnFilledHole,
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
    public bool isCollectable = false;

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
                else if (isCollectable)
                {
                    isCollectable = false;
                    Destroy(transform.GetChild(0).gameObject);
                }
                else
                {
                    this.type = Type.Floor;
                    GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.floor_file);
                }
                canWalk = true;
                canBeAttacked = false;
                transform.name = "Floor";
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.Wall:
                this.type = Type.Wall;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.floor_file);
                canWalk = false;
                canBeAttacked = false;
                transform.name = "Wall";
                InstantiateObject("BigRock", loadedLevel.wall_file, new Vector3(1,1,1), this.transform.position);
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.Water:
                this.type = Type.Water;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.water_file);
                canWalk = false;
                canBeAttacked = false;
                transform.name = "Water";
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.Tree:
                this.type = Type.Tree;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.floor_file);
                canWalk = false;
                canBeAttacked = false;
                transform.name = "TreeFloor";
                InstantiateObject("Tree", loadedLevel.tree_file, new Vector3(1f, 1f, 1f), this.transform.position + new Vector3(0f,0.7f,0f));
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.MovableRock:
                this.type = Type.MovableRock;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.movableRock_file);
                canWalk = false;
                canBeAttacked = false;
                transform.name = "MovableRock";
                GetComponent<SpriteRenderer>().sortingOrder = 2;
                break;
            case Type.MovableRockOnFilledHole:
                this.type = Type.MovableRockOnFilledHole;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.movableRock_file);
                canWalk = false;
                canBeAttacked = false;
                transform.name = "MovableRockOnFilledHole";
                GetComponent<SpriteRenderer>().sortingOrder = 2;
                break;
            case Type.Hole:
                this.type = Type.Hole;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.floor_file);
                canWalk = false;
                canBeAttacked = false;
                transform.name = "HoleFloor";
                InstantiateObject("Hole", loadedLevel.hole_file, new Vector3(1f, 1f, 1f), this.transform.position);
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.FilledHole:
                this.type = Type.FilledHole;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.floor_file);
                canWalk = true;
                canBeAttacked = false;
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.filledHole_file);
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.Collectable:
                this.type = Type.Collectable;
                isCollectable = true;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.floor_file);
                canWalk = true;
                canBeAttacked = false;
                transform.name = "CollectableFloor";
                InstantiateObject("Collectable", loadedLevel.collectable_file, new Vector3(0.5f, 0.5f, 0.5f), this.transform.position);
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.Orb:
                this.type = Type.Orb;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.floor_file);
                canWalk = false;
                canBeAttacked = true;
                transform.name = "OrbFloor";
                InstantiateObject("Orb", loadedLevel.orb_file, new Vector3(0.5f, 0.5f, 0.5f), this.transform.position);
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.BrokenOrb:
                this.type = Type.BrokenOrb;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.floor_file);
                canWalk = true;
                canBeAttacked = false;
                transform.name = "OrbFloor";
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.brokenOrb_file);
                Debug.Log(loadedLevel.quantityOfEnemies);
                while(loadedLevel.quantityOfEnemies > 0)
                {
                    Destroy(GameObject.Find("ForceField"));
                    loadedLevel.quantityOfEnemies--;
                }
                    
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.Mage:
                this.type = Type.Mage;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.floor_file);
                canWalk = false;
                canBeAttacked = true;
                transform.name = "MageFloor";
                InstantiateObject("Mage", loadedLevel.mage_file, new Vector3(0.4f, 0.4f, 0.4f), this.transform.position + new Vector3(0,0.5f,0));
                InstantiateObject("ForceField", loadedLevel.forceField_file, new Vector3(0.5f, 0.5f, 0.5f), this.transform.position + new Vector3(0, 0.5f, 0));
                transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = true;
                transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.KilledMage:
                this.type = Type.KilledMage;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.floor_file);
                canWalk = false;
                canBeAttacked = false;
                transform.name = "MageFloor";
                transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = false;
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.killedMage_file);
                transform.GetChild(0).gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.DestructableObstacle:
                this.type = Type.DestructableObstacle;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.destructableObstacle_file);
                canWalk = false;
                canBeAttacked = true;
                transform.name = "DestructableObstacle";
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
        }
        GetComponent<SpriteRenderer>().transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    private void InstantiateObject(string name, string file, Vector3 scale, Vector3 position)
    {
        GameObject newObject = Instantiate(Resources.Load<GameObject>("Prefabs/Object"));
        newObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(file);
        newObject.transform.position = position;
        newObject.name = name;
        newObject.transform.SetParent(this.transform);
        newObject.transform.localScale = scale;
    }
}
