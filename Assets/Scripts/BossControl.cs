using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossControl : MonoBehaviour
{
    public LoadLevel loadLevel;
    public PlayerMovement player;
    public int playerLife = 3;
    public int bossLife = 10;
    public SpriteRenderer playerLifeDisplay;
    public Slider bossLifeDisplay;
    public Text bossLifeText;
    private GameObject healthBar;
    private GameObject bossForceField;
    private int lastRnd = 0;
    private bool canTakeDamage = false;
    private bool bossIsDead = false;
    int rnd = 0;
    void Start()
    {
        PlayerAttack.onPlayerAttack += BossTakeDamage;
        healthBar = GameObject.Find("HealthBar");
        healthBar.SetActive(false);
    }

    void Update()
    {
        if (loadLevel.gameStarted)
        {
            bossForceField = GameObject.Find("ForceField1");
            StartCoroutine(StartPatterns());
        }
    }

    public void BossTakeDamage()
    {
        if (canTakeDamage)
        {
            bossLife--;
            bossLifeDisplay.value = bossLife;
            bossLifeText.text = bossLife + "/50";
            if(bossLife == 0)
            {
                bossIsDead = true;
                LoadLevel.bossLevel = false;
                loadLevel.gotCollectable = true;
                healthBar.SetActive(false);
                loadLevel.Win();
            }
        }
    }
    IEnumerator StartPatterns()
    {
        if (loadLevel.gameStarted)
        {
            loadLevel.gameStarted = false;
            yield return new WaitForSeconds(1f);
            healthBar.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        ChoosePattern();
        yield return new WaitForSeconds(3f);
        ChoosePattern();
        yield return new WaitForSeconds(3f);
        ChoosePattern();
        yield return new WaitForSeconds(3f);
        bossForceField.SetActive(false);
        canTakeDamage = true;
        yield return new WaitForSeconds(2f);
        canTakeDamage = false;
        if (bossIsDead)
        {

        }
        else
        {
            bossForceField.SetActive(true);
            StartCoroutine(StartPatterns());
        }
    }

    private void ChoosePattern()
    {
        do
        {
            rnd = UnityEngine.Random.Range(1, 8);
        }
        while (rnd == lastRnd);
        lastRnd = rnd; 

        switch (rnd)
        {
            case 1:
                Pattern1();
                break;
            case 2:
                Pattern2();
                break;
            case 3:
                Pattern3();
                break;
            case 4:
                Pattern4();
                break;
            case 5:
                Pattern5();
                break;
            case 6:
                Pattern6();
                break;
            case 7: 
                Pattern7();
                break;
            case 8: 
                Pattern8();
                break;

        }
    }

    IEnumerator PreviewBossAttack(int x, int y)
    {
        LeanTween.color(loadLevel.pos[x][y], new Color(0.7f,0.7f,0.7f), 0.15f);
        yield return new WaitForSeconds(0.15f);
        LeanTween.color(loadLevel.pos[x][y], Color.white, 0.15f);
        yield return new WaitForSeconds(0.15f);
        LeanTween.color(loadLevel.pos[x][y], new Color(0.7f, 0.7f, 0.7f), 0.15f);
        yield return new WaitForSeconds(0.15f);
        LeanTween.color(loadLevel.pos[x][y], Color.white, 0.15f);
        yield return new WaitForSeconds(0.15f);
        LeanTween.color(loadLevel.pos[x][y], new Color(1,0,0), 0.15f);
        if (x == player.posX && y == player.posY)
            PlayerTakeDamage();
        yield return new WaitForSeconds(1f);
        LeanTween.color(loadLevel.pos[x][y], Color.white, 1f);
    }

    private void PlayerTakeDamage()
    {
        playerLife--;
        if(playerLife == 2)
        {
            playerLifeDisplay.sprite = Resources.Load<Sprite>("2hearts");
        }
        else if (playerLife == 1)
        {
            playerLifeDisplay.sprite = Resources.Load<Sprite>("1hearts");
        }
        else
        {
            healthBar.SetActive(false);
            loadLevel.LoseGame();
        }
            
    }

    private void Pattern1()
    {
        StartCoroutine(PreviewBossAttack(1, 1));
        StartCoroutine(PreviewBossAttack(2, 1));
        StartCoroutine(PreviewBossAttack(3, 1));
        StartCoroutine(PreviewBossAttack(4, 1));
        StartCoroutine(PreviewBossAttack(5, 1));
        StartCoroutine(PreviewBossAttack(6, 1));
        StartCoroutine(PreviewBossAttack(7, 1));
        StartCoroutine(PreviewBossAttack(8, 1));

        StartCoroutine(PreviewBossAttack(1, 2));
        StartCoroutine(PreviewBossAttack(2, 2));
        StartCoroutine(PreviewBossAttack(7, 2));
        StartCoroutine(PreviewBossAttack(8, 2));

        StartCoroutine(PreviewBossAttack(1, 3));
        StartCoroutine(PreviewBossAttack(3, 3));
        StartCoroutine(PreviewBossAttack(6, 3));
        StartCoroutine(PreviewBossAttack(8, 3));

        StartCoroutine(PreviewBossAttack(1, 4));
        StartCoroutine(PreviewBossAttack(4, 4));
        StartCoroutine(PreviewBossAttack(5, 4));
        StartCoroutine(PreviewBossAttack(8, 4));

        StartCoroutine(PreviewBossAttack(1, 5));
        StartCoroutine(PreviewBossAttack(4, 5));
        StartCoroutine(PreviewBossAttack(5, 5));
        StartCoroutine(PreviewBossAttack(8, 5));

        StartCoroutine(PreviewBossAttack(1, 6));
        StartCoroutine(PreviewBossAttack(3, 6));
        StartCoroutine(PreviewBossAttack(6, 6));
        StartCoroutine(PreviewBossAttack(8, 6));

        StartCoroutine(PreviewBossAttack(1, 7));
        StartCoroutine(PreviewBossAttack(2, 7));
        StartCoroutine(PreviewBossAttack(3, 7));
        StartCoroutine(PreviewBossAttack(4, 7));
        StartCoroutine(PreviewBossAttack(5, 7));
        StartCoroutine(PreviewBossAttack(6, 7));
        StartCoroutine(PreviewBossAttack(7, 7));
        StartCoroutine(PreviewBossAttack(8, 7));

        StartCoroutine(PreviewBossAttack(1, 8));
        StartCoroutine(PreviewBossAttack(2, 8));
        StartCoroutine(PreviewBossAttack(3, 8));
        StartCoroutine(PreviewBossAttack(4, 8));
        StartCoroutine(PreviewBossAttack(5, 8));
        StartCoroutine(PreviewBossAttack(6, 8));
        StartCoroutine(PreviewBossAttack(7, 8));
        StartCoroutine(PreviewBossAttack(8, 8));
    }
    private void Pattern2() 
    {
        StartCoroutine(PreviewBossAttack(4, 1));
        StartCoroutine(PreviewBossAttack(5, 1));

        StartCoroutine(PreviewBossAttack(3, 2));
        StartCoroutine(PreviewBossAttack(4, 2));
        StartCoroutine(PreviewBossAttack(5, 2));
        StartCoroutine(PreviewBossAttack(6, 2));

        StartCoroutine(PreviewBossAttack(2, 3));
        StartCoroutine(PreviewBossAttack(3, 3));
        StartCoroutine(PreviewBossAttack(6, 3));
        StartCoroutine(PreviewBossAttack(7, 3));

        StartCoroutine(PreviewBossAttack(1, 4));
        StartCoroutine(PreviewBossAttack(2, 4));
        StartCoroutine(PreviewBossAttack(7, 4));
        StartCoroutine(PreviewBossAttack(8, 4));

        StartCoroutine(PreviewBossAttack(2, 5));
        StartCoroutine(PreviewBossAttack(3, 5));
        StartCoroutine(PreviewBossAttack(6, 5));
        StartCoroutine(PreviewBossAttack(7, 5));

        StartCoroutine(PreviewBossAttack(3, 6));
        StartCoroutine(PreviewBossAttack(4, 6));
        StartCoroutine(PreviewBossAttack(5, 6));
        StartCoroutine(PreviewBossAttack(6, 6));

        StartCoroutine(PreviewBossAttack(4, 7));
        StartCoroutine(PreviewBossAttack(5, 7));

        StartCoroutine(PreviewBossAttack(2, 8));
        StartCoroutine(PreviewBossAttack(3, 8));
        StartCoroutine(PreviewBossAttack(4, 8));
        StartCoroutine(PreviewBossAttack(5, 8));
        StartCoroutine(PreviewBossAttack(6, 8));
        StartCoroutine(PreviewBossAttack(7, 8));
    }
    private void Pattern3() 
    {
        StartCoroutine(PreviewBossAttack(2, 1));
        StartCoroutine(PreviewBossAttack(3, 1));
        StartCoroutine(PreviewBossAttack(4, 1));
        StartCoroutine(PreviewBossAttack(5, 1));
        StartCoroutine(PreviewBossAttack(6, 1));
        StartCoroutine(PreviewBossAttack(7, 1));

        StartCoroutine(PreviewBossAttack(1, 2));
        StartCoroutine(PreviewBossAttack(8, 2));

        StartCoroutine(PreviewBossAttack(1, 3));
        StartCoroutine(PreviewBossAttack(4, 3));
        StartCoroutine(PreviewBossAttack(5, 3));
        StartCoroutine(PreviewBossAttack(8, 3));

        StartCoroutine(PreviewBossAttack(1, 4));
        StartCoroutine(PreviewBossAttack(2, 4));
        StartCoroutine(PreviewBossAttack(3, 4));
        StartCoroutine(PreviewBossAttack(4, 4));
        StartCoroutine(PreviewBossAttack(5, 4));
        StartCoroutine(PreviewBossAttack(6, 4));
        StartCoroutine(PreviewBossAttack(7, 4));
        StartCoroutine(PreviewBossAttack(8, 4));

        StartCoroutine(PreviewBossAttack(1, 5));
        StartCoroutine(PreviewBossAttack(4, 5));
        StartCoroutine(PreviewBossAttack(5, 5));
        StartCoroutine(PreviewBossAttack(8, 5));

        StartCoroutine(PreviewBossAttack(1, 6));
        StartCoroutine(PreviewBossAttack(4, 6));
        StartCoroutine(PreviewBossAttack(5, 6));
        StartCoroutine(PreviewBossAttack(8, 6));

        StartCoroutine(PreviewBossAttack(2, 7));
        StartCoroutine(PreviewBossAttack(3, 7));
        StartCoroutine(PreviewBossAttack(4, 7));
        StartCoroutine(PreviewBossAttack(5, 7));
        StartCoroutine(PreviewBossAttack(6, 7));
        StartCoroutine(PreviewBossAttack(7, 7));

        StartCoroutine(PreviewBossAttack(3, 8));
        StartCoroutine(PreviewBossAttack(4, 8));
        StartCoroutine(PreviewBossAttack(5, 8));
        StartCoroutine(PreviewBossAttack(6, 8));
    } 
    private void Pattern4() 
    {
        StartCoroutine(PreviewBossAttack(2, 1));
        StartCoroutine(PreviewBossAttack(3, 1));
        StartCoroutine(PreviewBossAttack(4, 1));
        StartCoroutine(PreviewBossAttack(5, 1));
        StartCoroutine(PreviewBossAttack(6, 1));
        StartCoroutine(PreviewBossAttack(7, 1));

        StartCoroutine(PreviewBossAttack(4, 2));
        StartCoroutine(PreviewBossAttack(5, 2));

        StartCoroutine(PreviewBossAttack(4, 3));
        StartCoroutine(PreviewBossAttack(5, 3));

        StartCoroutine(PreviewBossAttack(3, 4));
        StartCoroutine(PreviewBossAttack(4, 4));
        StartCoroutine(PreviewBossAttack(5, 4));
        StartCoroutine(PreviewBossAttack(6, 4));

        StartCoroutine(PreviewBossAttack(3, 5));
        StartCoroutine(PreviewBossAttack(4, 5));
        StartCoroutine(PreviewBossAttack(5, 5));
        StartCoroutine(PreviewBossAttack(6, 5));

        StartCoroutine(PreviewBossAttack(4, 6));
        StartCoroutine(PreviewBossAttack(5, 6));

        StartCoroutine(PreviewBossAttack(4, 7));
        StartCoroutine(PreviewBossAttack(5, 7));

        StartCoroutine(PreviewBossAttack(2, 8));
        StartCoroutine(PreviewBossAttack(3, 8));
        StartCoroutine(PreviewBossAttack(4, 8));
        StartCoroutine(PreviewBossAttack(5, 8));
        StartCoroutine(PreviewBossAttack(6, 8));
        StartCoroutine(PreviewBossAttack(7, 8));
    }
    private void Pattern5() 
    {
        StartCoroutine(PreviewBossAttack(7, 1));
        StartCoroutine(PreviewBossAttack(8, 1));

        StartCoroutine(PreviewBossAttack(6, 2));
        StartCoroutine(PreviewBossAttack(7, 2));
        StartCoroutine(PreviewBossAttack(8, 2));

        StartCoroutine(PreviewBossAttack(5, 3));
        StartCoroutine(PreviewBossAttack(6, 3));
        StartCoroutine(PreviewBossAttack(7, 3));

        StartCoroutine(PreviewBossAttack(4, 4));
        StartCoroutine(PreviewBossAttack(5, 4));
        StartCoroutine(PreviewBossAttack(6, 4));

        StartCoroutine(PreviewBossAttack(3, 5));
        StartCoroutine(PreviewBossAttack(4, 5));
        StartCoroutine(PreviewBossAttack(5, 5));

        StartCoroutine(PreviewBossAttack(1, 6));
        StartCoroutine(PreviewBossAttack(2, 6));
        StartCoroutine(PreviewBossAttack(3, 6));
        StartCoroutine(PreviewBossAttack(4, 6));
        StartCoroutine(PreviewBossAttack(5, 6));
        StartCoroutine(PreviewBossAttack(6, 6));
        StartCoroutine(PreviewBossAttack(7, 6));
        StartCoroutine(PreviewBossAttack(8, 6));

        StartCoroutine(PreviewBossAttack(1, 7));
        StartCoroutine(PreviewBossAttack(2, 7));
        StartCoroutine(PreviewBossAttack(3, 7));
        StartCoroutine(PreviewBossAttack(4, 7));
        StartCoroutine(PreviewBossAttack(5, 7));
        StartCoroutine(PreviewBossAttack(6, 7));
        StartCoroutine(PreviewBossAttack(7, 7));
        StartCoroutine(PreviewBossAttack(8, 7));

        StartCoroutine(PreviewBossAttack(1, 8));
        StartCoroutine(PreviewBossAttack(2, 8));
        StartCoroutine(PreviewBossAttack(3, 8));
        StartCoroutine(PreviewBossAttack(4, 8));
        StartCoroutine(PreviewBossAttack(5, 8));
        StartCoroutine(PreviewBossAttack(6, 8));
        StartCoroutine(PreviewBossAttack(7, 8));
        StartCoroutine(PreviewBossAttack(8, 8));
    }
    private void Pattern6() 
    {
        StartCoroutine(PreviewBossAttack(1, 1));
        StartCoroutine(PreviewBossAttack(2, 1));

        StartCoroutine(PreviewBossAttack(1, 2));
        StartCoroutine(PreviewBossAttack(2, 2));
        StartCoroutine(PreviewBossAttack(3, 2));

        StartCoroutine(PreviewBossAttack(2, 3));
        StartCoroutine(PreviewBossAttack(3, 3));
        StartCoroutine(PreviewBossAttack(4, 3));

        StartCoroutine(PreviewBossAttack(3, 4));
        StartCoroutine(PreviewBossAttack(4, 4));
        StartCoroutine(PreviewBossAttack(5, 4));

        StartCoroutine(PreviewBossAttack(4, 5));
        StartCoroutine(PreviewBossAttack(5, 5));
        StartCoroutine(PreviewBossAttack(6, 5));

        StartCoroutine(PreviewBossAttack(1, 6));
        StartCoroutine(PreviewBossAttack(2, 6));
        StartCoroutine(PreviewBossAttack(3, 6));
        StartCoroutine(PreviewBossAttack(4, 6));
        StartCoroutine(PreviewBossAttack(5, 6));
        StartCoroutine(PreviewBossAttack(6, 6));
        StartCoroutine(PreviewBossAttack(7, 6));
        StartCoroutine(PreviewBossAttack(8, 6));

        StartCoroutine(PreviewBossAttack(1, 7));
        StartCoroutine(PreviewBossAttack(2, 7));
        StartCoroutine(PreviewBossAttack(3, 7));
        StartCoroutine(PreviewBossAttack(4, 7));
        StartCoroutine(PreviewBossAttack(5, 7));
        StartCoroutine(PreviewBossAttack(6, 7));
        StartCoroutine(PreviewBossAttack(7, 7));
        StartCoroutine(PreviewBossAttack(8, 7));

        StartCoroutine(PreviewBossAttack(1, 8));
        StartCoroutine(PreviewBossAttack(2, 8));
        StartCoroutine(PreviewBossAttack(3, 8));
        StartCoroutine(PreviewBossAttack(4, 8));
        StartCoroutine(PreviewBossAttack(5, 8));
        StartCoroutine(PreviewBossAttack(6, 8));
        StartCoroutine(PreviewBossAttack(7, 8));
        StartCoroutine(PreviewBossAttack(8, 8));
    }
    private void Pattern7() 
    {
        StartCoroutine(PreviewBossAttack(1, 1));
        StartCoroutine(PreviewBossAttack(3, 1));
        StartCoroutine(PreviewBossAttack(6, 1));
        StartCoroutine(PreviewBossAttack(8, 1));

        StartCoroutine(PreviewBossAttack(1, 2));
        StartCoroutine(PreviewBossAttack(2, 2));
        StartCoroutine(PreviewBossAttack(3, 2));
        StartCoroutine(PreviewBossAttack(4, 2));
        StartCoroutine(PreviewBossAttack(5, 2));
        StartCoroutine(PreviewBossAttack(6, 2));
        StartCoroutine(PreviewBossAttack(7, 2));
        StartCoroutine(PreviewBossAttack(8, 2));

        StartCoroutine(PreviewBossAttack(1, 3));
        StartCoroutine(PreviewBossAttack(3, 3));
        StartCoroutine(PreviewBossAttack(5, 3));
        StartCoroutine(PreviewBossAttack(7, 3));
        StartCoroutine(PreviewBossAttack(8, 3));

        StartCoroutine(PreviewBossAttack(1, 4));
        StartCoroutine(PreviewBossAttack(2, 4));
        StartCoroutine(PreviewBossAttack(4, 4));
        StartCoroutine(PreviewBossAttack(6, 4));
        StartCoroutine(PreviewBossAttack(8, 4));

        StartCoroutine(PreviewBossAttack(1, 5));
        StartCoroutine(PreviewBossAttack(3, 5));
        StartCoroutine(PreviewBossAttack(5, 5));
        StartCoroutine(PreviewBossAttack(7, 5));
        StartCoroutine(PreviewBossAttack(8, 5));

        StartCoroutine(PreviewBossAttack(1, 6));
        StartCoroutine(PreviewBossAttack(2, 6));
        StartCoroutine(PreviewBossAttack(4, 6));
        StartCoroutine(PreviewBossAttack(6, 6));
        StartCoroutine(PreviewBossAttack(8, 6));

        StartCoroutine(PreviewBossAttack(1, 7));
        StartCoroutine(PreviewBossAttack(2, 7));
        StartCoroutine(PreviewBossAttack(3, 7));
        StartCoroutine(PreviewBossAttack(4, 7));
        StartCoroutine(PreviewBossAttack(5, 7));
        StartCoroutine(PreviewBossAttack(6, 7));
        StartCoroutine(PreviewBossAttack(7, 7));
        StartCoroutine(PreviewBossAttack(8, 7));

        StartCoroutine(PreviewBossAttack(1, 8));
        StartCoroutine(PreviewBossAttack(3, 8));
        StartCoroutine(PreviewBossAttack(4, 8));
        StartCoroutine(PreviewBossAttack(5, 8));
        StartCoroutine(PreviewBossAttack(6, 8));
        StartCoroutine(PreviewBossAttack(8, 8));

    }
    private void Pattern8() 
    {
        StartCoroutine(PreviewBossAttack(2, 1));
        StartCoroutine(PreviewBossAttack(4, 1));
        StartCoroutine(PreviewBossAttack(5, 1));
        StartCoroutine(PreviewBossAttack(7, 1));

        StartCoroutine(PreviewBossAttack(1, 2));
        StartCoroutine(PreviewBossAttack(2, 2));
        StartCoroutine(PreviewBossAttack(3, 2));
        StartCoroutine(PreviewBossAttack(6, 2));
        StartCoroutine(PreviewBossAttack(7, 2));
        StartCoroutine(PreviewBossAttack(8, 2));

        StartCoroutine(PreviewBossAttack(2, 3));
        StartCoroutine(PreviewBossAttack(4, 3));
        StartCoroutine(PreviewBossAttack(5, 3));
        StartCoroutine(PreviewBossAttack(7, 3));

        StartCoroutine(PreviewBossAttack(1, 4));
        StartCoroutine(PreviewBossAttack(3, 4));
        StartCoroutine(PreviewBossAttack(5, 4));
        StartCoroutine(PreviewBossAttack(6, 4));
        StartCoroutine(PreviewBossAttack(8, 4));

        StartCoroutine(PreviewBossAttack(2, 5));
        StartCoroutine(PreviewBossAttack(4, 5));
        StartCoroutine(PreviewBossAttack(6, 5));
        StartCoroutine(PreviewBossAttack(8, 5));

        StartCoroutine(PreviewBossAttack(1, 6));
        StartCoroutine(PreviewBossAttack(3, 6));
        StartCoroutine(PreviewBossAttack(4, 6));
        StartCoroutine(PreviewBossAttack(5, 6));
        StartCoroutine(PreviewBossAttack(7, 6));

        StartCoroutine(PreviewBossAttack(2, 7));
        StartCoroutine(PreviewBossAttack(4, 7));
        StartCoroutine(PreviewBossAttack(6, 7));
        StartCoroutine(PreviewBossAttack(8, 7));

        StartCoroutine(PreviewBossAttack(1, 8));
        StartCoroutine(PreviewBossAttack(3, 8));
        StartCoroutine(PreviewBossAttack(5, 8));
        StartCoroutine(PreviewBossAttack(7, 8));
        StartCoroutine(PreviewBossAttack(8, 8));
    }

    private void OnDestroy()
    {
        PlayerAttack.onPlayerAttack -= BossTakeDamage;
    }
}
