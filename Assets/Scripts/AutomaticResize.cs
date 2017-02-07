using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class AutomaticResize : MonoBehaviour
{
    public float perspectiveMulti;
    [ExecuteInEditMode]
	void Update ()
    {
        float myY = this.transform.localPosition.y+5;
        this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.y);
        this.transform.localScale = new Vector3(perspectiveMulti/ myY, perspectiveMulti/ myY, 5); 
	}
}
