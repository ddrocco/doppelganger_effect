using UnityEngine;
using System.Collections;

public class DebugLogging : MonoBehaviour {
  public static DebugLogging _main;

  public bool _ALLOW_PRINT_PLAYER_INPUT = true;
  public static bool ALLOW_PRINT_PLAYER_INPUT {
    get {
      return _main._ALLOW_PRINT_PLAYER_INPUT;
    }
    set {
      _main._ALLOW_PRINT_PLAYER_INPUT = value;
    }
  }
  public static void PrintPlayerInput(PlayerInput input) {
    if (!_main._ALLOW_PRINT_PLAYER_INPUT) {
      return;
    }
    Debug.Log ("Move: " + input.movement + "\nRotate: " + input.xRotation + ", " + input.yRotation +
      "\nInteract: " + input.interact + "\nSprint: " + input.sprint + "\nPause: " + input.trigger_pause);
  }

  public bool _ALLOW_PRINT_MOVEMENT_STATE = true;
  public static bool ALLOW_PRINT_MOVEMENT_STATE {
    get {
      return _main._ALLOW_PRINT_MOVEMENT_STATE;
    }
    set {
      _main._ALLOW_PRINT_MOVEMENT_STATE = value;
    }
  }
  public static void PrintMovementState(WorldspaceState state) {
    if (!_main._ALLOW_PRINT_MOVEMENT_STATE) {
      return;
    }
    Debug.Log (
      "Current Position: " + state.current_position +
      "\nCurrent Movement: " + state.current_movement +
      "\nTarget Movement: " + state.target_movement +
      "\nCurrentXAngle: " + state.current_rotationX +
      "\nTargetXAngle: " + state.target_rotationX +
      "\nCurrentYAngle: " + state.current_rotationY +
      "\nTargetYAngle: " + state.target_rotationY);
  }

  void Awake() {
    _main = this;
  }
}
