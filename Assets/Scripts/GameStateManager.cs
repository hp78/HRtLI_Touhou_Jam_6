using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    //
    public GameUIController gameUIController;

    //
    [Space(5)]
    public string currScene;
    public string nextScene;

    //
    [Space(5)]
    public IntVal playerHealth;

    bool isLastScene = false;

    //
    public enum GameState { LOADING, PLAYING, PAUSED, NEXTLEVEL, WIN , GAMEOVER };
    public GameState currGameState = GameState.LOADING;

    // Start is called before the first frame update
    void Start()
    {
        if(currScene == "")
            currScene = SceneManager.GetActiveScene().name;
        if (nextScene == "")
            isLastScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
    }

    void Loading()
    {

    }

    void Playing()
    {
        if(playerHealth.value < 1)
        {

        }
    }

    void Paused()
    {

    }

    void Nextlevel()
    {
        if (isLastScene)
        {

        }
    }

    void Win()
    {

    }

    void Gameover()
    {

    }

    void Restart()
    {
        SceneManager.LoadScene(currScene);
    }

    void UpdateInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleEscape();
        }
    }

    void ToggleEscape()
    {
        if(currGameState == GameState.PLAYING)
        {
            Time.timeScale = 0.0f;
            currGameState = GameState.PAUSED;
            gameUIController.SetGameState(2);
        }

        else if (currGameState == GameState.PAUSED)
        {
            Time.timeScale = 1.0f;
            currGameState = GameState.PLAYING;
            gameUIController.SetGameState(1);
        }
    }
}
