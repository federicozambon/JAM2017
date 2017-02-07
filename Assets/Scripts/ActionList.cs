using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ActionList : MonoBehaviour
{
    public SceneVariable[] actionList;
}

[System.Serializable]
public class SceneVariable
{
    public SpriteRenderer[] sr;
    public string minuteAndHalf;
    public string matchResult;
    public bool[] frameList;
    public GameObject[] frameGo;
}