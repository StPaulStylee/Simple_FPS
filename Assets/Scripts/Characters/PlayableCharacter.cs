using fps.characters;
using fps.managers.game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fps.characters {
  [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
  public abstract class PlayableCharacter : Character {
    [Header("Movement")]
    public float JumpForce = 4; // force applied upwards
    public float SprintMultiplier = 2;
    protected Vector3 desiredVelocity;

    [Header("Camera")]
    public float LookSensitivity = 4f; // mouse look sensitivity
    public float MinLookX = -80f; // Lowest we can look
    public float MaxLookX = 80f; // Highest we can look
    private float currentRotationX; // current x rotation of the camera

    private Camera cam;
    protected Rigidbody rb;

    // Use only when trying to do stuff within this GameObject
    protected override void Awake() {
      base.Awake();
      cam = Camera.main;
      rb = GetComponent<Rigidbody>();
      Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the center of the screen and hides it
    }

    protected virtual void Update() {
      desiredVelocity = GetMoveInputs();
      if (Input.GetButtonDown("Jump")) {
        TryJump();
      }
      if (Input.GetButton("Sprint")) {
        desiredVelocity.x *= SprintMultiplier;
        desiredVelocity.z *= SprintMultiplier;
      }
      CameraLook();
    }
    private void FixedUpdate() {
      Move(desiredVelocity);
    }

    protected abstract void TryJump();

    // Virtual so the programmer can change behavior in inherited class
    protected virtual Vector3 GetMoveInputs() {
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

    protected void Move(Vector3 direction) {
      rb.velocity = direction;
    }

    protected virtual void CameraLook() {
      float y = Input.GetAxis("Mouse X") * LookSensitivity; // Get Y rotation AROUND the x axis
      currentRotationX += Input.GetAxis("Mouse Y") * LookSensitivity; // Get X rotation AROUND the y axis
      currentRotationX = Mathf.Clamp(currentRotationX, MinLookX, MaxLookX); // Restrict x rotation (can only look up and down to set boundries)
      cam.transform.localRotation = Quaternion.Euler(-currentRotationX, 0, 0); // Apply x restriction (Note the negative in the x param, this makes in NOT INVERTED)
      transform.eulerAngles += Vector3.up * y; // Apply the y rotation to the player (Not the camera) so the player is always facing the correct way
    }
  }
}
