using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FinalLevelEndTrigger : MonoBehaviour
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
    public Transform rocketTF;

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        rocketAnimator.SetBool(varName, true);
        if(isEndingScene)
        {
            playerGO.SetActive(false);
            cvc.Follow = rocketTF;
            Invoke("ChangeLevel", 7.5f);
        }
    }

    void ChangeLevel()
    {
        gsm.GoToNextLevel();
    }
}
