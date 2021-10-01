using fps.managers.game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fps.characters {
  public class Player : PlayableCharacter {
    private void Start() {
      GameUI.Instance.UpdateHealthBar(CurrentHp, MaxHp);
    }

    protected override void Update() {
      if (GameManager.Instance.IsGamePaused || GameManager.Instance.IsEndGame) {
        return;
      }
      if (Input.GetButton("Fire1")) { // GetButton is used here because it is called every frame that the button is held down
        if (weapon.CanShoot()) {
          weapon.Fire();
        }
      }
      base.Update();
    }

    protected override void TryJump() {
      Ray ray = new Ray(transform.position, Vector3.down);
      // The player is 2 units tall, therefore 1 unit is in the middle of the player so we want to send our raycast downwards 1.1 units...
      // ... all the way down the player body plus another .1 units
      // TODO: Get this value from the gameobject instead of hard coding
      if (Physics.Raycast(ray, 1.1f)) {
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);  // AddForce by default behaves like adding force to a very heavy object; slowly. Impluse makes it instant; quick/snappy
      }
    }

    protected override void Die() {
      GameManager.Instance.EndGame(false);
    }

    public void GiveHealth(int amount) {
      CurrentHp = Mathf.Clamp(CurrentHp + amount, 0, MaxHp);
    }

    public void GiveAmmo(int amount) {
      weapon.CurrentAmmo = Mathf.Clamp(weapon.CurrentAmmo + amount, 0, weapon.MaxAmmo);
    }

    public override void TakeDamge(int damage) {
      base.TakeDamge(damage);
      GameUI.Instance.UpdateHealthBar(CurrentHp, MaxHp);
    }
  }
}
