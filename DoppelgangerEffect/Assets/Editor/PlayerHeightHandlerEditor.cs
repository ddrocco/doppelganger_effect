using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlayerHeightHandler))]
public class PlayerHeightHandlerEditor : Editor {

  public override void OnInspectorGUI() {
    DrawDefaultInspector ();

    PlayerHeightHandler script = (PlayerHeightHandler)target;

    string detector_info = "Detectors: " + script.DETECTOR_COUNT.ToString ();
    foreach (float h in script.DETECTOR_HEIGHTS) {
      detector_info += "\n" + h.ToString ();
    }
    GUILayout.Box (detector_info);

    if (GUILayout.Button ("Generate Simple 3x3")) {
      script.SetUp ();
    }

    GUILayout.Box ("Target Y: " + PlayerHeightHandler.PLAYER_TARGET_HEIGHT_ADJUSTMENT.ToString());
  }
}
