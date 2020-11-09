using UnityEngine;

public class Level : MonoBehaviour
{
    public string floor_file, wall_file, water_file, tree_file, movableRock_file, hole_file, filledHole_file, collectable_file, orb_file, brokenOrb_file, mage_file, killedMage_file, destructableObstacle_file, forceField_file;
    public int quantityOfEnemiesWithShield = 0;
    public int quantityOfEnemiesInAlive = 0;
    public int createdForceFields = 0;
    public bool elite = false;
    public bool boss = false;

    public virtual void LoadSprites()
    {
        
        floor_file = "";
        wall_file = "";
        water_file = "";
        tree_file = "";
        movableRock_file = "";
        hole_file = "";
        filledHole_file = "";
        collectable_file = "";
        orb_file = "";
        brokenOrb_file = "";
        mage_file = "";
        killedMage_file = "";
        destructableObstacle_file = "";
        forceField_file = "";
    }
}
