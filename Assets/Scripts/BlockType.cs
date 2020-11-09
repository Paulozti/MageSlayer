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

    public string floorTile = "";
    public string waterTile = "";

    private void Awake()
    {
        loadedLevel = GameObject.Find("LevelManager").GetComponent<Level>();
    }

    public void ObstacleTakeDamage(bool orbIsDestroyed)
    {
        if (type == Type.Mage && !orbIsDestroyed)
            return;

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
                    loadedLevel.gameObject.GetComponent<LoadLevel>().orbDestroyed = true;
                    break;
                case Type.Mage:
                    ChangeBlockType(Type.KilledMage);
                    loadedLevel.quantityOfEnemiesInAlive--;
                    if (loadedLevel.quantityOfEnemiesInAlive <= 0)
                        loadedLevel.gameObject.GetComponent<LoadLevel>().didPlayerWin = true;
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
                    CheckFloorTile();
                }
                else if (isCollectable)
                {
                    isCollectable = false;
                    loadedLevel.GetComponent<LoadLevel>().gotCollectable = true;
                    Destroy(transform.GetChild(0).gameObject);
                }
                else
                {
                    this.type = Type.Floor;
                    CheckFloorTile();
                }
                canWalk = true;
                canBeAttacked = false;
                transform.name = "Floor";
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.Wall:
                this.type = Type.Wall;
                CheckFloorTile();
                canWalk = false;
                canBeAttacked = false;
                transform.name = "Wall";
                InstantiateObject("BigRock", loadedLevel.wall_file, new Vector3(1,1,1), this.transform.position, false, false);
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.Water:
                this.type = Type.Water;
                CheckWaterTile();
                canWalk = false;
                canBeAttacked = false;
                transform.name = "Water";
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.Tree:
                this.type = Type.Tree;
                CheckFloorTile();
                canWalk = false;
                canBeAttacked = false;
                transform.name = "TreeFloor";
                InstantiateObject("Tree", loadedLevel.tree_file, new Vector3(1f, 1f, 1f), this.transform.position + new Vector3(0f,0.7f,0f), false, false);
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 4;
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.MovableRock:
                this.type = Type.MovableRock;
                CheckFloorTile();
                canWalk = false;
                canBeAttacked = false;
                transform.name = "MovableRockFloor";
                InstantiateObject("MovableRock", loadedLevel.movableRock_file, new Vector3(1, 1, 1), this.transform.position, false, false);
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
                GetComponent<SpriteRenderer>().sortingOrder = 2;
                break;
            case Type.MovableRockOnFilledHole:
                this.type = Type.MovableRockOnFilledHole;
                CheckFloorTile();
                canWalk = false;
                canBeAttacked = false;
                transform.name = "MovableRockOnFilledHole";
                InstantiateObject("MovableRock", loadedLevel.movableRock_file, new Vector3(1, 1, 1), this.transform.position, false, false);
                transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
                GetComponent<SpriteRenderer>().sortingOrder = 2;
                break;
            case Type.Hole:
                this.type = Type.Hole;
                CheckFloorTile();
                canWalk = false;
                canBeAttacked = false;
                transform.name = "HoleFloor";
                InstantiateObject("Hole", loadedLevel.hole_file, new Vector3(1f, 1f, 1f), this.transform.position, false, false);
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.FilledHole:
                this.type = Type.FilledHole;
                CheckFloorTile();
                canWalk = true;
                canBeAttacked = false;
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.filledHole_file);
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.Collectable:
                this.type = Type.Collectable;
                isCollectable = true;
                CheckFloorTile();
                canWalk = true;
                canBeAttacked = false;
                transform.name = "CollectableFloor";
                InstantiateObject("Collectable", loadedLevel.collectable_file, new Vector3(0.5f, 0.5f, 0.5f), this.transform.position, false, false);
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.Orb:
                this.type = Type.Orb;
                CheckFloorTile();
                canWalk = false;
                canBeAttacked = true;
                transform.name = "OrbFloor";
                InstantiateObject("Orb", loadedLevel.orb_file, new Vector3(0.5f, 0.5f, 0.5f), this.transform.position, false, false);
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.BrokenOrb:
                this.type = Type.BrokenOrb;
                CheckFloorTile();
                canWalk = true;
                canBeAttacked = false;
                transform.name = "OrbFloor";
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.brokenOrb_file);
                while(loadedLevel.quantityOfEnemiesWithShield > 0)
                {
                    Destroy(GameObject.Find("ForceField"+ loadedLevel.quantityOfEnemiesWithShield));
                    loadedLevel.quantityOfEnemiesWithShield--;
                }
                    
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.Mage:
                this.type = Type.Mage;
                CheckFloorTile();
                canWalk = false;
                canBeAttacked = true;
                transform.name = "MageFloor";
                InstantiateObject("Mage", loadedLevel.mage_file, new Vector3(0.4f, 0.4f, 0.4f), this.transform.position + new Vector3(0,0.5f,0), loadedLevel.elite, loadedLevel.boss);
                loadedLevel.createdForceFields++;
                InstantiateObject("ForceField"+loadedLevel.createdForceFields, loadedLevel.forceField_file, new Vector3(0.5f, 0.5f, 0.5f), this.transform.position + new Vector3(0, 0.5f, 0), false, false);
                transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = true;
                transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case Type.KilledMage:
                this.type = Type.KilledMage;
                CheckFloorTile();
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

    private void InstantiateObject(string name, string file, Vector3 scale, Vector3 position, bool elite, bool boss)
    {
        GameObject newObject;
        if (boss)
        {
            newObject = Instantiate(Resources.Load<GameObject>("Prefabs/Boss"));
        }
        else if (elite)
        {
            scale = new Vector3(0.45f, 0.45f, 0.45f);
            newObject = Instantiate(Resources.Load<GameObject>("Prefabs/Mage"));
        }
        else
            newObject = Instantiate(Resources.Load<GameObject>("Prefabs/Object"));

        newObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(file);
        newObject.transform.position = position;
        newObject.name = name;
        newObject.transform.SetParent(this.transform);
        newObject.transform.localScale = scale;
    }

    private void CheckFloorTile()
    {
        if(floorTile == "")
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.floor_file);
        else
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/TileMap/Grass/" + floorTile);
    }

    private void CheckWaterTile()
    {
        if (waterTile == "")
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(loadedLevel.water_file);
        else
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/TileMap/Water/" + waterTile);
    }
}
