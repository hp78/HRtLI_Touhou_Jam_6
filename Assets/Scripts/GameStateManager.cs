using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JSAM;

public class GameStateManager : MonoBehaviour
{
    //
    public static GameStateManager instance;

    //
    public GameUIController guc;

    //
    [Space(5)]
    public string currScene;
    public string nextScene;

    //
    [Space(5)]
    public IntVal playerHealth;
    public IntVal comboCount;

    bool isLastScene = false;
    float hardLoadDelay = 0.0f;

    //
    public int musicType = 0;

    //
    public enum GameState { LOADING, PLAYING, PAUSED, NEXTLEVEL, WIN , GAMEOVER };
    public GameState currGameState = GameState.LOADING;

    // Start is called before the first frame update
    void Start()
    {
        if(musicType == 0)
        {
            AudioManager.PlayMusic(Music.Stage2);
        }
        else if (musicType == 1)
        {
            AudioManager.PlayMusic(Music.Boss);
        }
        else if (musicType == 2)
        {
            AudioManager.PlayMusic(Music.Stage);
        }

        instance = this;

        if (guc == null)
            guc = GameObject.Find("UI").GetComponent<GameUIController>();

        Time.timeScale = 0.0f;

        if (currScene == "")
            currScene = SceneManager.GetActiveScene().name;
        if (nextScene == "")
            isLastScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();

        switch(currGameState)
        {
            case GameState.LOADING:
                Loading();
                break;

            case GameState.PLAYING:
                Playing();
                break;

            case GameState.PAUSED:
                Paused();
                break;

            case GameState.NEXTLEVEL:
                Nextlevel();
                break;

            case GameState.WIN:
                Win();
                break;

            case GameState.GAMEOVER:
                Gameover();
                break;
        }
    }

    void Loading()
    {
        hardLoadDelay += Time.unscaledDeltaTime;
        playerHealth.value = 5;
        comboCount.value = 0;

        Time.timeScale = 1.0f;
        guc.SetGameState(1);
        currGameState = GameState.PLAYING;
    }

    void Playing()
    {
        if(playerHealth.value < 1)
        {
            SetGameover();
        }
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

    public void RestartGame()
    {
        SceneManager.LoadScene(currScene);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        currGameState = GameState.PLAYING;
        guc.SetGameState(1);
    }

    public void GoToNextLevel()
    {
        currGameState = GameState.NEXTLEVEL;
        guc.SetGameState(3);
        SceneManager.LoadScene(nextScene);
    }

    public void GoToMainMenu()
    {
        currGameState = GameState.NEXTLEVEL;
        guc.SetGameState(3);
        SceneManager.LoadScene("MainMenu");
    }

    public void SetWin()
    {
        Time.timeScale = 0.0f;
        currGameState = GameState.WIN;
        guc.SetGameState(4);
    }

    public void SetGameover()
    {
        Time.timeScale = 0.0f;
        currGameState = GameState.GAMEOVER;
        guc.SetGameState(5);
    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
            guc.SetGameState(2);
        }

        else if (currGameState == GameState.PAUSED)
        {
            Time.timeScale = 1.0f;
            currGameState = GameState.PLAYING;
            guc.SetGameState(1);
        }
    }
}
