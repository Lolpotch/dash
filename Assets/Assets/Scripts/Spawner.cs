using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] int spawnAmount = 0;
    public float areaHeight;
    public float areaWidth;
    [SerializeField] float minDistance = 0;

    [Header("Target Prefabs")]
    public GameObject[] targetPrefabs;
    [Range(0f, 1f)] public float[] percentage;

    [Header("Screen border")]
    public GameObject borderLB;
    public GameObject borderRA;
    [Space(10)]
    public Transform lava;

    //General References
    GameObject RA;
    GameObject LB;
    Camera mainCam;
    Vector3 screenBorder;
    void Awake()
    {
        mainCam = Camera.main;
        screenBorder = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10f));
        LB = Instantiate(borderLB, -screenBorder, Quaternion.identity, mainCam.transform);
        RA = Instantiate(borderRA, screenBorder, Quaternion.identity, mainCam.transform);
    }

    public void SpawnTargets(string area)
    {
        //Spawns a limited amount of targets
        for(int i = 0; i < spawnAmount; i++)
        {
            SetTargetPos(area);
        }
    }

    void SetTargetPos(string area)
    {
        //Spawns an individual targets
        Collider2D[] colliders = { };
        //Collider2D[] colliders_player = { };
        Vector3 spawnPos = Vector3.zero;

        do
        {
            if (area == "Center")
            {
                spawnPos = RandomCenterCam();
            }
            else if (area == "Right")
            {
                spawnPos = RandomRightCam();
            }
            else if (area == "Left")
            {
                spawnPos = RandomLeftCam();
            }
            else
            {
                print("An argument isn't right!: " + area);
            }

            colliders = Physics2D.OverlapCircleAll(spawnPos, minDistance, 1 << 9);
            //colliders_player = Physics2D.OverlapCircleAll(spawnPos, minDistance, 1 << 10);

        } while (colliders.Length > 0);//&& colliders_player.Length > 0);

        SpawnRandomTarget(spawnPos);
    }

    void SpawnRandomTarget(Vector3 spawnPos)
    {
        float chance = Random.Range(0f, 1f);

        for(int i = percentage.Length - 1; i >= 0; i--)
        {
            if(chance <= percentage[i])
            {
                Instantiate(targetPrefabs[i], spawnPos, Quaternion.identity);
                break;
            }
        }

    }

    public float GetBombSpawnRate()
    {
        return percentage[1];
    }

    public void SetBombSpawnRate(float percent)
    {
        percentage[1] = percent;
    }

    #region randomAreas
    Vector3 RandomRightCam()
    {
        float rightX = RA.transform.position.x;
        float randomX = Random.Range(rightX, rightX + areaWidth);
        float randomY = Random.Range(lava.position.y, areaHeight);
        Vector3 spawnPos = new Vector3(randomX, randomY, -1f);
        return spawnPos;
    }

    Vector3 RandomCenterCam()
    {
        Vector2 ra = RA.transform.position;
        Vector2 lb = LB.transform.position;

        float randomX = Random.Range(lb.x, ra.x);
        float randomY = Random.Range(lava.position.y, areaHeight);

        Vector3 spawnPos = new Vector3(randomX, randomY, -1f);

        return spawnPos;
    }

    Vector3 RandomLeftCam()
    {
        float leftX = LB.transform.position.x;
        float randomX = Random.Range(leftX, leftX - areaWidth);
        float randomY = Random.Range(lava.position.y, areaHeight);
        Vector3 spawnPos = new Vector3(randomX, randomY, -1f);
        return spawnPos;
    }
    #endregion
}
