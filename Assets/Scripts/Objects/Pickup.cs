using fps.characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fps.objects {
  public class Pickup : MonoBehaviour {
    public PickupType Type;
    public int Value;

    private void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Player")) {
        Debug.Log($"{Type}: {Value} TRIGGERED!");
        Player player = other.GetComponent<Player>();
        switch (Type) {
          case PickupType.Health:
            player.GiveHealth(Value);
            break;
          case PickupType.Ammo:
            player.GiveAmmo(Value);
            break;
        }
      }
    }
  }

  public enum PickupType {
    Health,
    Ammo
  }
}
