﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;
public class EnemyMook : MonoBehaviour
{
    public enum EnemyState { IDLE, ACTIVE, DEAD }
    public EnemyState currEnemyState = EnemyState.IDLE;
    public int enemyHealth = 3;
    public bool isMelee = true;
    public bool isMove = false;

    public Animator animator;
    public SpriteRenderer spriteRender;

    public GameObject deathFX;

    Coroutine currCoroutine = null;

    delegate void MookBehaviour();
    MookBehaviour mookBehaviour;

    public Animator attackAnimation;
    public float atkCD;
    public float internalAtkCD;

    public float moveCD;
    public float internalMoveCD;
    public float moveForce;

    public Transform enemyProjectile;
    public Transform projectileSpawnPos;
    public Vector2 projectileSpeed;

    public Transform player;
    public IntVal playerCombo;

    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        if (isMelee)
        {
            mookBehaviour = MeleeBehaviour;
        }
        else
        {
            mookBehaviour = RangeBehaviour;
        }

        rigidbody2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (currEnemyState == EnemyState.IDLE)
        {
            if (Vector3.Magnitude(player.position - this.transform.position) < 5f) currEnemyState = EnemyState.ACTIVE;
        }
        else if (currEnemyState == EnemyState.ACTIVE)
        {
            mookBehaviour();
            internalAtkCD -= Time.deltaTime;
            internalMoveCD -= Time.deltaTime;
        }

    }

    void MeleeBehaviour()
    {
       if(isMove)
        {
            if (Vector3.Magnitude(player.position - this.transform.position) > .5f && internalMoveCD < 0.0f)
            {
                rigidbody2d.velocity= new Vector2(moveForce, 2f);
                internalMoveCD = moveCD;
            }   
            else if((Vector3.Magnitude(player.position - this.transform.position) <= 1f && internalAtkCD < 0.0f) )
                {
                attackAnimation.Play("SwingSword");
                internalAtkCD = atkCD;
            }
        }
       else if(internalAtkCD < 0.0f)
        {
            attackAnimation.Play("SwingSword");
            internalAtkCD = atkCD;
        }
    }

    

    void RangeBehaviour()
    {
        if (internalAtkCD < 0.0f)
        {
            Rigidbody2D temp = Instantiate(enemyProjectile, projectileSpawnPos.position, enemyProjectile.rotation).GetComponent<Rigidbody2D>();
            temp.gameObject.SetActive(true);
            temp.velocity = projectileSpeed;
            Destroy(temp.gameObject, 5f);
            attackAnimation.Play("GunFire");
            internalAtkCD = atkCD;
            PlayShootSound();
        }
    }

    public void ReceiveDamage(int val = 1)
    {
        --enemyHealth;
        PlayDamagedSound();

        ++playerCombo.value;
        GameUIController.instance.UpdateComboCounter();

        if (enemyHealth < 1)
        {
            currEnemyState = EnemyState.DEAD;
            //animator.SetInteger("EnemyState", 3);
            gameObject.SetActive(false);
            Instantiate(deathFX, transform.position, Quaternion.identity);
        }
        else
        {
            if (currCoroutine != null)
                StopCoroutine(currCoroutine);
            currCoroutine = StartCoroutine(DamageFlicker());

            internalAtkCD = 2f;
        }
    }

    void Test()
    {
        ReceiveDamage();
    }

    IEnumerator DamageFlicker()
    {
        spriteRender.color = Color.red;
        yield return new WaitForFixedUpdate();
        spriteRender.color = Color.white;
        yield return new WaitForFixedUpdate();
        spriteRender.color = Color.black;
        yield return new WaitForFixedUpdate();
        spriteRender.color = Color.red;
        yield return new WaitForFixedUpdate();
        spriteRender.color = Color.white;
        currCoroutine = null;
        yield return new WaitForFixedUpdate();
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
}
