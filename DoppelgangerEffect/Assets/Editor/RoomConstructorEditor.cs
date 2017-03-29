using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RoomConstructor))]
public class RoomConstructorEditor : Editor {

  public override void OnInspectorGUI() {
    DrawDefaultInspector ();
    RoomConstructor room_editor_script = (RoomConstructor)target;

    string load_blueprint_button_text;
    if (room_editor_script.filename.Length > 0) {
      load_blueprint_button_text = "Load Blueprints from " + room_editor_script.filename + ".json";
    } else {
      load_blueprint_button_text = "(Load unavailable; must have filename)";
    }
    if (GUILayout.Button (load_blueprint_button_text)) {
      if (room_editor_script.filename.Length > 0) {
        room_editor_script.LoadBlueprint ();
      }
    }
      
    string construct_room_button_text;
    if (room_editor_script.blueprint != null) {
      construct_room_button_text = "Construct Room From Blueprints";
    } else {
      construct_room_button_text = "(Construct unavailable; must have blueprint)";
    }
    if (GUILayout.Button (construct_room_button_text)) {
      if (room_editor_script.blueprint != null) {
        room_editor_script.Construct ();
      }
    }

    string save_blueprint_button_text;
    if (room_editor_script.filename.Length > 0 && room_editor_script.blueprint != null) {
      save_blueprint_button_text = "Save Blueprints to " + room_editor_script.filename + ".json";
    } else {
      save_blueprint_button_text = "(Save unavailable; must have filename and blueprint)";
    }
    if (GUILayout.Button (save_blueprint_button_text)) {
      if (room_editor_script.filename.Length > 0 && room_editor_script.blueprint != null) {
        room_editor_script.SaveBlueprint();
      }
    }
  }
}
