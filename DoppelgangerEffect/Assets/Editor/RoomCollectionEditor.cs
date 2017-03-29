using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RoomCollection))]
public class RoomCollectionEditor : Editor {

  public override void OnInspectorGUI() {
    DrawDefaultInspector ();
    RoomCollection room_collection_script = (RoomCollection)target;

    string reload_rooms_button_text;
    reload_rooms_button_text = "Update Rooms";
    if (GUILayout.Button (reload_rooms_button_text)) {
      room_collection_script.UpdateRooms();
    }
  }
}
