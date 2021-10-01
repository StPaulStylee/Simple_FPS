using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fps.characters {
  public class MoveableCamera : PlayableCharacter {
    protected override void Die() {
      throw new System.NotImplementedException();
    }

    protected override void TryJump() {
      Ray ray = new Ray(transform.position, Vector3.down);
      // The player is 4 units tall, therefore 1 unit is in the middle of the player so we want to send our raycast downwards 1.1 units...
      // ... all the way down the player body plus another .1 units
      if (Physics.Raycast(ray, 2.1f)) {
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);  // AddForce by default behaves like adding force to a very heavy object; slowly. Impluse makes it instant; quick/snappy
      }
    }
  }
}
