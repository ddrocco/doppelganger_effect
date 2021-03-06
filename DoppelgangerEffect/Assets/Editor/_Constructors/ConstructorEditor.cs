using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Constructor))]
public class ConstructorEditor : Editor {

  public override void OnInspectorGUI() {
    DrawDefaultInspector ();

    Constructor constructor_script = (Constructor)target;

    string load_blueprint_button_text;
    if (constructor_script.filename.Length > 0) {
      load_blueprint_button_text = "Load Blueprints from " + constructor_script.FULL_FILENAME + ".json";
    } else {
      load_blueprint_button_text = "(Load unavailable; must have filename)";
    }
    if (GUILayout.Button (load_blueprint_button_text)) {
      if (constructor_script.filename.Length > 0) {
        constructor_script.LoadBlueprint ();
      }
    }

    string construct_room_button_text;
    if (constructor_script.HasBlueprints()) {
      construct_room_button_text = "Construct Room From Blueprints";
    } else {
      construct_room_button_text = "(Construct unavailable; must have blueprint)";
    }
    if (GUILayout.Button (construct_room_button_text)) {
      if (constructor_script.HasBlueprints()) {
        constructor_script.Construct ();
      }
    }

    string save_blueprint_button_text;
    if (constructor_script.filename.Length > 0 && constructor_script.HasBlueprints()) {
      save_blueprint_button_text = "Save Blueprints to " + constructor_script.FULL_FILENAME + ".json";
    } else {
      save_blueprint_button_text = "(Save unavailable; must have filename and blueprint)";
    }
    if (GUILayout.Button (save_blueprint_button_text)) {
      if (constructor_script.filename.Length > 0 && constructor_script.HasBlueprints()) {
        constructor_script.SaveBlueprint();
      }
    }
  }
}
