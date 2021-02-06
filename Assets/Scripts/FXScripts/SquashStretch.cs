using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashStretch : MonoBehaviour
{
    public bool xScale;
    public bool yScale;
    public bool zScale;

    public Vector3 scaleStart;
    Vector3 scaleStartNormalised;
    Vector3 originalScale;

    public float easeTime;
    float currTime;

    Transform squashStretchTransform;

    // Start is called before the first frame update
    void Start()
    {
        //endScale = transform.localScale;

        //Check for SpriteRenderer
        if (GetComponent<SpriteRenderer>())
        {
            squashStretchTransform = transform;
        }

        else
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<SpriteRenderer>())
                {
                    squashStretchTransform = child;
                    break;
                }
            }
        }

        if(squashStretchTransform == null)
        {
            squashStretchTransform = transform;
        }

        originalScale = squashStretchTransform.lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
        scaleStartNormalised = new Vector3(squashStretchTransform.lossyScale.x * scaleStart.x, squashStretchTransform.lossyScale.y * scaleStart.y, squashStretchTransform.lossyScale.z * scaleStart.z);

        if (currTime < easeTime)
        {
            currTime += Time.deltaTime;

            if(currTime > easeTime)
            {
                currTime = easeTime;
            }

            Vector3 newScale = originalScale;

            if(xScale)
            {
                newScale.x = Mathf.LerpUnclamped(scaleStartNormalised.x, originalScale.x, EasingFunction.EaseOutElastic(0, 1, currTime / easeTime));
            }

            if(yScale)
            {
                newScale.y = Mathf.LerpUnclamped(scaleStartNormalised.y, originalScale.y, EasingFunction.EaseOutElastic(0, 1, currTime / easeTime));
            }

            if(zScale)
            {
                newScale.z = Mathf.LerpUnclamped(scaleStartNormalised.z, originalScale.z, EasingFunction.EaseOutElastic(0, 1, currTime / easeTime));
            }

            squashStretchTransform.localScale = newScale;
        }
    }

    /// <summary>
    /// Call this when u wanna do tha thing
    /// </summary>
    public void StartSqushStretch()
    {
        currTime = 0;
    }

    /// <param name="startScale">Scaling to start from</param>
    public void SetValues(Vector3 startScale)
    {
        scaleStart = startScale;
    }

    public void SetValues(float x, float y, float z)
    {
        scaleStart.x = x;
        scaleStart.y = y;
        scaleStart.z = z;
    }

    public void SetBools(bool scaleX, bool scaleY, bool scaleZ)
    {
        xScale = scaleX;
        yScale = scaleY;
        zScale = scaleZ;
    }
}