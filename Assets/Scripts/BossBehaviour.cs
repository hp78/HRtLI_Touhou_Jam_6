using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;

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
        AudioManager.PlaySound(Sounds.Boom);
    }

    public void PlayShootSound()
    {
        int val = Random.Range(1, 3);
        switch (val)
        {
            case 1:
                AudioManager.PlaySound(Sounds.Pistol1);
                break;

            case 2:
                AudioManager.PlaySound(Sounds.Pistol2);
                break;
        }
    }

    public void PlayDamagedSound()
    {
        int val = Random.Range(1, 5);
        switch (val)
        {
            case 1:
                AudioManager.PlaySound(Sounds.Hit1);
                break;

            case 2:
                AudioManager.PlaySound(Sounds.Hit2);
                break;

            case 3:
                AudioManager.PlaySound(Sounds.Hit3);
                break;

            case 4:
                AudioManager.PlaySound(Sounds.Hit4);
                break;
        }
    }

    public void ReceiveDamage()
    {
        ++comboCount.value;
        PlayDamagedSound();

        if (comboCount.value > 99)
        {
            GameStateManager.instance.GoToNextLevel();
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
