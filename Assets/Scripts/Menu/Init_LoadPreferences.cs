﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Init_LoadPreferences : MonoBehaviour
{
    #region Variables
    //BRIGHTNESS
    [Space(20)]
    [SerializeField] private Brightness brightnessEffect;
    [SerializeField] private Text brightnessText;
    [SerializeField] private Slider brightnessSlider;

    //VOLUME
    [Space(20)]
    [SerializeField] private Text volumeText;
    [SerializeField] private Slider volumeSlider;

    [Space(20)]
    [SerializeField] private bool canUse = false;
    [SerializeField] private bool isMenu = false;
    [SerializeField] private MenuController menuController;
    #endregion

    private void Awake()
    {
        Debug.Log("Loading player prefs test");

        if (canUse)
        {
            //BRIGHTNESS
            if (brightnessEffect != null)
            {
                if (PlayerPrefs.HasKey("masterBrightness"))
                {
                    float localBrightness = PlayerPrefs.GetFloat("masterBrightness");

                    brightnessText.text = localBrightness.ToString("0.0");
                    brightnessSlider.value = localBrightness;
                    brightnessEffect.brightness = localBrightness;
                }

                else
                {
                    menuController.ResetButton("Brightness");
                }
            }

            //VOLUME
            if (PlayerPrefs.HasKey("masterVolume"))
            {
                float localVolume = PlayerPrefs.GetFloat("masterVolume");

                volumeText.text = localVolume.ToString("0.0");
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }
            else
            {
                menuController.ResetButton("Audio");
            }
        }
    }
}
