using UnityEngine;
using System.Collections;

public class DebugLogging : MonoBehaviour {
  public static DebugLogging _main;

  public static void PrintPlayerInput(PlayerInput input) {
    if (!DebugConstants.ALLOW_PRINT_PLAYER_INPUT) {
      return;
    }
    Debug.Log ("Move: " + input.movement + "\nRotate: " + input.xRotation + ", " + input.yRotation +
      "\nInteract: " + input.interact + "\nSprint: " + input.sprint + "\nPause: " + input.trigger_pause);
  }
    
  public static void PrintWorldspaceState(WorldspaceState state) {
    if (!DebugConstants.ALLOW_PRINT_WORLDSPACE_STATE) {
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

  public static void PrintLocationState(LocationState state) {
    if (!DebugConstants.ALLOW_PRINT_LOCATION_STATE) {
      return;
    }
    Debug.Log ("LOCATIONSTATE:" +
      "\nPosition: " + state.pos +
      "\nFacing: " + state.facing.eulerAngles +
      "\nRoom: " + state.room_id);
  }

  public static void DrawDebugRay(Vector3 position, Vector3 direction) {
    if (!DebugConstants.ALLOW_DEBUG_RAYS) {
      return;
    }
    Debug.DrawRay (position, direction);
  }

  void Awake() {
    _main = this;
  }
}
