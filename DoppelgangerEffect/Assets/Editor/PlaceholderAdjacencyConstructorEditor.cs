using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlaceholderAdjacencyConstructor))]
public class PlaceholderAdjacencyConstructorEditor : Editor {

  string err_msg = "";

  public override void OnInspectorGUI() {
    DrawDefaultInspector ();
    PlaceholderAdjacencyConstructor pac_script = (PlaceholderAdjacencyConstructor)target;

    if (GUILayout.Button ("Detect Adjacencies")) {
      pac_script.PlaceholderAdjacencyDetection();
      if (pac_script.first != null && pac_script.second != null) {
        err_msg = "";
      } else {
        err_msg = "PAC failed.";
      }
    }

    if (pac_script.first != null && pac_script.second != null) {
      GUILayout.Box ("Adjacent IDs: (" + pac_script.first.id.ToString () + ", " + pac_script.second.id.ToString ());
    }
    if (err_msg != "") {
      GUILayout.Box (err_msg);
    }
  }
}
