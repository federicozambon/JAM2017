using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour
{
    public Vector3 startTransform;
    public Vector3 startRot;
    public Transform endTransform;
    GameController gc;
    public bool toAnimate, toAnimateReplay;

    void Awake()
    {
        gc = FindObjectOfType<GameController>();  
    }

    public void ResetPositions()
    {
        if (tag != "CameraZoom")
        {
            this.transform.position = startTransform;
            this.transform.eulerAngles = startRot;
        }
    }

    public IEnumerator AnimationHandler()
    {
        startTransform = this.transform.position;
        startRot = this.transform.eulerAngles;

        if (toAnimate)
        {
            float timer = 0;

            while (timer <= gc.animationTime)
            {
                timer += Time.deltaTime;
                this.transform.eulerAngles = Vector3.Lerp(startTransform, endTransform.eulerAngles, timer);
                this.transform.position = Vector3.Lerp(startTransform, endTransform.position, timer);
                yield return null;
            }
        }
    }

    public IEnumerator ReplayHandler()
    {
        ResetPositions();
        if (toAnimateReplay)
        {
            float timer = 0;

            while (timer <= gc.animationTime*2)
            {
                startTransform = new Vector3(startTransform.x, startTransform.y, -1);
                endTransform.position = new Vector3(endTransform.position.x, endTransform.position.y, -1);
                timer += Time.deltaTime;
                this.transform.eulerAngles = Vector3.Lerp(startTransform, endTransform.eulerAngles, timer/2);
                this.transform.position = Vector3.Lerp(startTransform, endTransform.position, timer/2);
                yield return null;
            }
        }
    }
}
