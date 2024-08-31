using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] GameObject targetDestroyed = null;
    [SerializeField] GameObject bombDestroyed = null;
    [SerializeField] GameObject heartDestroyed = null;
    [SerializeField] GameObject playerDestroyed = null;

    [SerializeField] GameObject plus10 = null;
    [SerializeField] GameObject minus50 = null;
    [SerializeField] GameObject plusLife = null;


    [SerializeField] GameObject tapEffect = null;
    

    public void TapEffect(Vector3 target)
    {
        Instantiate(tapEffect, target, Quaternion.identity);
    }

    public void PointAddedEffect(string targetTag, Vector3 target)
    {
        switch(targetTag)
        {
            case "Target":
                Instantiate(plus10, target, Quaternion.identity);
                break;

            case "Bomb":
                Instantiate(minus50, target, Quaternion.identity);
                break;

            case "Life":
                Instantiate(plusLife, target, Quaternion.identity);
                break;

            default:
                Debug.LogError("Unknow tag: " + targetTag);
                break;
        }
    }

    public void TargetDestroyed(string targetTag, Vector3 target)
    {
        switch (targetTag)
        {
            case "Target":
                Instantiate(targetDestroyed, target, Quaternion.identity);
                break;

            case "Bomb":
                Instantiate(bombDestroyed, target, Quaternion.identity);
                break;

            case "Life":
                Instantiate(heartDestroyed, target, Quaternion.identity);
                break;

            default:
                Debug.LogError("Unknow tag: " + targetTag);
                break;
        }
    }

    public void PlayerDestroyed(Vector3 player)
    {
        Instantiate(playerDestroyed, player, Quaternion.identity);
    }

    /*
    public void DashEffect(Transform dashSpot)
    {
        GameObject a = Instantiate(dashEffect, dashSpot.position, Quaternion.identity);
        a.transform.rotation = dashSpot.rotation;
    }
    */
}