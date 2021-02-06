using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New IntVal", menuName = "Scriptable INT", order = 51)]
public class IntVal : ScriptableObject
{
    [SerializeField]
    int val;

    public int value
    {
        get { return val; }
        set { val = value; }
    }
}
