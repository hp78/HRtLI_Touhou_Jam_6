using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMook : MonoBehaviour
{
    public enum EnemyState { IDLE, ACTIVE, DEAD }
    public EnemyState currEnemyState = EnemyState.IDLE;
    public int enemyHealth = 3;
    public bool isMelee = true;
    public Animator animator;

    public GameObject deathFX;

    delegate void MookBehaviour();
    MookBehaviour mookBehaviour;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (currEnemyState == EnemyState.IDLE)
        {
            // if player nearby go active
        }
        else if (currEnemyState == EnemyState.ACTIVE)
        {
            mookBehaviour();
        }
    }

    void MeleeBehaviour()
    {
        // melee attack every x seconds
    }

    void RangeBehaviour()
    {
        // range attack every x seconds
    }

    public void ReceiveDamage(int val = 1)
    {
        --enemyHealth;

        if (enemyHealth < 1)
        {
            currEnemyState = EnemyState.DEAD;
            animator.SetInteger("EnemyState", 3);
            gameObject.SetActive(false);
            Instantiate(deathFX, transform.position, Quaternion.identity);
        }
    }
}
