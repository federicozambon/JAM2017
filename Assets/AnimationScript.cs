using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour
{
    private Vector3 startTransform;
    public Transform endTransform;
    GameController gc;
    public bool toAnimate;

    void Awake()
    {
        gc = FindObjectOfType<GameController>();
    }

    void Start()
    {
        startTransform = this.transform.position;
    }

    public IEnumerator AnimationHandler()
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
