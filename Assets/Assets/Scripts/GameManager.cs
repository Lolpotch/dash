using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Main" && PlayerPrefs.GetInt("Tutorial", 0) == 1)
        {
            FindObjectOfType<AudioManager>().StartMusic();
        }
        else if(PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            tutorialPanel.SetActive(true);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
