using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameSessionInfo", menuName = "Game Session", order = 51)]
public class GameSessionInfo : ScriptableObject
{
    public int currLevel;
    public bool hasCleared = false;

}
