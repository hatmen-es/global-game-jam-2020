using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Init_LoadPreferences : MonoBehaviour
{
    public static string nicknameKey = "nickname";
    public static string brightnessKey = "masterBrightness";
    public static string volumenKey = "masterVolume";

    #region Variables
    // NICKNAME
    [SerializeField] private InputField nicknameInputField;
    [SerializeField] private Button newGameButton;
    
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
                if (PlayerPrefs.HasKey(nicknameKey))
                {
                    string text = PlayerPrefs.GetString(nicknameKey);
                    nicknameInputField.text = text;
                    newGameButton.interactable = text != null && text.Length > 0;
                }
                else
                {
                    newGameButton.interactable = false;
                }
            }

            //BRIGHTNESS
            if (brightnessEffect != null)
            {
                if (PlayerPrefs.HasKey(brightnessKey))
                {
                    float localBrightness = PlayerPrefs.GetFloat(brightnessKey);

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
            if (PlayerPrefs.HasKey(volumenKey))
            {
                float localVolume = PlayerPrefs.GetFloat(volumenKey);

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
