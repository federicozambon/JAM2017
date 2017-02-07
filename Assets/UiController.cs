using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiController : MonoBehaviour
{
    public Slider timeSlider;
    float timer = 0f;
    float timeToAnswer = 3f;

	IEnumerator PlayerChoice()
    {
        while (timer <= timeToAnswer)
        {
            timer += Time.deltaTime;
            timeSlider.value -= timer / timeToAnswer;
            yield return null;
        }
        
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PlayerChoice());
        }
	}
}
