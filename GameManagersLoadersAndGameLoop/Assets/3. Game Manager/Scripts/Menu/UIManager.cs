﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private Camera _dummyCamera;

    private void Start()
    {
        //event should be registered at the time the object is created
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void Update()
    {
        //UI manager can start the game only when the game is in the required state
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //start game
            GameManager.Instance.StartGame();

            //_mainMenu.FadeOut(); //it's being called on OnGameStateChanged event now in the MainMenu
        }
    }

    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.gameObject.SetActive(active);
    }

    private void HandleGameStateChanged(GameManager.GameState currentGameState, GameManager.GameState previousGameState)
    {
        _pauseMenu.gameObject.SetActive(currentGameState == GameManager.GameState.PAUSED);
    }
}
