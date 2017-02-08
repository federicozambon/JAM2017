using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScene : MonoBehaviour
{
    public int score;

    public Text voto;
    public Text commento;
    public GameObject arbitroGood, arbitroBad;

    void Awake()
    {
        if (FindObjectOfType<MusicManager>())
        {
            score = FindObjectOfType<MusicManager>().score;
        }
        else
        {
            score = 1   ;
        }
    }

	void Start ()
    {
        if (score>5)
        {
            arbitroGood.SetActive(true); 
        }
        else
        {
            arbitroBad.SetActive(true);
        }

        switch (score)
        {
            case 0:
                commento.text = "Moreno";
                break;
            case 1:
                commento.text = "Juventino";
                break;
            case 2:
                commento.text = "Non pervenuto";
                break;
            case 3:
                commento.text = "Marquinhos";
                break;
            case 4:
                commento.text = "Lento ed impacciato";
                break;
            case 5:
                commento.text = "Ipovedente";
                break;
            case 6:
                commento.text = "Mediocre";
                break;
            case 7:
                commento.text = "Arbitro di provincia";
                break;
            case 8:
                commento.text = "Bomberone";
                break;
            case 9:
                commento.text = "'cezzionale";
                break;
            case 10:
                commento.text = "Collina";
                break;
        }

        voto.text = "VOTO: " + score.ToString();
	}

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
