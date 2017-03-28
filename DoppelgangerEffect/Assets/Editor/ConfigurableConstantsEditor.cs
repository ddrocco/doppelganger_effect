using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ConfigurableConstants))]
public class ConfigurableConstantsEditor : Editor {

  public override void OnInspectorGUI() {
    DrawDefaultInspector ();
    ConfigurableConstants config_constants_script = (ConfigurableConstants)target;
    if (GUILayout.Button ("Toggle Hide Mouse (Currently " + ConfigurableConstants.MOUSE_HIDE_ENABLED + ")")) {
      config_constants_script.ToggleShowMouse ();
    }
    if (GUILayout.Button ("Toggle Lock Mouse (Currently " + ConfigurableConstants.MOUSE_LOCK_ENABLED + ")")) {
      config_constants_script.ToggleLockMouse ();
    }
  }
}
