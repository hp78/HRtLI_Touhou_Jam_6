using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    //
    public static GameUIController instance;

    //
    public GameStateManager gsm;

    //
    [Space(5)]
    public GameObject[] playerHealths;
    public IntVal playerHealth;

    [Space(5)]
    public GameObject comboTextObj;
    public Text comboCountObj;
    public IntVal playerCombo;

    //
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        if (gsm == null)
            gsm = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
        //animator = GetComponent<Animator>();
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

    public void UpdateHealth()
    {
        for(int i = 0; i < playerHealth.value && i < 5; ++i)
        {
            playerHealths[i].SetActive(true);
        }
        for(int i = playerHealth.value; i < 5; ++i)
        {
            playerHealths[i].SetActive(false);
        }
    }

    public void UpdateComboCounter()
    {
        comboCountObj.text = playerCombo.value.ToString();
}
}
