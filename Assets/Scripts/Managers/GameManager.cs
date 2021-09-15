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
    public bool IsEndGame = false;
    public static GameManager Instance;

    private void Awake() {
      Instance = this;
    }

    private void Start() {
      Time.timeScale = 1f;
      GameUI.Instance.UpdateScoreText(CurrentScore);
    }

    private void Update() {
      if (Input.GetButtonDown("Cancel")) {
        TogglePauseGame();
      }
    }

    public void TogglePauseGame() {
      if (IsEndGame) {
        return;
      }
      IsGamePaused = !IsGamePaused;
      if (IsGamePaused) {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
      } else {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
      }
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
        EndGame(true);
      }
    }

    public void EndGame(bool victoryState) {
      IsEndGame = true;
      Time.timeScale = 0f;
      Cursor.lockState = CursorLockMode.Confined;
      GameUI.Instance.SetEndGameMenu(victoryState, CurrentScore);
    }
  }
}

