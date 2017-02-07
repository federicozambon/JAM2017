using UnityEngine;
using System.Collections;

public class BackGroundPicker : MonoBehaviour
{
    public string backGroundID;
    SpriteRenderer sr;

	void Awake ()
    {
        sr = GetComponent<SpriteRenderer>();
        switch (backGroundID)
        {
            case "guardalinee":
                sr.sprite = Resources.Load<Sprite> ("Scena1");
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
