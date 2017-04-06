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
    ConstructRooms ();
    ConstructDoors ();
    RoomCollection.INSTANCE.UpdateRooms ();
  }

  public override void SaveBlueprint() {
    string blueprint_json = JsonUtility.ToJson (blueprints.contents);
    Parser.SaveResourceTextfile (blueprint_json, Parser.BLUEPRINTS_FULL_PATH, FULL_FILENAME + ".json");
    Debug.Log ("Saved blueprints to " + Parser.BLUEPRINTS_FULL_PATH + FULL_FILENAME + ".json");
    Debug.Log ("Saved: \n" + blueprint_json);
  }

  /* Private methods */

  void Start() {
    if (RoomCollection.ROOMS.Count == 0) {
      Construct ();
    }
  }

  void ConstructRooms() {
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
    Debug.Log ("Constructed " + i.ToString() + " rooms from blueprints.");
    RoomCollection.INSTANCE.UpdateRooms ();
  }

  void ConstructDoors() {
    foreach (DoorwayObject door in FindObjectsOfType<DoorwayObject>()) {
      Lib.DestroyChildrenAndSelf (door.transform);
    }

    int i = 0;
    foreach (DoorwayPosition door in blueprints.contents.doors) {
      GameObject door_gameobj = GameObject.Instantiate (BuildingPrefabs.DOORWAY);

      Vector3 rotation_euler = door_gameobj.transform.eulerAngles;
      switch (door.open_dir) {
      case Direction.NORTH:
        rotation_euler.y = 0f;
        break;
      case Direction.WEST:
        rotation_euler.y = 90f;
        break;
      case Direction.SOUTH:
        rotation_euler.y = 180f;
        break;
      case Direction.EAST:
        rotation_euler.y = 270f;
        break;
      }
      door_gameobj.transform.eulerAngles = rotation_euler;

      door_gameobj.transform.position = new Vector3 (door.position.x, 0.9f, door.position.y);

      door_gameobj.transform.SetParent (RoomCollection.INSTANCE.transform);
      door_gameobj.name = "Door_UNSET_" + i.ToString ();
      ++i;
    }
    Debug.Log ("Constructed " + i.ToString() + " doors from blueprints.");
  }
}
