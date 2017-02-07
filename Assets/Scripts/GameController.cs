using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    UiController ui;
    ActionList aList;

    GameObject currentGameObject;
    Coroutine myCoroutine;
    public Coroutine myTimerCo;

    int currentPlayerScore = 0;
    int currentAction = -1;
    int currentFrame = -1;

    void Awake()
    {
        ui = FindObjectOfType<UiController>();
        aList = GetComponent<ActionList>();
    }

    void Start()
    {
        currentGameObject = GameObject.Find("Action1");
        NextAction();
    }

    IEnumerator ShowFrame()
    {
        yield return new WaitForSeconds(1f);
        myTimerCo = StartCoroutine(ui.Timer());
    }

    public void CheckAnswer(bool hasPlayerWhistled)
    {
        StartCoroutine(CheckAnswerCo(hasPlayerWhistled));
    }

	public IEnumerator CheckAnswerCo(bool hasPlayerWhistled)
    {
        while (myTimerCo != null)
        {
            yield return null;
        }

        if (hasPlayerWhistled)
        {
            if (aList.actionList[currentAction].frameList[currentFrame])
            {
                CorrectAnswer(true);
            }
            else
            {
                WrongAnswer(true);
            }
        }
        else
        {
            Debug.Log(currentAction + "    " + currentFrame);
            if (!aList.actionList[currentAction].frameList[currentFrame])
            {
                CorrectAnswer(false);
            }
            else
            {
                WrongAnswer(false);
            }
        }
	}
	
	void NextFrame()
    {
        if (currentFrame < aList.actionList[currentAction].frameList.Length-1)
        {
            currentFrame++;
            currentGameObject.SetActive(false);
            currentGameObject = aList.actionList[currentAction].frameGo[currentFrame];
            currentGameObject.SetActive(true);
            myCoroutine = StartCoroutine(ShowFrame());
        }
        else
        {
            NextAction();
        }
	}

    void NextAction()
    {
        if (currentAction < aList.actionList.Length-1)
        {        
            currentAction++;
            if (currentAction != 0)
            {
                currentGameObject.SetActive(false);
            }
            currentGameObject = aList.actionList[currentAction].frameGo[0];
            currentFrame = 0;
            ui.timeAndHalf.text = aList.actionList[currentAction].minuteAndHalf;
            ui.matchResult.text = aList.actionList[currentAction].matchResult;
            currentGameObject.SetActive(true);
            myCoroutine = StartCoroutine(ShowFrame());
        }
        else
        {
            //LOADSCORESCENE
        }
    }

    void CorrectAnswer(bool isFault)
    {
        currentPlayerScore++;
        ui.currentPlayerScore.text = "Player Score: " + currentPlayerScore.ToString();
        if (isFault)
        {
            StartCoroutine(ShowFault());
            //PlayCorrectSfx();
        }
        else
        {
            StartCoroutine(ShowNoFaultCorrect());
            //PlayCorrectSfx();
        }
    }

    void WrongAnswer(bool isFault)
    {
        if (isFault)
        {
            StartCoroutine(ShowNoFaultWrong());
            //PlayWrongSfx();
        }
        else
        {
            StartCoroutine(ShowFault());
        }
    }

    IEnumerator ShowFault()
    {
        currentGameObject.transform.FindChild("Circle").gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        NextFrame();
    }

    IEnumerator ShowNoFaultCorrect()
    {
        ui.allRight.SetActive(true);
        yield return new WaitForSeconds(3f);
        ui.allRight.SetActive(false);
        NextFrame();
    }

    IEnumerator ShowNoFaultWrong()
    {
        ui.noFault.SetActive(true);
        yield return new WaitForSeconds(3f);
        ui.noFault.SetActive(false);
        NextFrame();
    }
}
