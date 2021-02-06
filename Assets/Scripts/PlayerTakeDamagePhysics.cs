using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamagePhysics : MonoBehaviour
{

    PlayerAttack playerAtkScript;

    Rigidbody2D rigidbody2d;
    public Vector2 addforce;
    public float invulCD;
    float internalCD;
    // Start is called before the first frame update
    void Start()
    {
        playerAtkScript = GetComponent<PlayerAttack>();
        rigidbody2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        internalCD -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttackBox") && internalCD < 0.0f)
        {
            Vector2 temp = collision.gameObject.transform.position - transform.position;
            bool tempb = false;
            if (temp.x > 0f) tempb = true;
            StartCoroutine(TakeDmg(tempb));
        }
    }

    IEnumerator TakeDmg(bool right)
    {
        rigidbody2d.velocity = new Vector2();
        Debug.Log("SHIT");
        yield return new WaitForSeconds(0.2f);
        if (right) addforce *= new Vector2(-1f, 1f);
        rigidbody2d.AddForce(addforce, ForceMode2D.Impulse);
        playerAtkScript.ReceiveDamage();
        internalCD = invulCD;
        yield return 0;
    }
}
