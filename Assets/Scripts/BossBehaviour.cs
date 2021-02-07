using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer bossSprite;

    public BossMapBehaviour bmb;

    public IntVal comboCount;
    bool isHeads = false;
    float countdown = 1.0f;

    Coroutine currCoroutine = null;

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
        ++comboCount.value;

        if (comboCount.value > 99)
        {
            // win
        }
        else
        {
            if (currCoroutine != null)
                StopCoroutine(currCoroutine);
            currCoroutine = StartCoroutine(DamageFlicker());
        }

        GameUIController.instance.UpdateComboCounter();
    }


    IEnumerator DamageFlicker()
    {
        bossSprite.color = Color.red;
        yield return new WaitForFixedUpdate();
        bossSprite.color = Color.white;
        yield return new WaitForFixedUpdate();
        bossSprite.color = Color.black;
        yield return new WaitForFixedUpdate();
        bossSprite.color = Color.red;
        yield return new WaitForFixedUpdate();
        bossSprite.color = Color.white;
        currCoroutine = null;
        yield return new WaitForFixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttackBox"))
        {
            ReceiveDamage();
        }
    }
}
