using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeRoomConstructor : Constructor {
  public CubeRoomBlueprints blueprints;

  public override bool HasBlueprints() {
    return (blueprints != null);
  }

  public override void LoadBlueprint() {
    string blueprint_json = Parser.LoadResourceTextfile (Parser.BLUEPRINTS_SHORT_PATH, FULL_FILENAME);
    if (blueprints == null) {
      blueprints = gameObject.AddComponent<CubeRoomBlueprints> ();
    }
    blueprints.contents = JsonUtility.FromJson<CubeLayout> (blueprint_json);
    Debug.Log ("Loaded blueprints from " + Parser.BLUEPRINTS_SHORT_PATH + FULL_FILENAME + ".json");
  }

  public override void Construct() {
    RoomCollection.INSTANCE.DestroyAllRooms ();
    int i = 0;
    foreach (CubeRoom room in blueprints.contents.rooms) {
      GameObject room_gameobj = GameObject.Instantiate (BuildingPrefabs.EMPTY_OBJECT);
      room_gameobj.transform.SetParent (RoomCollection.INSTANCE.transform);
      room_gameobj.name = "Room" + i.ToString ();
      RoomObject room_component = room_gameobj.AddComponent<RoomObject> ();
      room_component.id = i;
      RoomCollection.ROOMS.Add (room_component);
      int j = 0;
      foreach (FloorTile tile in room.tiles) {
        Vector3 position = new Vector3 (tile.position.x, -1f, tile.position.y);
        Vector3 dimensions = new Vector3 (tile.dimensions.x, 1f, tile.dimensions.y);
        GameObject tile_gameobj = GameObject.Instantiate (
          BuildingPrefabs.FLOOR_TILE, position, Quaternion.identity, room_gameobj.transform) as GameObject;
        tile_gameobj.name = "FloorTile" + i.ToString() + "-" + j.ToString ();
        tile_gameobj.transform.localScale = dimensions;
        ++j;
      }
      ++i;
    }
    RoomCollection.INSTANCE.UpdateRooms ();
    Debug.Log ("Constructed room from blueprints");
  }

  public override void SaveBlueprint() {
    string blueprint_json = JsonUtility.ToJson (blueprints.contents);
    Parser.SaveResourceTextfile (blueprint_json, Parser.BLUEPRINTS_FULL_PATH, FULL_FILENAME + ".json");
    Debug.Log ("Saved blueprints to " + Parser.BLUEPRINTS_FULL_PATH + FULL_FILENAME + ".json");
    Debug.Log ("Saved: \n" + blueprint_json);
  }

  void Start() {
    if (RoomCollection.ROOMS.Count == 0) {
      Construct ();
    }
  }
}
