using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    //
    public GameUIController guc;

    //
    [Space(5)]
    public string currScene;
    public string nextScene;

    //
    [Space(5)]
    public IntVal playerHealth;

    bool isLastScene = false;
    float hardLoadDelay = 0.0f;

    //
    public enum GameState { LOADING, PLAYING, PAUSED, NEXTLEVEL, WIN , GAMEOVER };
    public GameState currGameState = GameState.LOADING;

    // Start is called before the first frame update
    void Start()
    {
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

        Time.timeScale = 1.0f;
        guc.SetGameState(1);
        currGameState = GameState.PLAYING;
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

    }

    public void GoToMainMenu()
    {

    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadNextLevel()
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
