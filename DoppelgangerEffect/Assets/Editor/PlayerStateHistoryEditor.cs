using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlayerStateHistory))]
public class PlayerStateHistoryEditor : Editor {

  public override void OnInspectorGUI() {
    DrawDefaultInspector ();

    PlayerStateHistory script = (PlayerStateHistory)target;

    GUILayout.Box ("Steps: " + PlayerStateHistory.CURRENT_STEP.ToString());
    GUILayout.Box ("Foes: " + PlayerStateHistory.NUMBER_OF_FOES.ToString());
    GUILayout.Box ("Current Occupied Rooms: " + Player.CURRENT_OCCUPIED_ROOM.ToString());
    GUILayout.Box ("Time Ratio: " + PlayerStateHistory.STEP_TIME_RATIO.ToString());
  }
}
