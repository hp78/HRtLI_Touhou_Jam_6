using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamagePhysics : MonoBehaviour
{
    EnemyMook enemyMook;
    Rigidbody2D rigidbody2d;
    public Vector2 Addforce;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        enemyMook = GetComponent<EnemyMook>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TakeDmg(bool right)
    {
        rigidbody2d.velocity = new Vector2();
       // Debug.Log("SHIT");
        yield return new WaitForSeconds(0.2f);
        if (right) Addforce *= new Vector2(-1f, 1f);

        rigidbody2d.AddForce(Addforce,ForceMode2D.Impulse);
        enemyMook.ReceiveDamage();
        yield return 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerAttackBox"))
        {
            Vector2 temp = collision.gameObject.transform.position - transform.position;
            bool tempb = false;
            if (temp.x > 0f) tempb = true;
            StartCoroutine( TakeDmg(tempb));
        }
    }
}
