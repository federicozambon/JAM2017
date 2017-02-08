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
    public string firstTeam;
    public string matchResult;
    public string secondTeam;
    public string minute;
    public string half;
    public bool[] frameList;
    public GameObject[] frameGo;
}