using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    RectTransform rect;
    Image image;
    [HideInInspector] public float ratio = 1f;
    [HideInInspector] public bool started = false;

    float drainSpeed;
    [SerializeField] float maxDrainSpeed = 1f;
    [SerializeField] float minDrainSpeed = .2f;
    [SerializeField] float increaseSpeed = .1f;

    [Header("Color")]
    [SerializeField] Gradient color = null;

    private void Awake()
    {
        started = false;
        drainSpeed = minDrainSpeed;
        ratio = 1f;

        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        if(started)
        {
            DecreaseEnergy();
        }
        ChangeColor();
    }

    void DecreaseEnergy()
    {
        rect.localScale = new Vector2(ratio, 1f);
        ratio -= drainSpeed * Time.deltaTime;

        if(ratio < 0f)
        {
            ratio = 0f;
        }
    }

    public void RefillEnergy()
    {
        ratio = 1f;
        rect.localScale = new Vector2(ratio, 1f);
    }

    public void IncreaseDrainSpeed()
    {
        drainSpeed += increaseSpeed;
        
        if(drainSpeed > maxDrainSpeed)
        {
            drainSpeed = maxDrainSpeed;
        }
        print(drainSpeed);
    }

    void ChangeColor()
    {
        image.color = color.Evaluate(ratio);
    }
}
