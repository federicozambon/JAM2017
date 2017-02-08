using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour
{
    private Vector3 startTransform;
    private Vector3 startRot;
    public Transform endTransform;
    GameController gc;
    public bool toAnimate, toAnimateReplay;
    UiController ui;

    void Awake()
    {
        ui = FindObjectOfType<UiController>();
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

            gc = FindObjectOfType<GameController>();

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
            ui.replay.SetActive(true);
            ui.whistleBtn.gameObject.SetActive(false);
            ui.timeSlider.gameObject.SetActive(false);
            float timer = 0;

            while (timer <= gc.animationTime*2)
            {
                Debug.Log(this.gameObject);
                startTransform = new Vector3(startTransform.x, startTransform.y, startTransform.z);
                endTransform.position = new Vector3(endTransform.position.x, endTransform.position.y, endTransform.position.z);
                timer += Time.deltaTime;
                this.transform.eulerAngles = Vector3.Lerp(startTransform, endTransform.eulerAngles, timer/2);
                this.transform.position = Vector3.Lerp(startTransform, endTransform.position, timer/2);
                yield return null;
            }
            ui.whistleBtn.gameObject.SetActive(true);
            ui.timeSlider.gameObject.SetActive(true);
            ui.replay.SetActive(false);
        }
    }
}
