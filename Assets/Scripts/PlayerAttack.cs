using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;

public class PlayerAttack : MonoBehaviour
{

    public int layermask;

    public enum AttackBoxArea
    {
        HIGH,
        LOW,
        NEAR,
        FAR,
        REALLYFAR
    }

    //testing
    public float forceMultiplier = 50.0f;

    public IntVal playerHealth;

    public List<char> inputKeys;
    float inputHoldTime;

    public Vector2 jabForce;
    public Vector2 farJabForce;
    public Vector2 reallyFarJabForce;
    public Vector2 uppercutForce;
    public Vector2 diveForce;

    public Transform atkBoxNear;
    public Transform atkBoxFar;
    public Transform atkBoxHigh;
    public Transform atkBoxDown;

    public Transform atkBoxNearR;
    public Transform atkBoxFarR;
    public Transform atkBoxHighR;
    public Transform atkBoxDownR;

    public bool facingRight = false;

    public Transform ballProjectile;
    public float ballForce;

    Rigidbody2D rigidbody2d;
    public float groundDetect;
    public SpriteRenderer sprite;

    float attackCD = 0f;

    float gettingHitCD;
    Coroutine currCoroutine = null;

    public Animator animator;

    public bool inAir;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        layermask = (1 << 0);   //Player
        //layermask = (1 << 9);   //Player


       // layermask = ~layermask;

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
            sprite.flipX = facingRight;
            if (rigidbody2d.gravityScale < 10f) rigidbody2d.gravityScale += .1f;
        

        gettingHitCD -= Time.deltaTime;
    }


    void LowInput()
    {
        string input = InputToString();
        attackCD = 0.1f;

        switch (input)
        {
            case ("8"): 
                if(!inAir)  
                    StartCoroutine(Action(uppercutForce , 0.0f, AttackBoxArea.HIGH, 1f));
                break;

            case ("6"):        
            case ("4"):             
                StartCoroutine(Action(farJabForce , 0.25f, AttackBoxArea.FAR, .5f)); 
                break;

            case ("66"):    
            case ("44"):            
                StartCoroutine(Action(reallyFarJabForce , .6f, AttackBoxArea.REALLYFAR, 1f)); 
                attackCD += 0.3f;
                break;

            case ("2"): if (inAir)  
                    StartCoroutine(Action(diveForce , 0.1f, AttackBoxArea.LOW, .5f)); 
                attackCD +=0.3f ; 
                break;


            case ("236"):            
                Hadouken(true) ;
                break;
            case ("214"):            
                Hadouken(false) ; 
                break;

            default:                
                StartCoroutine(Action(jabForce, 0.0f, AttackBoxArea.NEAR, 0.2f)); 
                break;
        }
        PlayAttackSound();
    }
    


    IEnumerator Action(Vector2 force, float delay, AttackBoxArea atkBoxArea, float boxDuration)
    {
        rigidbody2d.velocity = new Vector2();
        rigidbody2d.gravityScale = 0.0f;
        yield return new WaitForSeconds(delay);


        Transform spawnBox;

        switch (atkBoxArea)
        {
            case (AttackBoxArea.FAR):
                {
                    if (!facingRight) spawnBox = atkBoxFar;
                    else                spawnBox = atkBoxFarR;
                    animator.Play("farJab");

                    break;
                }
            case (AttackBoxArea.REALLYFAR):
                {
                    if (!facingRight) spawnBox = atkBoxFar;
                    else spawnBox = atkBoxFarR;
                    animator.Play("diveKick");

                    break;
                }
            case (AttackBoxArea.LOW):
                {
                    if (!facingRight)
                    { spawnBox = atkBoxDown; animator.Play("dropKick"); }


                    else { spawnBox = atkBoxDownR; animator.Play("dropKickR"); }

                    break;
                }
            case (AttackBoxArea.NEAR):
                {
                    if (!facingRight) spawnBox = atkBoxNear;
                    else                spawnBox = atkBoxNearR;
                    animator.Play("shortJap");

                    break;
                }
            case (AttackBoxArea.HIGH):
                {
                    if (!facingRight) spawnBox = atkBoxHigh;
                    else                spawnBox = atkBoxHighR;
                    animator.Play("upperCut");
                    break;
                }

                   
            default: spawnBox = atkBoxNear; animator.Play("shortJap"); break;
        }


        if (!facingRight)
        {
            force *= new Vector2(-1f, 1f);
        }
        rigidbody2d.velocity = force * forceMultiplier;

        Transform tempBox = Instantiate(spawnBox, spawnBox.position ,spawnBox.rotation, this.transform);
        tempBox.gameObject.SetActive(true);
        Destroy(tempBox.gameObject, boxDuration);

        


        yield return 0;
    }


    void Hadouken(bool facingRight)
    {
        float force = ballForce;


        Rigidbody2D rigid2D = Instantiate(ballProjectile, transform).GetComponent<Rigidbody2D>();
        if (!facingRight)
        {
            force *= -1f;
            rigid2D.GetComponent<SpriteRenderer>().flipX = true;
        }
        rigid2D.AddForce(new Vector2(force, 0.0f), ForceMode2D.Impulse);
        animator.Play("hado");
        Destroy(rigid2D.gameObject, 3f);
    }


    void KeyInput()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !inputKeys.Contains('8'))
        {
            inputKeys.Add('8');
            inputHoldTime = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (inputKeys.Contains('2')) inputKeys.Add('3');
            else                         inputKeys.Add('6');

            facingRight = true;
            inputHoldTime = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            if (inputKeys.Contains('2')) inputKeys.Add('1');
            else                         inputKeys.Add('4');

            facingRight = false;
            inputHoldTime = 0.5f;
        }

        if (Input.GetKey(KeyCode.DownArrow) && !inputKeys.Contains('2'))
        {
            inputKeys.Add('2');
            inputHoldTime = 0.5f;
        }

        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (inputKeys.Contains('3')) inputKeys.Add('6');
            else if (inputKeys.Contains('1')) inputKeys.Add('4');
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
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + 0.36f, transform.position.y), Vector2.down, groundDetect, layermask);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x - 0.36f, transform.position.y), Vector2.down, groundDetect, layermask);

        if (hit || hit2)
        {
            inAir = false;
            Debug.DrawLine(new Vector2(transform.position.x + 0.36f, transform.position.y), hit.point, Color.cyan);
            Debug.DrawLine(new Vector2(transform.position.x - 0.36f, transform.position.y), hit2.point, Color.cyan);
            
        }

        else
            inAir = true;
    }

    public void ReceiveDamage()
    {
        gettingHitCD = 3f;
        if (currCoroutine != null)
            StopCoroutine(currCoroutine);
        currCoroutine = StartCoroutine(DamageFlicker());
        --GameUIController.instance.playerHealth.value;
        GameUIController.instance.UpdateHealth();
    }



    IEnumerator DamageFlicker()
    {
        sprite.color = Color.red;
        yield return new WaitForFixedUpdate();
        sprite.color = Color.white;
        yield return new WaitForFixedUpdate();
        sprite.color = Color.black;
        yield return new WaitForFixedUpdate();
        sprite.color = Color.red;
        yield return new WaitForFixedUpdate();
        sprite.color = Color.white;
        currCoroutine = null;
        yield return new WaitForFixedUpdate();
    }

    public void PlayAttackSound()
    {
        int val = Random.Range(1, 5);
        switch (val)
        {
            case 1:
                AudioManager.PlaySound(Sounds.Attack1);
                break;

            case 2:
                AudioManager.PlaySound(Sounds.Attack2);
                break;

            case 3:
                AudioManager.PlaySound(Sounds.Attack3);
                break;

            case 4:
                AudioManager.PlaySound(Sounds.Attack4);
                break;
        }
    }
}
