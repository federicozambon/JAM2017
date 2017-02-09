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

    public string startingAction;
    int currentPlayerScore = 0;
    public int currentAction = -1;
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
        currentGameObject = GameObject.Find(startingAction);
        
        currentGameObject.transform.FindChild("Frame1").gameObject.SetActive(true);
        StartCoroutine(NextAction());
    }

    IEnumerator ShowFrame()
    {
        if (!mM.aS.isPlaying)
        {
            mM.PlayActionMusic();
        }
       
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

    Coroutine answered;

    public void CheckAnswer(bool hasPlayerWhistled)
    {
        if (answered == null)
        {
            answered = StartCoroutine(CheckAnswerCo(hasPlayerWhistled));
        }   
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
        answered = null;      
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
            StartCoroutine(NextAction());
        }
	}

    IEnumerator NextAction()
    {
        ui.intermezzoCo = StartCoroutine(ui.Intermezzo());

        while (ui.intermezzoCo != null)
        {
            yield return null;
        }     

        if (currentAction < 9)
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
            ui.intermezzo.SetActive(false);
            myCoroutine = StartCoroutine(ShowFrame());
        }
        else
        {
            SceneManager.LoadScene("Score");
        }
    }

    void CorrectAnswer(bool isFault)
    {
        mM.PlayFeedbackMusic(1);
        currentPlayerScore++;
        mM.score++;
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
            Debug.Log("foundcircle");
            currentGameObject.transform.FindChild("Circle").gameObject.SetActive(true);
        }
        if (currentGameObject.transform.FindChild("Line"))
        {
            Debug.Log("foundline");
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
