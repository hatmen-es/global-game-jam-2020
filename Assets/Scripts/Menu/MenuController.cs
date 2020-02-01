using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    #region Default Values
    [Header("Go to main")]
    public string scene = "Main";
    public Color loadToColor = Color.black;
    #endregion

    #region Default Values
    [Header("Default Menu Values")]
    [SerializeField] private float defaultBrightness;
    [SerializeField] private float defaultVolume;
    [SerializeField] private int defaultSen;
    [SerializeField] private bool defaultInvertY;

    [Header("Levels To Load")]
    [SerializeField] private int menuNumber;
    #endregion

    #region Menu Dialogs
    [Header("Main Menu Components")]
    [SerializeField] private GameObject menuDefaultCanvas;
    [SerializeField] private GameObject GeneralSettingsCanvas;
    [SerializeField] private GameObject creditsCanvas;
    [SerializeField] private GameObject graphicsMenu;
    [SerializeField] private GameObject soundMenu;
    [Space(10)]
    #endregion

    #region Default
    [SerializeField] private Button newGameButton;
    [SerializeField] private InputField nicknameInputField;
    #endregion

    #region Slider Linking
    [Header("Menu Sliders")]
    [SerializeField] private Brightness brightnessEffect;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Text brightnessText;
    [Space(10)]
    [SerializeField] private Text volumeText;
    [SerializeField] private Slider volumeSlider;
    #endregion

    public AudioClip AudioClipMenu;
    AudioSource audioSourceMenu;

    #region Initialisation - Button Selection & Menu Order
    private void Start()
    {
        menuNumber = 1;
        audioSourceMenu = gameObject.AddComponent<AudioSource>();
        audioSourceMenu.clip = AudioClipMenu;
        audioSourceMenu.Play();
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuNumber == 2 || menuNumber == 7 || menuNumber == 8)
            {
                GoBackToMainMenu();
                ClickSound();
            }

            else if (menuNumber == 3 || menuNumber == 4 || menuNumber == 5)
            {
                GoBackToOptionsMenu();
                ClickSound();
            }

            else if (menuNumber == 6) //CONTROLS MENU
            {
                GoBackToGameplayMenu();
                ClickSound();
            }
        }
    }

    private void ClickSound()
    {
        GetComponent<AudioSource>().Play();
    }

    #region Menu Mouse Clicks
    public void  MouseClick(string buttonType)
    {

        if (buttonType == "Graphics")
        {
            GeneralSettingsCanvas.SetActive(false);
            graphicsMenu.SetActive(true);
            menuNumber = 3;
        }

        if (buttonType == "Sound")
        {
            GeneralSettingsCanvas.SetActive(false);
            soundMenu.SetActive(true);
            menuNumber = 4;
        }


		if(buttonType == "Exit")
		{
			Debug.Log("YES QUIT!");
			Application.Quit();
		}
	
		if(buttonType == "Options")
		{
            menuDefaultCanvas.SetActive(false);
            GeneralSettingsCanvas.SetActive(true);
            menuNumber = 2;
        }
	
        if(buttonType == "Credits")
		{
            menuDefaultCanvas.SetActive(false);
            creditsCanvas.SetActive(true);
            menuNumber = 10;
        }

		if(buttonType == "NewGame")
		{
            menuDefaultCanvas.SetActive(false);
            Initiate.Fade(scene, loadToColor, 1.0f);

            menuNumber = 7;
        }
    }
    #endregion

    #region Nickname
    public void NicknameInputField()
    {
        SaveNickNameApply();
    }

    public void SaveNickNameApply()
    {
        string text = nicknameInputField.text;
        PlayerPrefs.SetString(Init_LoadPreferences.nicknameKey, text);
        newGameButton.interactable = text != null && text.Length > 0;

        Random.InitState(text.GetHashCode());
    }

    #endregion

    #region Volume Sliders Click
    public void VolumeSlider(float volume)
    {
        AudioListener.volume = volume;
        volumeText.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat(Init_LoadPreferences.volumenKey, AudioListener.volume);
    }
    #endregion

    #region Brightness Sliders Click
    public void BrightnessSlider(float brightness)
    {
        brightnessEffect.brightness = brightness;
        brightnessText.text = brightness.ToString("0.0");
    }

    public void BrightnessApply()
    {
        PlayerPrefs.SetFloat(Init_LoadPreferences.brightnessKey, brightnessEffect.brightness);
    }
    #endregion

    #region ResetButton
    public void ResetButton(string GraphicsMenu)
    {
        if (GraphicsMenu == "Brightness")
        {
            brightnessEffect.brightness = defaultBrightness;
            brightnessSlider.value = defaultBrightness;
            brightnessText.text = defaultBrightness.ToString("0.0");
            BrightnessApply();
        }

        if (GraphicsMenu == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeText.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
    }
    #endregion


    #region Back to Menus
    public void GoBackToOptionsMenu()
    {
        GeneralSettingsCanvas.SetActive(true);
        graphicsMenu.SetActive(false);
        soundMenu.SetActive(false);

        BrightnessApply();
        VolumeApply();

        menuNumber = 2;
    }

    public void GoBackToMainMenu()
    {
        menuDefaultCanvas.SetActive(true);
        GeneralSettingsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        graphicsMenu.SetActive(false);
        soundMenu.SetActive(false);
        menuNumber = 1;
    }

    public void GoBackToGameplayMenu()
    {
        menuNumber = 5;
    }

    public void ClickQuitOptions()
    {
        GoBackToMainMenu();
    }

    #endregion
}
