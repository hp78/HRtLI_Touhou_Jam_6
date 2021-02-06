using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeMook : MonoBehaviour
{
    public int enemyHealth = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public void ReceiveDamage(int val = 1)
    {
        --enemyHealth;
    }
}
