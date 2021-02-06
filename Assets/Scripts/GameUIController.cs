using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    //
    public GameStateManager gsm;

    //
    public GameObject[] playerHealths;

    public GameObject comboTextObj;
    public Text comboCountObj;

    //
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (gsm == null)
            gsm = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameState(int val)
    {
        animator.SetInteger("GameState", val);
    }

    public void ResumeButton()
    {
        gsm.ResumeGame();
    }

    public void RestartButton()
    {
        gsm.RestartGame();
    }

    public void MainMenuButton()
    {
        gsm.GoToMainMenu();
    }

    public void QuitButton()
    {
        gsm.QuitGame();
    }

    public void NextLevelButton()
    {
        gsm.GoToNextLevel();
    }
}
