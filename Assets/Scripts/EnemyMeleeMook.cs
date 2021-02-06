using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeMook : MonoBehaviour
{
    public int enemyHealth = 3;

    public Animator meleeAtk;
    public float atkCD;
    float internalCD;



    private void Update()
    {
        internalCD -= Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public void ReceiveDamage(int val = 1)
    {
        --enemyHealth;
    }
}
