using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public Animator animator;
    public Sprite bossSprite;

    public BossMapBehaviour bmb;

    public IntVal comboCount;
    bool isHeads = false;
    float countdown = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0.0f)
        {
            countdown = Random.Range(1.5f, 5.5f);
            isHeads = !isHeads;
            animator.SetBool("Heads", isHeads);
        }
    }

    public void BoomAt(int index)
    {
        bmb.BoomAtSpot(index);
    }

    public void ReceiveDamage()
    {
        if(comboCount.value > 99)
        {
            // win
        }
    }
}
