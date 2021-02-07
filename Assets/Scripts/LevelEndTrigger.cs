using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelEndTrigger : MonoBehaviour
{
    //
    public GameStateManager gsm;
    public Animator rocketAnimator;
    public string varName;

    //
    [Space(5)]
    public bool isEndingScene = false;
    public CinemachineVirtualCamera cvc;
    public GameObject playerGO;

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ChangeLevel();
        }
    }

    void ChangeLevel()
    {
        gsm.GoToNextLevel();
    }
}
