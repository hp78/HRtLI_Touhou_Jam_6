using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingFX : MonoBehaviour
{
    //Requires a SpriteRenderer
    SpriteRenderer SR;

    public float blinkingTime;
    public float blinkFrequency;
    float currTime;
    Coroutine blinkRoutine;

    // Start is called before the first frame update
    void Start()
    {
        //Check for SpriteRenderer
        if (GetComponent<SpriteRenderer>())
        {
            SR = GetComponent<SpriteRenderer>();
        }

        else
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<SpriteRenderer>())
                {
                    SR = (child.gameObject.GetComponent<SpriteRenderer>());
                    break;
                }
            }
        }

        if(SR == null)
        {
            print("SpriteRenderer not found. Requires a SpriteRenderer, deleting");
            Destroy(this);
        }
    } 

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            StartBlink();
        }

        if(currTime < blinkingTime)
            currTime += Time.deltaTime;
    }

    public void StartBlink()
    {
        currTime = 0;
        StopCoroutine(BlinkToggle());
        StartCoroutine(BlinkToggle());
    }

    IEnumerator BlinkToggle()
    {
        while(currTime < blinkingTime)
        {
            if (SR.enabled)
                SR.enabled = false;

            else
                SR.enabled = true;

            yield return new WaitForSeconds(blinkFrequency);
        }

        SR.enabled = true;

    }
}
