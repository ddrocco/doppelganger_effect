using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RoomObject))]
public class RoomObjectEditor : Editor {

  public override void OnInspectorGUI() {
    DrawDefaultInspector ();

    RoomObject room_object_script = (RoomObject)target;

    GUILayout.Box ("ID: " + room_object_script.id.ToString());
  }
}
