﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Text timerText;
    public GameObject gameOvertext;

    public float timer = 5.0f;
    private int visibleTimer;
    public bool gameOver = false;
    public float scrollSpeed = -1.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        timerText.text = timer.ToString();
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
            visibleTimer = (int)timer;
            timerText.text = visibleTimer.ToString();
            if ( visibleTimer <= 0 )
            {
                GameOver();
            }
        }
    }


    public void IncrementTimer(int increment = 3)
    {
        timer = timer + increment;
    }

    public void GameOver()
    {
        gameOvertext.SetActive(true);
        gameOver = true;
    }
}