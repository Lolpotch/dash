using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Lava : MonoBehaviour
{
    KillPlayer kill;

    void Awake()
    {
        kill = FindObjectOfType<KillPlayer>();
    }

    void Update()
    {
        float x = Camera.main.transform.position.x;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            kill.DestroyPlayer();
        }
    }
}
