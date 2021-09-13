using fps.characters;
using fps.managers.game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fps.objects {
  public class Pickup : MonoBehaviour {
    public PickupType Type;
    public int Value;

    [Header("Animate")]
    public float RotateSpeed = 90f;
    public float BobSpeed = 1f;
    public float BobHeight = 1f;
    private Vector3 startPosition;
    private bool isBobbingUp;

    private void Start() {
      startPosition = transform.position;
    }

    private void Update() {
      Animate();
    }

    private void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Player")) {
        Debug.Log($"{Type}: {Value} TRIGGERED!");
        Player player = other.GetComponent<Player>();
        switch (Type) {
          case PickupType.Health:
            player.GiveHealth(Value);
            GameUI.Instance.UpdateHealthBar(player.CurrentHp, player.MaxHp);
            break;
          case PickupType.Ammo:
            player.GiveAmmo(Value);
            GameUI.Instance.UpdateAmmoText(player.weapon.CurrentAmmo, player.weapon.MaxAmmo);
            break;
        }
        Destroy(gameObject);
      }
    }

    private void Animate() {
      transform.Rotate(Vector3.up, RotateSpeed * Time.deltaTime);
      Vector3 offset = isBobbingUp == true ? new Vector3(0, BobHeight / 2, 0) : new Vector3(0, -BobHeight / 2, 0); // Either moving up towards the start position or down towards it
      transform.position = Vector3.MoveTowards(transform.position, startPosition + offset, BobSpeed * Time.deltaTime);
      if (transform.position == startPosition + offset) {
        isBobbingUp = !isBobbingUp;
      }
    }
  }

  public enum PickupType {
    Health,
    Ammo
  }
}
