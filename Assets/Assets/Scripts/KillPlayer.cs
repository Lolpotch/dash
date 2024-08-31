using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject losePanel = null;
    CameraShaker shaker;
    ParticleManager particle;
    Life life;
    Sound playerDestroyed;
    Sound music;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shaker = FindObjectOfType<CameraShaker>();
        particle = FindObjectOfType<ParticleManager>();
        life = FindObjectOfType<Life>();
        playerDestroyed = FindObjectOfType<AudioManager>().GetClip("Player Destroyed");
        music = FindObjectOfType<AudioManager>().GetClip("Music");
    }

    public void DestroyPlayer()
    {
        print("You dead");
        StartCoroutine(shaker.CameraShake(1.5f, .01f, 20));
        particle.PlayerDestroyed(player.transform.position);
        life.SetLifeDisplay(0);
        Destroy(player.gameObject);
        playerDestroyed.Play();
        music.Stop();

        losePanel.SetActive(true);
    }
}
