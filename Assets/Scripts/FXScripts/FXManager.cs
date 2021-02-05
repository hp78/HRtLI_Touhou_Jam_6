using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    private static FXManager _instance;
    SquashStretch squashStretchfx;

    public static FXManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            print("Additional FXManager Found, Deleting this");
            Destroy(gameObject);
        }

        else
        {
            _instance = this;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        squashStretchfx = GetComponent<SquashStretch>();
    }

    public void SSFX()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
