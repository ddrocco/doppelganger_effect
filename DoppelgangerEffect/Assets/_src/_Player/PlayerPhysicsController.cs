using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerPhysicsController : MonoBehaviour {
  public static PlayerPhysicsController INSTANCE;

  private MovementState current_movement_state;

  // Object references
  private Camera headCamera;
  private AudioSource audioSource;
  private static Rigidbody _rigidbody;
  public static Rigidbody RIGIDBODY {
    get {
      return _rigidbody;
    }
  }

  // Public variables to be changed in the inspector
  public GameObject camPrefab;

  // Public variables accessible by other classes
  [HideInInspector]
  public GameObject cam;

  public List<AudioClip> footsteps;
  public List<AudioClip> runFootsteps;
  int currentFootstep = 0;

  private static MovementState _delta_state = new MovementState();
  public static MovementState DELTA_STATE {
    get {
      return _delta_state;
    }
  }

  /*
   * AWAKE METHODS
   */

  void Awake() {
    INSTANCE = this;
    cam = Camera.main.gameObject;
    if (cam == null)
      cam = Instantiate(camPrefab, transform.position, Quaternion.identity) as GameObject;
    headCamera = cam.GetComponent<Camera>();
    audioSource = GetComponent<AudioSource>();
    _rigidbody = GetComponent<Rigidbody>();
    _rigidbody.useGravity = false;
  }
    
  /* 
   * FIXEDUPDATE METHODS
  */

  void FixedUpdate() {
    if (PauseScript.GamePaused) return;

    float dt = Time.fixedDeltaTime;
    Move(dt);
    Look(dt);
    AdjustHeight (dt);
  }

  // Moves the player on a fixed interval
  void Move(float dt) {
    Vector3 target_delta_v = PlayerInputManager.TARGET_STATE.movement - PlayerInputManager.CURRENT_STATE.movement;
    float adjusted_acceleration_rate = Constants.PLAYER_ACCELERATION_RATE * dt;
    if (target_delta_v.magnitude < adjusted_acceleration_rate) {
      _delta_state.movement = PlayerInputManager.TARGET_STATE.movement;
    } else {
      _delta_state.movement += target_delta_v.normalized * adjusted_acceleration_rate;
    }
    RIGIDBODY.velocity = DELTA_STATE.movement;
    // TODO(ddrocco): Set this as a getter or setter
    // or actually just figure out how to work with walls, too.  This is an intention, not anything set in stone.
    // Gotta get stairs to work too.
  }

  // Rotates the player and camera on a fixed interval
  void Look(float dt) {
    float adjusted_lr_rotation_rate = Constants.PLAYER_LR_ROTATION_RATE * dt;
    float adjusted_ud_rotation_rate = Constants.PLAYER_UD_ROTATION_RATE * dt;
    float new_rotation_x = Mathf.Lerp (PlayerInputManager.CURRENT_STATE.rotationX, PlayerInputManager.TARGET_STATE.rotationX, adjusted_ud_rotation_rate);
    float new_rotation_y = Mathf.Lerp (PlayerInputManager.CURRENT_STATE.rotationY, PlayerInputManager.TARGET_STATE.rotationY, adjusted_lr_rotation_rate);
    _delta_state.rotationX = new_rotation_x - PlayerInputManager.CURRENT_STATE.rotationX;
    _delta_state.rotationY = new_rotation_y - PlayerInputManager.CURRENT_STATE.rotationY;
    transform.rotation = Quaternion.Euler(new Vector3(new_rotation_x, new_rotation_y, 0f));
  }

  void AdjustHeight(float dt) {
    transform.position += PlayerHeightHandler.PLAYER_TARGET_HEIGHT_ADJUSTMENT * Constants.PLAYER_HEIGHT_ACCELERATION * dt * Vector3.up;
  }

  // Presently unused.
  void Interact()
  {
    return;
    //if (canInteract) {
    //  interactiveObj.SendMessage("Interact");
    //}
  }

  /*
   * UPDATE METHODS
   * Presently unused.
  */

  void Update() {
    if (PauseScript.GamePaused)
      return;
    AdjustSoundConstants ();
  }

  void AdjustSoundConstants() {
    return;
    /*if (_movement_type = MovementType.WALKING) {
      if (!audioSource.isPlaying) {
        if (++currentFootstep >= footsteps.Count) {
          currentFootstep = 0;
        }
        audioSource.clip = footsteps[currentFootstep];
        audioSource.volume = 0.02f;
        audioSource.Play();
      }
    } else if (_movement_type = MovementType.SPRINTING) {
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
}