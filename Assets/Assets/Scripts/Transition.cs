using System.Collections;
using UnityEngine;

public class Transition : MonoBehaviour
{
    Animator transition;
    GameManager manager;

    void Awake()
    {
        transition = GetComponent<Animator>();
        manager = FindObjectOfType<GameManager>();
    }

    public void StartTransition(string sceneName)
    {
        StartCoroutine(EnumTransition(sceneName));
    }

    IEnumerator EnumTransition(string name)
    {
        transition.Play("Fade In");

        yield return new WaitForSeconds(.5f);

        manager.LoadScene(name);
    }
}
