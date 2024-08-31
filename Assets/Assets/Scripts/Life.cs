using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    KillPlayer kill;
    int lifeAmount;
    int maxLifeAmount;
    [SerializeField] GameObject[] lifes = null;
    void Awake()
    {
        maxLifeAmount = lifes.Length;
        RefillLife();

        kill = FindObjectOfType<KillPlayer>();
    }

    public void SetLifeDisplay(int lifeAmount)
    {
        for (int i = 0; i < maxLifeAmount; i++)
        {
            if(i+1 <=  lifeAmount)
            {
                lifes[i].SetActive(true);
            }else
            {
                lifes[i].SetActive(false);
            }
        }
    }

    public void AddLife(int amount)
    {
        lifeAmount += amount;
        if(lifeAmount <= 0)
        {
            lifeAmount = 0;
            kill.DestroyPlayer();
        }
        else if(lifeAmount > maxLifeAmount)
        {
            lifeAmount = maxLifeAmount;
        }

        SetLifeDisplay(lifeAmount);
    }

    public void RefillLife()
    {
        lifeAmount = maxLifeAmount;
        SetLifeDisplay(lifeAmount);
    }

}
