using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour
{
    public Vector3 startTransform;
    public Transform endTransform;
    GameController gc;
    public bool toAnimate, toAnimateReplay;

    void Awake()
    {
        gc = FindObjectOfType<GameController>();
        startTransform = this.transform.position;
    }

    public IEnumerator AnimationHandler()
    {
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
        if (toAnimateReplay)
        {
            float timer = 0;

            while (timer <= gc.animationTime)
            {
                startTransform = new Vector3(startTransform.x, startTransform.y, -1);
                endTransform.position = new Vector3(endTransform.position.x, endTransform.position.y, -1);
                timer += Time.deltaTime;
                this.transform.eulerAngles = Vector3.Lerp(startTransform, endTransform.eulerAngles, timer);
                this.transform.position = Vector3.Lerp(startTransform, endTransform.position, timer);
                yield return null;
            }
        }
    }
}
