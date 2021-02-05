using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public List<char> inputKeys;
    float inputHoldTime;

    public Vector2 jabForce;
    public float jabCD;

    public Vector2 farJabForce;
    public Vector2 farJabCD;

    public Vector2 uppercutForce;
    public float upperCutCD;




    Rigidbody2D rigidbody2d;
    Collider2D atkboxMid;
    Collider2D atkboxHigh;

    float attackCD = 0f;
    float atkboxDuration = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

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
        attackCD -= Time.deltaTime;
    }


    void LowInput()
    {
        string input = InputToString();
        
        switch(input)
        {
            case ("8"):UpperCut();break;
            case ("6"):FarJab();break;
            default:LowJab();break;
        }

    }

    void LowJab()
    {
        rigidbody2d.AddForce(jabForce, ForceMode2D.Impulse);
        attackCD = 0.5f;
    }

    void FarJab()
    {
        rigidbody2d.AddForce(farJabForce, ForceMode2D.Impulse);
        attackCD = 1f;

    }

    void UpperCut()
    {
        rigidbody2d.AddForce(uppercutForce, ForceMode2D.Impulse);
        attackCD = 1f;
    }    


    void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !inputKeys.Contains('8'))
        {
            inputKeys.Add('8');
            inputHoldTime = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && !inputKeys.Contains('6'))
        {
            inputKeys.Add('6');
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
}
