using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    #region Default Values
    [Header("Default Menu Values")]
    [SerializeField] private float defaultBrightness;
    [SerializeField] private float defaultVolume;
    [SerializeField] private int defaultSen;
    [SerializeField] private bool defaultInvertY;

    [Header("Levels To Load")]
    private string sceneName = "Main";
    [SerializeField] private int menuNumber;
    #endregion

    #region Menu Dialogs
    [Header("Main Menu Components")]
    [SerializeField] private GameObject menuDefaultCanvas;
    [SerializeField] private GameObject GeneralSettingsCanvas;
    [SerializeField] private GameObject creditsCanvas;
    [SerializeField] private GameObject graphicsMenu;
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject gameplayMenu;
    [SerializeField] private GameObject controlsMenu;
    [Space(10)]
    #endregion

    #region Slider Linking
    [Header("Menu Sliders")]
    [SerializeField] private Text controllerSenText;
    [SerializeField] private Slider controllerSenSlider;
    public float controlSenFloat = 2f;
    [Space(10)]
    [SerializeField] private Brightness brightnessEffect;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Text brightnessText;
    [Space(10)]
    [SerializeField] private Text volumeText;
    [SerializeField] private Slider volumeSlider;
    [Space(10)]
    [SerializeField] private Toggle invertYToggle;
    #endregion

    #region Initialisation - Button Selection & Menu Order
    private void Start()
    {
        menuNumber = 1;
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
            SceneManager.LoadScene(sceneName);

            menuNumber = 7;
        }
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
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        Debug.Log(PlayerPrefs.GetFloat("masterVolume"));
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
        PlayerPrefs.SetFloat("masterBrightness", brightnessEffect.brightness);
        Debug.Log(PlayerPrefs.GetFloat("masterBrightness"));
    }
    #endregion

    #region Controller Sensitivity
    public void ControllerSen()
    {
        controllerSenText.text = controllerSenSlider.value.ToString("0");
        controlSenFloat = controllerSenSlider.value;
    }
    #endregion

    public void GameplayApply()
    {
        #region Invert
        if (invertYToggle.isOn) //Invert Y ON
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
            Debug.Log("Invert" + " " + PlayerPrefs.GetInt("masterInvertY"));
        }

        else if (!invertYToggle.isOn) //Invert Y OFF
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
            Debug.Log(PlayerPrefs.GetInt("masterInvertY"));
        }
        #endregion

        #region Controller Sensitivity
        PlayerPrefs.SetFloat("masterSen", controlSenFloat);
        Debug.Log("Sensitivity" + " " + PlayerPrefs.GetFloat("masterSen"));
        #endregion
    }

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

        if (GraphicsMenu == "Graphics")
        {
            controllerSenText.text = defaultSen.ToString("0");
            controllerSenSlider.value = defaultSen;
            controlSenFloat = defaultSen;

            invertYToggle.isOn = false;

            GameplayApply();
        }
    }
    #endregion


    #region Back to Menus
    public void GoBackToOptionsMenu()
    {
        GeneralSettingsCanvas.SetActive(true);
        graphicsMenu.SetActive(false);
        soundMenu.SetActive(false);
        gameplayMenu.SetActive(false);

        GameplayApply();
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
        gameplayMenu.SetActive(false);
        menuNumber = 1;
    }

    public void GoBackToGameplayMenu()
    {
        controlsMenu.SetActive(false);
        gameplayMenu.SetActive(true);
        menuNumber = 5;
    }

    public void ClickQuitOptions()
    {
        GoBackToMainMenu();
    }

    #endregion
}
