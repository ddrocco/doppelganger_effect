using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CubeRoomBlueprints))]
public class CubeRoomBlueprintsEditor : Editor {

  public override void OnInspectorGUI() {
    DrawDefaultInspector ();

    CubeRoomBlueprints blueprints_script = (CubeRoomBlueprints)target;

    string create_room_button_text = "Create new room at index " + blueprints_script.rooms.Count.ToString();
    if (GUILayout.Button (create_room_button_text)) {
      blueprints_script.CreateRoom ();
    }

    string remove_room_button_text = "Remove room at index " + blueprints_script.room_index.ToString();
    if (GUILayout.Button (remove_room_button_text)) {
      blueprints_script.RemoveRoom (blueprints_script.room_index);
    }

    string add_floor_tile_text = "Add Floor Tile ( "
      + blueprints_script.new_floor_tile_position.x.ToString () + ", "
      + blueprints_script.new_floor_tile_position.y.ToString () + "): ["
      + blueprints_script.new_floor_tile_dimensions.x.ToString () + ", "
      + blueprints_script.new_floor_tile_dimensions.y.ToString () + "] to Room "
      + blueprints_script.room_index.ToString();
    if (GUILayout.Button (add_floor_tile_text)) {
      blueprints_script.AddFloorTileToRoom (blueprints_script.room_index,
        blueprints_script.new_floor_tile_position, blueprints_script.new_floor_tile_dimensions);
    }
      
    string remove_floor_tile_text = "Remove floor tile at index "
      + blueprints_script.room_index.ToString() + " from Room " + blueprints_script.room_index.ToString();
    if (GUILayout.Button (remove_floor_tile_text)) {
      blueprints_script.RemoveFloorTileFromRoom (blueprints_script.room_index, blueprints_script.floor_tile_index);
    }
  }
}
