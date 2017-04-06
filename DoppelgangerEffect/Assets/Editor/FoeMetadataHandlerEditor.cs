using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FoeMetadataHandler))]
public class FoeMetadataHandlerEditor : Editor {

  public override void OnInspectorGUI() {
    DrawDefaultInspector ();

    FoeMetadataHandler script = (FoeMetadataHandler)target;

    GUILayout.Box ("Next Loc State: " + script.NEXT_LOC_STATE.pos.ToString ());
    GUILayout.Box ("Last Loc State: " + script.LAST_LOC_STATE.pos.ToString ());
    GUILayout.Box ("Next Room: " + script.NEXT_LOC_STATE.room_id.ToString ());

    GUILayout.Box ("POS: " + (
      script.NEXT_LOC_STATE.pos * PlayerStateHistory.STEP_TIME_RATIO +
      script.LAST_LOC_STATE.pos * (1 - PlayerStateHistory.STEP_TIME_RATIO)).ToString ());
  }
}
