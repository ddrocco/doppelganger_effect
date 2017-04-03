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

    if (GUILayout.Button ("Print Room Adjacencies")) {
      string msg = "Room adjacencies:";
      foreach (RoomObject room in RoomCollection.ROOMS) {
        msg += "\nRoom" + room.id.ToString () + ": [";
        bool first_element = true;
        foreach (int i in room.adjacent_rooms) {
          if (!first_element) {
            msg += ", ";
          } else {
            first_element = false;
          }
          msg += i.ToString ();
        }
        msg += "]";
      }
      Debug.Log (msg);
    }

    if (GUILayout.Button ("Recalculate Room Adjacencies")) {
      room_collection_script.DetectRoomAdjacencies ();
    }
  }
}
