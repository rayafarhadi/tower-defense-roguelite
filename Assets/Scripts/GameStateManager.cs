using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    private static GameStateManager instance;
    public static GameStateManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    public static bool gameOver;
    public GameObject gameOverUI;

    public static bool paused;
    public GameObject pauseUI;

    private void Awake()
    {

        if (instance != null)
        {
            Debug.Log("More than one BuildManager instance");
            return;
        }
        instance = this;
    }

    private void Start() {
        gameOver = false;
        paused = false;

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (gameOver)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("p")){
            Pause();
        }

        if (PlayerStats.lives <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        //Game Over
        gameOver = true;
        gameOverUI.SetActive(true);
    }

    private void Pause(){
        paused = true;
        pauseUI.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Unpause(){
        paused = false;
        pauseUI.SetActive(false);

        Time.timeScale = 1f;
    }
}
