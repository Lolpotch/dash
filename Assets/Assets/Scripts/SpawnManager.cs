using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float minBombSpawnRate = .25f;
    [SerializeField] float maxBombSpawnRate = .4f;
    [SerializeField] float increaseRatio = .01f;

    float leftTravelDistance = 0f;
    float rightTravelDistance = 0f;
    float leftLimit;
    float rightLimit;

    Spawner spawner;
    Transform player;

    void Awake()
    {
        spawner = GetComponent<Spawner>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        leftLimit = -90f;
        rightLimit = 90f;
        spawner.SetBombSpawnRate(minBombSpawnRate);
    }

    void Start()
    {
        if(PlayerPrefs.GetInt("Tutorial", 0) == 1)
        {
            BeginSpawn();
        }
    }

    public void BeginSpawn()
    {
        spawner.SpawnTargets("Left");
        spawner.SpawnTargets("Center");
        spawner.SpawnTargets("Right");
    }

    void Update()
    {
        if(player != null)
        {
            CountPlayerDistance();
        }
        TriggerSpawner();
    }

    void CountPlayerDistance()
    {
        float playerX = player.position.x;

        if (playerX < leftTravelDistance)
        {
            leftTravelDistance = playerX;
        }
        else if (playerX > rightTravelDistance)
        {
            rightTravelDistance = playerX;
        }
    }

    void TriggerSpawner()
    {
        if (leftTravelDistance < leftLimit)
        {
            IncreaseBombSpawnRate();
            spawner.SpawnTargets("Left");
            leftLimit -= spawner.areaWidth;
        }

        if(rightTravelDistance > rightLimit)
        {
            IncreaseBombSpawnRate();
            spawner.SpawnTargets("Right");
            rightLimit += spawner.areaWidth;
        }
    }

    void IncreaseBombSpawnRate()
    {
        float percent = spawner.GetBombSpawnRate() + increaseRatio;
        if(percent > maxBombSpawnRate)
        {
            percent = maxBombSpawnRate;
        }
        spawner.SetBombSpawnRate(percent);
    }

    //For score reference
    public int GetTravelDistance()
    {
        float travelDistance = rightTravelDistance + leftTravelDistance * -1;

        return (int)(travelDistance * .1f);
    }
}
