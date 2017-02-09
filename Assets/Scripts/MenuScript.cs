using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    void Start()
    {
        if (FindObjectsOfType<MusicManager>().Length > 1)
        {
            Destroy(FindObjectsOfType<MusicManager>()[1].gameObject);
        }
    }

    public void QuitMenu()
    {
        Application.Quit();
    }

    public void ChangeScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }
}
