using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{

    private static GameStateManager instance;
    public static GameStateManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    [Header("Pause")]
    public static bool paused;
    public GameObject pauseUI;

    [Header("Encounter")]
    public static bool inEncounter;
    private EncounterManager encounterManager;

    [Header("Rewards")]
    public static bool showRewards;
    public GameObject rewardsUI;

    [Header("Game Over")]
    public static bool gameOver;
    public GameObject gameOverUI;

    private void Awake()
    {

        if (instance != null)
        {
            Debug.Log("More than one GameStateManager instance");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        gameOver = false;
        paused = false;
        showRewards = false;
        inEncounter = false;

        Time.timeScale = 1f;

        StartEncounter();
    }

    private void Update()
    {
        if (gameOver)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("p"))
        {
            Pause();
        }

        if(encounterManager.encounterEnded){
            EndEncounter();
        }

         if(inEncounter){
            encounterManager.Update();
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

    public void StartEncounter(){
        inEncounter = true;
        encounterManager = (EncounterManager) ScriptableObject.CreateInstance("EncounterManager");
        encounterManager.Init();
    }

    public void EndEncounter(){
        inEncounter = false;
        ShowRewards();
    }

    private void ShowRewards(){
        showRewards = true;
        rewardsUI.SetActive(true);
    }

    private void Pause()
    {
        paused = true;
        pauseUI.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        paused = false;
        pauseUI.SetActive(false);

        Time.timeScale = 1f;
    }
}
