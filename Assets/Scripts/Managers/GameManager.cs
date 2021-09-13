using fps.managers.game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace fps.managers.game {
  public class GameManager : MonoBehaviour {
    public int ScoreToWin;
    public int CurrentScore = 0;

    public bool IsGamePaused;

    public static GameManager Instance;

    private void Awake() {
      Instance = this;
    }

    private void Start() {
      GameUI.Instance.UpdateScoreText(CurrentScore);
    }

    private void Update() {
      if (Input.GetButtonDown("Cancel")) {
        TogglePauseGame();
      }
    }

    public void TogglePauseGame() {
      IsGamePaused = !IsGamePaused;
      Time.timeScale = IsGamePaused ? 0f : 1f;
      GameUI.Instance.TogglePauseMenu(IsGamePaused);
    }

    public void OnRestart() {
      SceneManager.LoadScene("Game");
    }

    public void OnMenu() {
      SceneManager.LoadScene("Menu");
    }

    public void OnExit() {
      Debug.Log("You're quitting the game.");
    }

    public void AddScore(int score) {
      CurrentScore += score;
      GameUI.Instance.UpdateScoreText(CurrentScore);
      if (CurrentScore >= ScoreToWin) {
        WinGame();
      }
    }

    private void WinGame() {
      GameUI.Instance.SetEndGameMenu(true, CurrentScore);
    }
  }
}

