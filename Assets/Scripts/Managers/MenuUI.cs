using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace fps.managers.game {
  public class MenuUI : MonoBehaviour {
    public void OnStart() {
      SceneManager.LoadScene("Game");
    }

    public void OnExit() {
      Application.Quit();
    }
  }
}
