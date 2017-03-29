using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MovementState {
  WALKING,
  SPRINTING
}

public class PlayerController : MonoBehaviour {
  private PlayerInput _active_input;
  private WorldspaceState _worldspace_state;
  private MovementState _movement_state;

  public static PlayerController player;
  [HideInInspector]
  public MovementState movement_movement_state {
    get { return _movement_state; }
    set {
      Player_movement_stateChange(value);
      _movement_state = value;
    }
  }

  // Object references
  private Camera headCamera;
  private AudioSource audioSource;
  private Rigidbody body;

  // Public variables to be changed in the inspector
  public GameObject camPrefab;

  // Public variables accessible by other classes
  [HideInInspector]
  public GameObject cam;

  public List<AudioClip> footsteps;
  public List<AudioClip> runFootsteps;
  int currentFootstep = 0;

  /*
   * AWAKE METHODS
   */

  void Awake() {
    player = this;
    cam = Camera.main.gameObject;
    if (cam == null)
      cam = Instantiate(camPrefab, transform.position, Quaternion.identity) as GameObject;
    headCamera = cam.GetComponent<Camera>();
    audioSource = GetComponent<AudioSource>();
    body = GetComponent<Rigidbody>();

    _movement_state = MovementState.WALKING;
  }

  /*
   * UPDATE METHODS
  */

  void Update() {
    if (PauseScript.GamePaused)
      return;

    // Stop sprinting if walking anywhere between sideways and backwards
    if (_movement_state == MovementState.SPRINTING) {
      float angle = Vector3.Angle (transform.forward, _active_input.movement);
      if (angle >= 90f)
        _movement_state = MovementState.WALKING;
    }

    AdjustSoundConstants ();
    DebugLogging.PrintWorldspaceState (_worldspace_state);
  }

  // Called passively (by another obj) at the Input step.
  public void UpdateInput(PlayerInput input) {
    _active_input = input;
    _worldspace_state.target_movement = GetMovementVector(_active_input.movement);
    _worldspace_state.target_rotationX = _worldspace_state.current_rotationX + _active_input.xRotation;
    _worldspace_state.target_rotationY = Mathf.Clamp(
      _worldspace_state.current_rotationY + _active_input.yRotation,
      ConfigurableConstants.PLAYER_LOOK_ANGLE_MIN, ConfigurableConstants.PLAYER_LOOK_ANGLE_MAX);
  }

  // Sets the player's movement direction based on controller left stick input
  // Called in update, so controller input is registered as quickly as possible
  Vector3 GetMovementVector(Vector3 move_direction) {
    move_direction = transform.TransformDirection(move_direction);

    if (move_direction != Vector3.zero) {     
      // Get the length of the direction vector and then normalize it
      // Dividing by the length is cheaper than normalizing
      float directionLength = move_direction.magnitude;
      move_direction = move_direction / directionLength;

      // Make sure the length is no bigger than 1
      directionLength = Mathf.Min(1, directionLength);

      // Make the input vector more sensitive towards the extremes and less sensitive in the middle
      // This makes it easier to control slow speeds when using analog sticks
      directionLength = directionLength * directionLength;

      // Multiply the normalized direction vector by the modified length
      move_direction = move_direction * directionLength;
      return move_direction;
    }
    else {
      if (_movement_state == MovementState.SPRINTING)
        _movement_state = MovementState.WALKING;
    }
    return Vector3.zero;
  }

  void AdjustSoundConstants() {
    return;
    /*if (_movement_state == MovementState.WALKING) {
      if (!audioSource.isPlaying) {
        if (++currentFootstep >= footsteps.Count) {
          currentFootstep = 0;
        }
        audioSource.clip = footsteps[currentFootstep];
        audioSource.volume = 0.02f;
        audioSource.Play();
      }
    } else if (_movement_state == MovementState.SPRINTING) {
      if (!audioSource.isPlaying) {
        if (++currentFootstep >= runFootsteps.Count) {
          currentFootstep = 0;
        }
        audioSource.clip = runFootsteps[currentFootstep];
        audioSource.volume = 1f;
        audioSource.Play();
      }
    }*/
  }
    
  /* 
   * FIXEDUPDATE METHODS
  */

  void FixedUpdate() {
    if (PauseScript.GamePaused) return;

    float dt = Time.fixedDeltaTime;
    Move(dt);
    Look(dt);
  }

  // Moves the player on a fixed interval
  void Move(float dt) {
    Vector3 target_delta_v = _worldspace_state.target_movement - _worldspace_state.current_movement;
    float adjusted_acceleration_rate = Constants.PLAYER_ACCELERATION_RATE * dt;
    if (target_delta_v.magnitude < adjusted_acceleration_rate) {
      _worldspace_state.current_movement = _worldspace_state.target_movement;
    } else {
      _worldspace_state.current_movement += target_delta_v.normalized * adjusted_acceleration_rate;
    }
    // TODO(ddrocco): Set this as a getter or setter
    // or actually just figure out how to work with walls, too.  This is an intention, not anything set in stone.
    // Gotta get stairs to work too.
    body.velocity = _worldspace_state.current_movement;
  }

  // Rotates the player and camera on a fixed interval
  void Look(float dt) {

    Debug.Log(_worldspace_state.current_rotationX + " " + _worldspace_state.target_rotationX);
    float adjusted_rotation_rate = Constants.PLAYER_ROTATION_RATE * dt;
    _worldspace_state.current_rotationX = Mathf.Lerp (_worldspace_state.current_rotationX,
                                                    _worldspace_state.target_rotationX, adjusted_rotation_rate);
    _worldspace_state.current_rotationY = Mathf.Lerp (_worldspace_state.current_rotationY,
                                                    _worldspace_state.target_rotationY, adjusted_rotation_rate);
    Vector3 current_euler = transform.eulerAngles;
    Debug.Log("EULER" + transform.eulerAngles + " " + new Vector3(0f, _worldspace_state.current_rotationX - current_euler.x,
      _worldspace_state.current_rotationY - current_euler.y));
    transform.Rotate(new Vector3(0f, _worldspace_state.current_rotationX - current_euler.x,
                                 _worldspace_state.current_rotationY - current_euler.y));
  }

  // Called whenever the player's "_movement_state" variable gets changed
  // Handles movement speed changes and crouching
  void Player_movement_stateChange(MovementState new_movement_state) {
    // Don't make any changes if the _movement_state was not changed
    if (_movement_state == new_movement_state) return;
    if (_movement_state == MovementState.WALKING && new_movement_state == MovementState.SPRINTING) {
      _worldspace_state.current_movement = _worldspace_state.current_movement.normalized * Constants.PLAYER_WALKSPEED;
      _worldspace_state.target_movement = _worldspace_state.current_movement.normalized * Constants.PLAYER_RUNSPEED;
    } else if (_movement_state == MovementState.SPRINTING && new_movement_state == MovementState.WALKING) {
      _worldspace_state.target_movement = _worldspace_state.current_movement.normalized * Constants.PLAYER_WALKSPEED;
    } else {
      Debug.LogError("Invalid player _movement_state change");
    }
  }

  void Interact()
  {
    return;
    //if (canInteract) {
    //  interactiveObj.SendMessage("Interact");
    //}
  }
}