using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public int layermask;


    public List<char> inputKeys;
    float inputHoldTime;

    public Vector2 jabForce;
    public Vector2 farJabForce;
    public Vector2 reallyFarJabForce;
    public Vector2 uppercutForce;
    public Vector2 diveForce;

    public bool facingRight = false;

    Rigidbody2D rigidbody2d;
    Collider2D atkboxMid;
    Collider2D atkboxHigh;

    float attackCD = 0f;
    float atkboxDuration = 0f;

    bool inAir;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        layermask = (1 << 8);   //Player
        layermask = ~layermask;

    }

    // Update is called once per frame
    void Update()
    {
        if (attackCD < 0f)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                LowInput();
            }
        }
        KeyInput();
        CheckIfInAir();
        attackCD -= Time.deltaTime;
    }


    void LowInput()
    {
        string input = InputToString();
        attackCD = 0.1f;

        switch (input)
        {
            case ("8"): if(!inAir)  StartCoroutine(Action(uppercutForce, 0.0f));break;

            case ("6"):        
            case ("4"):             StartCoroutine(Action(farJabForce, 0.25f)); break;

            case ("66"):    
            case ("44"):            StartCoroutine(Action(reallyFarJabForce, .6f)); attackCD += 0.3f; break;

            case ("2"): if (inAir)  StartCoroutine(Action(diveForce, 0.1f)); attackCD +=0.3f ; break;

            default:                StartCoroutine(Action(jabForce, 0.0f)); break;
        }

    }
    


    IEnumerator Action(Vector2 force, float delay)
    {
        rigidbody2d.velocity = new Vector2();
        rigidbody2d.gravityScale = 0.0f;
        yield return new WaitForSeconds(delay);
        rigidbody2d.gravityScale = 1.0f;

        if (!facingRight) force *= new Vector2(-1f, 1f);
        rigidbody2d.velocity = force;
        yield return 0;
    }



    void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !inputKeys.Contains('8'))
        {
            inputKeys.Add('8');
            inputHoldTime = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            inputKeys.Add('6');
            facingRight = true;
            inputHoldTime = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            inputKeys.Add('4');
            facingRight = false;
            inputHoldTime = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !inputKeys.Contains('2'))
        {
            inputKeys.Add('2');
            inputHoldTime = 0.5f;
        }

        inputHoldTime -= Time.deltaTime;
        if (inputHoldTime < 0.0f) inputKeys.Clear();
    }


    string InputToString()
    {
        string result = "";
        foreach (char c in inputKeys) result = result + c;
        inputKeys.Clear();
        return result;
    }

    void CheckIfInAir()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + 0.36f, transform.position.y), Vector2.down, .6f, layermask);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x - 0.36f, transform.position.y), Vector2.down, .6f, layermask);

        if (hit || hit2)
        {
            inAir = false;
            Debug.DrawLine(new Vector2(transform.position.x + 0.36f, transform.position.y), hit.point, Color.cyan);
            Debug.DrawLine(new Vector2(transform.position.x - 0.36f, transform.position.y), hit2.point, Color.cyan);
        }

        else
            inAir = true;

    }
}
