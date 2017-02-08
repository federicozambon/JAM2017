using UnityEngine;
using System.Collections;

public class AnimationRuntimeScript : MonoBehaviour
{
    public float perspectiveMulti = 2;

    void Update()
    {
        float myY = this.transform.localPosition.y + 5;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.y);
        this.transform.localScale = new Vector3(perspectiveMulti / myY, perspectiveMulti / myY, 5);
    }
}
