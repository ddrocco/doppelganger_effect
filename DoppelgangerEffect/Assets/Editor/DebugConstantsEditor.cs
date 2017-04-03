using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DebugConstants))]
public class DebugConstantsEditor : Editor {

  public override void OnInspectorGUI() {
    DrawDefaultInspector ();
    // DebugConstants debug_constants_script = (DebugConstants)target;

    if (GUILayout.Button ("UpdateColors")) {
      RoomCollection._main.ColorRooms();
    }
  }
}
