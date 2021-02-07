using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsParallax : MonoBehaviour
{
    public Transform star1;
    public Transform star2;
    public Transform star3;
    public Transform earth;

    public float offset1;
    public float offset2;
    public float offset3;
    public float earthOffset;

    // Start is called before the first frame update
    void Start()
    {
        offset1 = star1.localPosition.x;
        offset2 = star2.localPosition.x;
        offset3 = star3.localPosition.x;
        earthOffset = earth.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        AllignPosition(star1, offset1, -0.05f);
        AllignPosition(star2, offset2, -0.05f);
        AllignPosition(star3, offset3, -0.05f);
        AllignPosition(earth, earthOffset, -0.2f);
    }

    void AllignPosition(Transform star, float offset, float multiplier)
    {
        star.localPosition = new Vector3(transform.position.x * multiplier + offset, 0, 10);
    }
}
