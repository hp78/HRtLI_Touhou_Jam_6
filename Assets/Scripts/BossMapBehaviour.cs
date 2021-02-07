using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMapBehaviour : MonoBehaviour
{
    public GameObject pfBigBoom;
    public Transform[] spots;
    public BoxCollider2D[] spotsHurtbox;

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
        StartCoroutine(SpotDamage(spotIndex));
    }

    IEnumerator SpotDamage(int spotIndex)
    {
        spotsHurtbox[spotIndex].enabled = true;
        yield return new WaitForSeconds(0.5f);
        spotsHurtbox[spotIndex].enabled = false;
    }
}
