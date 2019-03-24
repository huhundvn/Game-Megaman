using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public Image hp; // máu nhân vật
    public GameObject currentCheckPoint; // kiểm tra vị trí hồi sinh
    public Text score, life, sound; // tính điểm, mạng
    public GameObject pauseMenu, pauseButton;

    private PlayerController player;
    private bool isSound;

    // Use this for initialization
    void Start () {
        isSound = true;
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        score.text = "Score: " + player.myScore.ToString();
        life.text = "x" + player.myLife.ToString();
        hp.fillAmount = player.hpPlayer / 10.0f;

        if (Input.GetKey(KeyCode.Escape))
        {
            pauseGame();
        }
	}

    public void respawnPlayer()
    {
        player.transform.position = currentCheckPoint.transform.position;
    }

    public void pauseGame()
    {
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void returnMain()
    {
        Time.timeScale = 1;
        Application.LoadLevel("MainMenu");
    }

    public void restartGame()
    {
        Time.timeScale = 1;
        Application.LoadLevel("Level01");
    }

    public void gameOver()
    {
        Time.timeScale = 1;
        Application.LoadLevel("GameOver");
    }

    public void changeSound()
    {
        if (isSound)
        {
            AudioListener.volume = 0.0f;
            sound.text = "Sound OFF";
        } else
        {
            AudioListener.volume = 1.0f;
            sound.text = "Sound ON";
        }
        isSound = !isSound;
    }
}
