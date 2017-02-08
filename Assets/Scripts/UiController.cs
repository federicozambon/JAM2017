using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiController : MonoBehaviour
{
    GameController gc;
    public Slider timeSlider;
    public float timeToAnswer = 2f;
    float timer = 0f;
    public Text allRight;
    public Text noFault;
    public Text faultFound;
    public Text faultMissed;
    public Text firstTeam;
    public Text secondTeam;
    public Text result;
    public Text minute;
    public Text half;
    public Text currentPlayerScore;
    public Button whistleBtn;
    public GameObject replay;

    void Awake()
    {
        allRight.text = "Tutto regolare";
        noFault.text = "Cosa hai fischiato?";
        gc = FindObjectOfType<GameController>();
    }

    public bool isPushed;

    public void PushedButton()
    {
        isPushed = true;
    }

	public IEnumerator Timer()
    {
        isPushed = false;
        timer = 0;
        timeSlider.value = 1;
        while (timeSlider.value > 0f)
        {
            timer += Time.deltaTime;
            timeSlider.value = Mathf.Lerp(1,0, timer/timeToAnswer);
            yield return null;
        }
        gc.myTimerCo = null;

        if (!isPushed)
        {
            gc.CheckAnswer(false);
        }

	}
	
	void Update ()
    {
        if (gc.myTimerCo != null)
        {
            timeSlider.gameObject.SetActive(true);
            whistleBtn.gameObject.SetActive(true);
        }
        else
        {
            timeSlider.gameObject.SetActive(false);
            whistleBtn.gameObject.SetActive(false);
        }
	}
}
