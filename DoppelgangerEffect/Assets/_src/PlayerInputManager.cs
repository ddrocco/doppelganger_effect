using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInputManager : MonoBehaviour {
  public static PlayerInputManager _main;

  static List<PlayerInteractor> _interactors;
  public static List<PlayerInteractor> INTERACTORS {
    get {
      return _interactors;
    }
  }

  private MovementType _movement_type;
  [HideInInspector]
  public MovementType movement_type {
    get { return _movement_type; }
    set {
      if (value == _movement_type) {
        return;
      }
      // TODO(ddrocco): Do MOVEMENT TYPE CHANGE STUFF for (value);
      _movement_type = value;
    }
  }

  private static MovementState _target_state = new MovementState();
  public static MovementState TARGET_STATE {
    get {
      return _target_state;
    }
  }

  public static MovementState CURRENT_STATE {
    get {
      Rigidbody body = PlayerPhysicsController.RIGIDBODY;
      return new MovementState(body.velocity, body.rotation.eulerAngles.x, body.rotation.eulerAngles.y);
    }
  }

  void Awake() {
    _interactors = new List<PlayerInteractor> ();
    foreach (PlayerInteractor interactor in FindObjectsOfType<PlayerInteractor>()) {
      _interactors.Add (interactor);
    }
  }

  /* 
   * Update-level functions 
  */

  void Update() {
    UpdateTargetFromInput (InputMapper.MAPPED_INPUT);
    GetInteraction (InputMapper.MAPPED_INPUT);

    if (DebugConstants.ENABLE_PLAYER_PHYSICS_MESSAGING) {
      string msg = "Movement (curr, target): " + CURRENT_STATE.movement + " " + TARGET_STATE.movement +
                   "\nRotation x (curr, target): " + CURRENT_STATE.rotationX + " " + TARGET_STATE.rotationX +
                   "\nRotation y (curr, target): " + CURRENT_STATE.rotationY + " " + TARGET_STATE.rotationY +
                   "\nInput: " + InputMapper.MAPPED_INPUT.movement + " " + InputMapper.MAPPED_INPUT.LRRotation + " " + InputMapper.MAPPED_INPUT.UDRotation +
                   "\n[DebugConstants.ENABLE_PLAYER_PHYSICS_MESSAGING]";
      Debug.Log(msg);
      DebugText.player_target_movement_text = msg;
    }
  }

  void UpdateTargetFromInput(PlayerInput input) {
    MovementState new_target_movement_state = new MovementState ();
    new_target_movement_state.movement = GetMovementVector(input.movement);
    /*
     * new_target_movement_state.rotationX = Mathf.Clamp(
      CURRENT_STATE.rotationX + 360f - input.UDRotation,
      ConfigurableConstants.PLAYER_LOOK_ANGLE_MIN + 360f, ConfigurableConstants.PLAYER_LOOK_ANGLE_MAX + 360f);
    */
    new_target_movement_state.rotationX = Lib.AngleClamp180(
      CURRENT_STATE.rotationX - input.UDRotation,
      ConfigurableConstants.PLAYER_LOOK_ANGLE_MIN, ConfigurableConstants.PLAYER_LOOK_ANGLE_MAX);
    new_target_movement_state.rotationY = CURRENT_STATE.rotationY + input.LRRotation;
    _target_state = new_target_movement_state;
  }

  void GetInteraction(PlayerInput input) {
    if (input.interact) {
      INTERACTORS [0].Toggle ();
    }
  }

  /* 
   * Helper methods
   */

  // Sets the player's movement direction based on controller left stick input
  // Called in update, so controller input is registered as quickly as possible
  Vector3 GetMovementVector(Vector3 move_direction) {
    move_direction = PlayerPhysicsController.RIGIDBODY.transform.TransformDirection(move_direction);
    //TODO(ddrocco): Fix movement direction to eliminate Y.

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
      if (movement_type == MovementType.SPRINTING)
        movement_type = MovementType.WALKING;
    }
    return Vector3.zero;
  }
}
