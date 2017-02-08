using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    AudioSource aS;
    public AudioClip[] musicClipArray;
    public AudioClip[] feedbackClipArray;
    public AudioClip whistleClip;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

	void Start ()
    {
        aS = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	public void PlayActionMusic()
    {
        int randomClip = Random.Range(0, 3);

        aS.Stop();
        aS.clip = musicClipArray[randomClip];
        aS.Play();
	}

    public void PlayFeedbackMusic(int clipIndex)
    {
        aS.Stop();
        aS.clip = feedbackClipArray[clipIndex];
        aS.Play();
    }

    public void PlayWhistle()
    {       
        aS.Stop();
        aS.clip = whistleClip;
        aS.Play();
    }
}
