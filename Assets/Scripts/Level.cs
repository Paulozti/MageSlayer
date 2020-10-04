using UnityEngine;

public class Level : MonoBehaviour
{
    public string floor_file, wall_file, movableRock_file, hole_file, filledHole_file, collectable_file, orb_file, brokenOrb_file, mage_file, killedMage_file, destructableObstacle_file;

    public virtual void LoadSprites()
    {
        
        floor_file = "";
        wall_file = "";
        movableRock_file = "";
        hole_file = "";
        filledHole_file = "";
        collectable_file = "";
        orb_file = "";
        brokenOrb_file = "";
        mage_file = "";
        killedMage_file = "";
        destructableObstacle_file = "";

    }
}
