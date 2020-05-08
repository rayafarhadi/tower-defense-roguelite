using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public SceneFader sceneFader;
    public string menuScene = "MainMenu";

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("p")){
            Continue();
        }
    }

    public void Continue(){
        GameStateManager.Instance.Unpause();
    }

    public void Restart(){
        GameStateManager.Instance.Unpause();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu() {
        GameStateManager.Instance.Unpause();
        sceneFader.FadeTo(menuScene);
    }
}
