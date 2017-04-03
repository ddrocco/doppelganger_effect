using UnityEngine;
using System.Collections;

public class DebugLogging : MonoBehaviour {
  static DebugLogging _instance;
  public static DebugLogging INSTANCE {
    get {
      if (_instance != null) {
        return _instance;
      }
      _instance = GameObject.FindObjectOfType<DebugLogging> ();
      if (_instance != null) {
        return _instance;
      }
      MasterInit master_init_instance = GameObject.FindObjectOfType<MasterInit>();
      _instance = master_init_instance.gameObject.AddComponent<DebugLogging> ();
      return _instance;
    }
  }

  public static void PrintPlayerInput(PlayerInput input) {
    if (!DebugConstants.ALLOW_PRINT_PLAYER_INPUT) {
      return;
    }
    Debug.Log ("Move: " + input.movement + "\nRotate: " + input.LRRotation + ", " + input.UDRotation +
      "\nInteract: " + input.interact + "\nSprint: " + input.sprint + "\nPause: " + input.trigger_pause);
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
}
