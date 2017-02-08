using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    UiController ui;
    ActionList aList;
    MusicManager mM;

    GameObject currentGameObject;
    Coroutine myCoroutine;
    public Coroutine myTimerCo;

    int currentPlayerScore = 0;
    int currentAction = -1;
    int currentFrame = -1;
    public float animationTime = 1;
    public bool activeAnimation;

    void Awake()
    {
        mM = FindObjectOfType<MusicManager>();
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
        mM.PlayActionMusic();

        foreach (var player in aList.actionList[currentAction].frameGo[currentFrame].GetComponentsInChildren<AnimationScript>(true))
        {
            StartCoroutine(player.AnimationHandler());
            if (player.toAnimate)
            {
                player.toAnimateReplay = true;
                player.toAnimate = false;
            }
        }
    
        yield return new WaitForSeconds(animationTime);
        if (!aList.actionList[currentAction].sceneToSkip[currentFrame])
        {
            myTimerCo = StartCoroutine(ui.Timer());
        }
        else
        {
            NextFrame();
        }
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
            ui.firstTeam.text = aList.actionList[currentAction].firstTeam;
            ui.secondTeam.text = aList.actionList[currentAction].secondTeam;
            ui.result.text = aList.actionList[currentAction].matchResult;
            ui.minute.text = aList.actionList[currentAction].minute;
            ui.half.text = aList.actionList[currentAction].half;
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
        mM.PlayFeedbackMusic(1);
        currentPlayerScore++;
        ui.currentPlayerScore.text = "Player Score: " + currentPlayerScore.ToString();
        if (isFault)
        {
            StartCoroutine(FaultFound());
            StartCoroutine(ShowFault()); 
                  
        }
        else
        {
            StartCoroutine(ShowNoFaultCorrect());
        }
    }

    void WrongAnswer(bool isFault)
    {
        mM.PlayFeedbackMusic(0);
        if (isFault)
        {
            StartCoroutine(ShowNoFaultWrong());
        }
        else
        {
            StartCoroutine(FaultMissed());
            StartCoroutine(ShowFault());
        }
    }

    IEnumerator FaultFound()
    {
        ui.faultFound.transform.parent.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        ui.faultFound.transform.parent.transform.parent.gameObject.SetActive(false);
    }

    IEnumerator FaultMissed()
    {
        ui.faultMissed.transform.parent.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        ui.faultMissed.transform.parent.transform.parent.gameObject.SetActive(false);
    }

    IEnumerator ShowFault()
    {
        if (currentGameObject.transform.FindChild("Circle"))
        {
            currentGameObject.transform.FindChild("Circle").gameObject.SetActive(true);
        }
        if (currentGameObject.transform.FindChild("Line"))
        {
            currentGameObject.transform.FindChild("Line").gameObject.SetActive(true);
        }

        foreach (var player in aList.actionList[currentAction].frameGo[currentFrame].GetComponentsInChildren<AnimationScript>(true))
        {
            StartCoroutine(player.GetComponent<AnimationScript>().ReplayHandler());
        }

   
        yield return new WaitForSeconds(3f);
        NextFrame();
    }

    IEnumerator ShowNoFaultCorrect()
    {
        ui.allRight.transform.parent.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        ui.allRight.transform.parent.transform.parent.gameObject.SetActive(false);
        NextFrame();
    }

    IEnumerator ShowNoFaultWrong()
    {
        ui.noFault.transform.parent.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        ui.noFault.transform.parent.transform.parent.gameObject.SetActive(false);
        NextFrame();
    }
}
