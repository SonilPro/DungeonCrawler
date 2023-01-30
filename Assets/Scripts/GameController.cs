using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public GameObject player { get; private set; }

    [SerializeField] private Texture2D cursor;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject pauseMenu;
    private bool gameIsPaused = false;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player");

        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.ForceSoftware);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            (gameIsPaused ? (Action)ResumeGame : PauseGame)();
        }
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
    }

}
