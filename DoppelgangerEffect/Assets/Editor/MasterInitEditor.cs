using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MasterInit))]
public class MasterInitEditor : Editor {

  public override void OnInspectorGUI() {
    DrawDefaultInspector ();
    MasterInit master_init_script = (MasterInit)target;

    if (GUILayout.Button ("UpdateSingletons")) {
      master_init_script.UpdateSingletons();
    }
  }
}
