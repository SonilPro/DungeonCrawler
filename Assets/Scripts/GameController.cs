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
        if (Input.GetKeyDown(KeyCode.P))
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
        gameIsPaused = true;
        Time.timeScale = 0f;
    }
    private void ResumeGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
    }

}
