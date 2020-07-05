using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour


{
    int brokenBottles = 0;
    int score = 0;

    GameObject player;

    public bool gameOver = false;

    public TMP_Text scoreTxt;
    public TMP_Text gameoverScoreText;
    public GameObject playerHealthUi;
    public GameObject gameoverPanel;

    Animator scoreAnim;

    AudioManager audioMan;

    public int BrokenBottles { get => brokenBottles; private set => brokenBottles = value; }
    public int Score { get => score; private set => score = value; }

    // Start is called before the first frame update
    void Start()
    {
        gameoverPanel.SetActive(false);
        player = GameObject.Find("Player");
        scoreAnim = scoreTxt.GetComponent<Animator>();
        audioMan = AudioManager.instance;
        if (audioMan == null)
        {
            Debug.LogError("No Audio Manager Found in the scene.");
        }
        audioMan.StopSound("MenuBgm");
        audioMan.StopSound("Laugh");
        audioMan.PlaySound("LevelBgm");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addBrokenBottle(int amount)
    {
        checkForGameOver();
        BrokenBottles += amount;
        player.GetComponent<PlayerAnimationManager>().WasHit();
        player.GetComponent<PlayerHealthSystem>().DecreaseHealth();
    }

    public void addToScore(int amount)
    {
        audioMan.PlaySound("AddScore");
        Score += amount;
        scoreAnim.SetTrigger("add");
        scoreTxt.text = Score.ToString();
    }

    public void checkForGameOver()
    {
        if (!player.GetComponent<PlayerHealthSystem>().IsAlive)
        {
            gameOver = true;
            audioMan.PlaySound("GameOver");
            audioMan.PlaySound("Laugh");
            LoadGameOver();
        }
    }

    void LoadGameOver()
    {
        playerHealthUi.SetActive(false);
        gameoverPanel.SetActive(true);

        //set final score
        gameoverScoreText.text = Score.ToString();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
