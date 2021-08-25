using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fps.characters {
  public class Player : Character {
    [Header("Movement")]
    public float JumpForce; // force applied upwards
    private Vector3 desiredVelocity;

    [Header("Camera")]
    public float LookSensitivity; // mouse look sensitivity
    public float MinLookX; // Lowest we can look
    public float MaxLookX; // Highest we can look
    private float currentRotationX; // current x rotation of the camera

    private Camera cam;
    private Rigidbody rb;

    // Use only when trying to do stuff within this GameObject
    protected override void Awake() {
      base.Awake();
      cam = Camera.main;
      rb = GetComponent<Rigidbody>();
      Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the center of the screen and hides it
    }

    private void Update() {
      desiredVelocity = GetMoveInputs();
      if (Input.GetButtonDown("Jump")) {
        TryJump();
      }
      if (Input.GetButton("Fire1")) { // GetButton is used here because it is called every frame that the button is held down
        if (weapon.CanShoot()) {
          weapon.Fire();
        }
      }
      CameraLook();
    }

    private void FixedUpdate() {
      Move(desiredVelocity);
    }

    private void TryJump() {
      Ray ray = new Ray(transform.position, Vector3.down);
      // The player is 2 units tall, therefore 1 unit is in the middle of the player so we want to send our raycast downwards 1.1 units...
      // ... all the way down the player body plus another .1 units
      if (Physics.Raycast(ray, 1.1f)) {
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);  // AddForce by default behaves like adding force to a very heavy object; slowly. Impluse makes it instant; quick/snappy
      }
    }

    private Vector3 GetMoveInputs() {
      Vector2 playerInput;
      playerInput.x = Input.GetAxis("Horizontal");
      // Remember, this will actually be applied to the 'z' of the velocity vector
      playerInput.y = Input.GetAxis("Vertical");
      // This eliminates that behavior that allows diagonal speed to be faster than forward/back/side to side speed
      playerInput = Vector2.ClampMagnitude(playerInput, 1f);
      // This takes the global values recieved above and makes them local to our player
      // This is necessary to correctly move in the direction we are facing
      // The transform.right/forward correspond to x axis (red) and z axis (blue)
      // Is there a way to do this in editor and not in code?
      Vector3 direction = (transform.right * playerInput.x + transform.forward * playerInput.y) * MoveSpeed;
      direction.y = rb.velocity.y; // Set the y to the players current y
      return direction;
    }

    private void Move(Vector3 direction) {
      rb.velocity = direction;
    }

    private void CameraLook() {
      float y = Input.GetAxis("Mouse X") * LookSensitivity; // Get Y rotation AROUND the x axis
      currentRotationX += Input.GetAxis("Mouse Y") * LookSensitivity; // Get X rotation AROUND the y axis
      currentRotationX = Mathf.Clamp(currentRotationX, MinLookX, MaxLookX); // Restrict x rotation (can only look up and down to set boundries)
      cam.transform.localRotation = Quaternion.Euler(-currentRotationX, 0, 0); // Apply x restriction (Note the negative in the x param, this makes in NOT INVERTED)
      transform.eulerAngles += Vector3.up * y; // Apply the y rotation to the player (Not the camera) so the player is always facing the correct way
    }

    protected override void Die() {
      Debug.Log("Ya done, son!");
    }

    public void GiveHealth(int amount) {
      CurrentHp = Mathf.Clamp(CurrentHp + amount, 0, MaxHp);
    }

    public void GiveAmmo(int amount) {
      weapon.CurrentAmmo = Mathf.Clamp(weapon.CurrentAmmo + amount, 0, weapon.MaxAmmo);
    }
  }
}
