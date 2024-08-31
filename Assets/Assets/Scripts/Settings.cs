using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    Volume volume;
    Slider slider = null;
    [SerializeField] GameObject fastFill = null;
    [SerializeField] GameObject fancyFill = null;
    Sound menuClick;
    void Awake()
    {
        volume = Camera.main.GetComponent<Volume>();
        slider = FindObjectOfType<Slider>();
        
        bool volumeOn = intToBool(PlayerPrefs.GetInt("Volume", 1));
        volume.enabled = volumeOn;

        if(volumeOn)
        {
            fastFill.SetActive(false);
            fancyFill.SetActive(true);
        }else
        {
            fastFill.SetActive(true);
            fancyFill.SetActive(false);
        }
    }

    void Start()
    {
        menuClick = FindObjectOfType<AudioManager>().GetClip("Menu Click");
    }

    bool intToBool(int integer)
    {
        if (integer == 0)
        {
            return false;
        }
        else if (integer == 1)
        {
            return true;
        }
        else
        {
            Debug.LogError("Parameter doesn't contain 0 or 1: " + integer);
            return false;
        }
    }

    #region Button Methods
    public void GraphicsFancy()
    {
        volume.enabled = true;
        PlayerPrefs.SetInt("Volume", 1);
    }

    public void GraphicsFast()
    {
        volume.enabled = false;
        PlayerPrefs.SetInt("Volume", 0);
    }

    public void GetAudioVolume()
    {
        slider = FindObjectOfType<Slider>();
        slider.value = PlayerPrefs.GetFloat("Audio Volume", 1f);
    }

    public void SetAudioVolume()
    {
        FindObjectOfType<AudioManager>().SetSoundVolume(slider.value);
    }

    public void PlayClickMenuSound()
    {
        menuClick.Play();
    }
    #endregion

}
