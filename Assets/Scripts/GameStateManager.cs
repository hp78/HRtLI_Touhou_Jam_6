using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    //
    public GameUIController gameUIController;

    //
    public enum GameState { LOADING, PLAYING, PAUSED, NEXTLEVEL, WIN , GAMEOVER };
    public GameState currGameState = GameState.LOADING;

    // Start is called before the first frame update
    void Start()
    {
        
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

    }

    void Paused()
    {

    }

    void Nextlevel()
    {

    }

    void Win()
    {

    }

    void Gameover()
    {

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
