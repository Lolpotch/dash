using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : MonoBehaviour
{
    PlayerMovement playerMovement;
    Rigidbody2D playerRb;
    SpawnManager spawnManager;

    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerRb = playerMovement.GetComponent<Rigidbody2D>();
        spawnManager= FindObjectOfType<SpawnManager>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            playerMovement.tutorial = 1;

            playerRb.gravityScale = 3f;
            FindObjectOfType<AudioManager>().StartMusic();
            spawnManager.BeginSpawn();

            gameObject.SetActive(false);
        }
    }
}
