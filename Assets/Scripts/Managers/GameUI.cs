using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace fps.managers.game {
  public class GameUI : MonoBehaviour {
    [Header("HUD")]
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private Text AmmoText;
    [SerializeField]
    private Image HealthBar;

    [Header("Pause Menu")]
    [SerializeField]
    private GameObject pauseMenu;

    [Header("End Game Dialogue")]
    [SerializeField]
    private GameObject endGameMenu;
    [SerializeField]
    private Text endGameHeader;
    [SerializeField]
    private Text endGameScore;

    public static GameUI Instance;

    private void Awake() {
      Instance = this; 
    }

    public void UpdateScoreText(int score = 0) {
      ScoreText.text = $"Score: {score}";
    }

    public void UpdateAmmoText(int currentAmmo, int maxAmmo) {
      AmmoText.text = $"Ammo: {currentAmmo} / {maxAmmo}";
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth) {
      HealthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    public void TogglePauseMenu(bool isPaused) {
      pauseMenu.SetActive(isPaused);
    }

    public void SetEndGameMenu(bool isWin, int score) {
      endGameMenu.SetActive(true);
      endGameHeader.text = isWin ? "You win!!" : "You lose!!";
      endGameHeader.color = isWin ? Color.green : Color.red;
      endGameScore.text = $"Score: {score}";
    }
  }
}
