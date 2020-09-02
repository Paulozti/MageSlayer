using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void TryAgain()
    {
        SceneManager.LoadScene(0);
    }
}
