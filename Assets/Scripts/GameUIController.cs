using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    //
    public GameObject[] playerHealths;

    public GameObject comboTextObj;
    public Text comboCountObj;

    //
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
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
}
