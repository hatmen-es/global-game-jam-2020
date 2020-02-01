using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Init_LoadPreferences : MonoBehaviour
{
    #region Variables
    // NICKNAME
    [SerializeField] private InputField nicknameInputField;
    
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
            //NICKNAME
            if (nicknameInputField != null)
            {
                if (PlayerPrefs.HasKey("nickname"))
                {
                    string text = PlayerPrefs.GetString("nickname");
                    nicknameInputField.text = text;
                }
            }

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
