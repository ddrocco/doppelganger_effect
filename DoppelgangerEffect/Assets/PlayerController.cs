/* DEBUGGUING KEYBOARD CONTROLS
 * 
 * IJKL - movement
 * Mouse - look around
 * Tab - switch between Stan's mouse controls and Q's mouse controls
 * Left click - interact
 * O - sprint
 * 
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class PlayerController : MonoBehaviour {
  public bool useMouseLock = true;
  bool mouseIsLocked = true;

  public static PlayerController player;

  public bool isStationary = true;
  public enum State {
    walking, sprinting
  }
  private State _state;
  [HideInInspector]
  public State state {
    get { return _state; }
    set {
      PlayerStateChange(value);
      _state = value;
    }
  }

  // Object references
  private InputDevice device;
  private Camera headCamera;
  private AudioSource audioSource;
  private Rigidbody body;

  // Movement variables
  private float currentSpeed;
  private float targetSpeed;

  // Rotation variables
  private Vector3 moveDirection = Vector3.zero;
  private float rotationY = 0f;
  private float rotationYDelta = 0f;
  private Vector3 xRotation = Vector3.zero;
  [HideInInspector]
  public Vector3 yRotation = Vector3.zero;
  private int sensitivity = 5;

  // Public variables to be changed in the inspector
  public float walkSpeed;
  public float sprintSpeed;
  public float acceleration;
  public float rotateSpeed;
  public float minimumY = -60f;
  public float maximumY = 60f;
  public GameObject camPrefab;

  // Public variables accessible by other classes
  [HideInInspector]
  public bool canInteract = false;
  [HideInInspector]
  public GameObject interactiveObj;
  [HideInInspector]
  public static bool debugControls = false;
  [HideInInspector]
  public static bool mouseMovement = false;
  [HideInInspector]
  public GameObject cam;

  public List<AudioClip> footsteps;
  public List<AudioClip> runFootsteps;
  int currentFootstep = 0;

  void Awake() {
    player = this;
    cam = Camera.main.gameObject;
    if (cam == null)
      cam = Instantiate(camPrefab, transform.position, Quaternion.identity) as GameObject;
    headCamera = cam.GetComponent<Camera>();
    audioSource = GetComponent<AudioSource>();
    body = GetComponent<Rigidbody>();


    state = State.walking;
    currentSpeed = walkSpeed;
    targetSpeed = walkSpeed;
  }

  void Start() {
    device = InputManager.ActiveDevice;
    foreach (var Device in InputManager.Devices) {
      if (Device.Name.Contains("Unknown")) {
        debugControls = true;
      }
    }
    if (InputManager.Devices.Count == 0) {
      debugControls = true;
    }

    if (debugControls) {
      Debug.Log ("Debug controls active");
      mouseMovement = true;
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }
  }

  void Update() {
    if (PauseScript.GamePaused) return;

    if (mouseMovement) {
      if (Input.GetKeyDown(KeyCode.Tab)) {
        mouseIsLocked = !mouseIsLocked;
      }
      if (mouseIsLocked) {
        Cursor.lockState = CursorLockMode.Locked;
      } else {
        Cursor.lockState = CursorLockMode.None;
      }
    } else {
      Cursor.lockState = CursorLockMode.None;
    }
      
    if (debugControls) {
      SetMoveDirectionDebug();
      SetLookDirectionDebug();
      CheckDebugStates();
      return;
    }

    SetMoveDirection();
    SetLookDirection();

    // Change states based on controller input
    if (device.Action3.WasPressed) { // X button on Xbox
      if (state == State.sprinting)
        state = State.walking;
      else
        state = State.sprinting;
    }
    if (device.Action1.WasPressed) { // A button on Xbox
      Interact();
    }

    // Stop sprinting if walking anywhere between sideways and backwards
    if (state == State.sprinting) {
      float angle = Vector3.Angle(transform.forward, moveDirection);
      if (angle >= 90f)
        state = State.walking;
    }
    //Debug.Log("Facing: " + transform.forward);
    //Debug.Log("Moving: " + moveDirection);
  }

  void FixedUpdate() {
    if (PauseScript.GamePaused) return;

    float dt = Time.fixedDeltaTime;
    Move(dt);
    Look(dt);
  }

  void CheckDebugStates() {
    if (Input.GetKeyDown(KeyCode.Tab)) // Switch between Stan and Q mouse controls
    {
      mouseMovement = !mouseMovement;
      if (mouseMovement)
      {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
      }
      else
      {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
      }
    }
    if (Input.GetKeyDown(KeyCode.O)) // Sprint
    {
      if (state == State.sprinting)
        state = State.walking;
      else
        state = State.sprinting;
    }

    // Cannot use mouse clicks when Q has control
    if (!mouseMovement) return;

    if (Input.GetKeyDown(KeyCode.Mouse0) || (debugControls && Input.GetKeyDown(KeyCode.U))) // Interact
    {
      Interact();
    }
  }

  // Sets the player's movement direction based on controller left stick input
  // Called in update, so controller input is registered as quickly as possible
  void SetMoveDirection() {
    // Get the input vector from left analog stick
    moveDirection.x = device.LeftStickX.Value;
    moveDirection.z = device.LeftStickY.Value;

    moveDirection = transform.TransformDirection(moveDirection);

    if (moveDirection != Vector3.zero)
    {     
      // Get the length of the direction vector and then normalize it
      // Dividing by the length is cheaper than normalizing
      float directionLength = moveDirection.magnitude;
      moveDirection = moveDirection / directionLength;

      // Make sure the length is no bigger than 1
      directionLength = Mathf.Min(1, directionLength);

      // Make the input vector more sensitive towards the extremes and less sensitive in the middle
      // This makes it easier to control slow speeds when using analog sticks
      directionLength = directionLength * directionLength;

      // Multiply the normalized direction vector by the modified length
      moveDirection = moveDirection * directionLength;

      AdjustSoundConstants();
    }
    else
    {
      if (state == State.sprinting)
        state = State.walking;
    }
  }

  void SetMoveDirectionDebug() {
    moveDirection.x = Input.GetAxis("Horizontal");
    moveDirection.z = Input.GetAxis("Vertical");

    moveDirection = transform.TransformDirection(moveDirection);

    if (moveDirection != Vector3.zero)
    {
      // Get the length of the direction vector and then normalize it
      // Dividing by the length is cheaper than normalizing
      float directionLength = moveDirection.magnitude;
      moveDirection = moveDirection / directionLength;

      // Make sure the length is no bigger than 1
      directionLength = Mathf.Min(1, directionLength);

      // Make the input vector more sensitive towards the extremes and less sensitive in the middle
      // This makes it easier to control slow speeds when using analog sticks
      directionLength = directionLength * directionLength;

      // Multiply the normalized direction vector by the modified length
      moveDirection = moveDirection * directionLength;

      AdjustSoundConstants();
    }
    else
    {
      if (state == State.sprinting)
        state = State.walking;
    }
  }

  // Sets the direction for the player to look in, based on the controller's right stick
  void SetLookDirection()
  {
    return;
    // Chanage x rotation based on right stick input
    xRotation.y = device.RightStickX.Value * rotateSpeed * sensitivity;

    // Increment y rotation by right stick input
    rotationYDelta = device.RightStickY.Value * rotateSpeed * sensitivity * 0.75f;
  }

  void SetLookDirectionDebug()
  {
    return;
    if (!mouseMovement)
    {
      rotationYDelta = 0f;
      return;
    }

    xRotation.y += Input.GetAxis("Mouse X") * rotateSpeed * sensitivity;
    rotationYDelta = Input.GetAxis("Mouse Y") * rotateSpeed * sensitivity * 2f;
  }

  // Moves the player on a fixed interval
  void Move(float dt)
  {
    return;
    currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);
    body.velocity = (moveDirection * currentSpeed * dt);
    isStationary = (moveDirection == Vector3.zero);
  }

  // Rotates the player and camera on a fixed interval
  void Look(float dt)
  {
    return;
    // Calculate player's x rotation based on delta time
    xRotation.y *= dt;

    // Calculate player's y rotation based on delta time
    rotationY += rotationYDelta * dt;
    rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
    yRotation.x = -rotationY;
    yRotation.y = headCamera.transform.eulerAngles.y;

    // Set player x rotation and camera y rotation
    transform.Rotate(xRotation);
    headCamera.transform.eulerAngles = yRotation;
  }

  // Called whenever the player's "state" variable gets changed
  // Handles movement speed changes and crouching
  void PlayerStateChange(State newState)
  {
    return;
    // Don't make any changes if the state was not changed
    if (state == newState) return;

    if (state == State.walking && newState == State.sprinting)
    {
      currentSpeed = walkSpeed;
      targetSpeed = sprintSpeed;
      acceleration = Mathf.Abs(acceleration);
    }
    else if (state == State.sprinting && newState == State.walking)
    {
      targetSpeed = walkSpeed;
      acceleration = Mathf.Abs(acceleration) * -1f;
    }
    else
      Debug.LogError("Invalid player state change");
  }

  void Interact()
  {
    return;
    if (canInteract) {
      interactiveObj.SendMessage("Interact");
    }
  }

  float IncrementTowards(float current, float target, float accel)
  {
    if (current == target) return current;

    if (Mathf.Sign(accel) > 0f)
    {
      return Mathf.Clamp(current + accel, current, target);
    }
    else
    {
      return Mathf.Clamp(current + accel, target, current);
    }
  }

  void AdjustSoundConstants() {
    return;
    if (state == State.walking) {
      if (!audioSource.isPlaying) {
        if (++currentFootstep >= footsteps.Count) {
          currentFootstep = 0;
        }
        audioSource.clip = footsteps[currentFootstep];
        audioSource.volume = 0.02f;
        audioSource.Play();
      }
    } else if (state == State.sprinting) {
      if (!audioSource.isPlaying) {
        if (++currentFootstep >= runFootsteps.Count) {
          currentFootstep = 0;
        }
        audioSource.clip = runFootsteps[currentFootstep];
        audioSource.volume = 1f;
        audioSource.Play();
      }
    }
  }

  // 1 <= val <= 10
  public void SetLookSensitivity(int val)
  {
    sensitivity = val;
  }
}