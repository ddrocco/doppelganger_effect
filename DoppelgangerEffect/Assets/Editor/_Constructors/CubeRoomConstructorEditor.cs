using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CubeRoomConstructor))]
public class CubeRoomConstructorEditor : ConstructorEditor {
  
  public override void OnInspectorGUI() {
    base.OnInspectorGUI ();

    CubeRoomConstructor constructor_script = (CubeRoomConstructor)target;

    string print_blueprints_msg;
    if (constructor_script.blueprints != null) {
      print_blueprints_msg = "Print Blueprints Contents to Console";
    } else {
      print_blueprints_msg = "Unavailable (Print Blueprints Contents to Console)";
    }
    if (GUILayout.Button (print_blueprints_msg) && constructor_script.blueprints != null) {
      string msg = "CubeRoomBlueprintsBlueprints (" + constructor_script.blueprints.rooms.Count.ToString() + " rooms):";
      int i = 0;
      foreach (CubeRoom room in constructor_script.blueprints.rooms) {
        msg += "\nRoom " + i.ToString () + "(" + room.tiles.Count.ToString () + " tiles):";
        foreach (FloorTile tile in room.tiles) {
          msg += "\n  --(" + tile.position.x.ToString () + ", " + tile.position.y.ToString () + "): ["
            + tile.dimensions.x.ToString () + " x " + tile.dimensions.y.ToString () + "]";
        }
        ++i;
      }
      Debug.Log (msg);
    }
  }
}