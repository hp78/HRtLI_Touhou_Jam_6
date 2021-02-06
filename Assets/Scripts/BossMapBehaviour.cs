using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMapBehaviour : MonoBehaviour
{
    public GameObject pfBigBoom;
    public Transform[] spots;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BoomAtSpot(int spotIndex)
    {
        Instantiate(pfBigBoom, spots[spotIndex].position, Quaternion.identity);
    }
}
