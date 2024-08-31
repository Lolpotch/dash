using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEraser : MonoBehaviour
{
    GameObject player;
    public float distance;
    public float multiplier;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(player != null)
        {
            float x = player.transform.position.x + distance * multiplier;
            Vector2 newPos = new Vector2(x, transform.position.y);

            transform.position = newPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Target")
        {
            Destroy(collision.gameObject);
        }
    }
}
