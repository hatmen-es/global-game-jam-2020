﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Text timerText;
    public Text distanceText;
    public GameObject gameOvertext;

    public float timer = 5.0f;
    private float totalTimer = 0.0f;
    private int visibleTimer;
    public bool gameOver = false;

    public GameObject imageTool1;
    public GameObject imageTool2;
    public GameObject imageTool3;
    public GameObject imageTool4;
    private int selectedTool = 1;
    private int speed = 1;

    public AudioClip AudioClipSong;
    public AudioClip AudioClipGameover;
    AudioSource audioSourceSong;
    AudioSource audioSourceGameover;

    private void Awake()
    {
        timerText.text = timer.ToString();
        setTool(selectedTool);
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        audioSourceSong = gameObject.AddComponent<AudioSource>();
        audioSourceGameover = gameObject.AddComponent<AudioSource>();

        audioSourceSong.clip = AudioClipSong;
        audioSourceGameover.clip = AudioClipGameover;
        
        audioSourceSong.Play();

    }

    void Update()
    {
        if (gameOver && Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (!gameOver)
        {
            timer -= Time.deltaTime;
            totalTimer += Time.deltaTime;
            visibleTimer = (int)timer;
            timerText.text = visibleTimer.ToString();
            // TODO: Replace this speed variable by CameraMovement.cs
            distanceText.text = ((int)totalTimer * speed).ToString() + " m";
            if ( visibleTimer <= 0 )
            {
                GameOver();
            }
        }
    }

    private void resetToolScale(GameObject tool)
    {
        tool.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
    }

    private void selectTool(GameObject tool, int selected)
    {
        resetAllToolsScale();
        tool.GetComponent<RectTransform>().localScale += new Vector3(.5f, 0.5f, 0);
        selectedTool = selected;
    }

    private void resetAllToolsScale()
    {
        resetToolScale(imageTool1);
        resetToolScale(imageTool2);
        resetToolScale(imageTool3);
        resetToolScale(imageTool4);
    }

    public void setTool(int selected)
    {
        switch (selected)
          {
          case 1:
              selectTool(imageTool1, 1);
              break;
          case 2:
              selectTool(imageTool2, 2);
              break;
          case 3:
              selectTool(imageTool3, 3);
              break;
          case 4:
              selectTool(imageTool4, 4);
              break;
          default:
              // Console.WriteLine("Error in selection");
              break;
      }
    }

    public int getSelectedTool()
    {
        return selectedTool;
    }

    public void IncrementTimer(int increment = 3)
    {
        timer = timer + increment;
    }

    public void GameOver()
    {
         audioSourceSong.Stop();
        audioSourceGameover.Play();
        gameOvertext.SetActive(true);
        gameOver = true;
    }
}