using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct FloorTile {
  public Vector2 position;
  public Vector2 dimensions;
}

[System.Serializable]
public struct CubeRoom {
  public List<FloorTile> tiles;
}

[System.Serializable]
public struct DoorwayPosition {
  public Vector2 position;
  public Direction open_dir;
}

[System.Serializable]
public struct CubeLayout {
  public List<CubeRoom> rooms;
  public List<DoorwayPosition> doors;
}

public class CubeRoomBlueprints : MonoBehaviour {
  public CubeLayout contents;

  public CubeRoomBlueprints() {
    contents = new CubeLayout();
  }

  /* User-facing Editor Components */
  public int room_index;
  public int floor_tile_index;
  public Vector2 new_floor_tile_position;
  public Vector2 new_floor_tile_dimensions;
  public Direction new_door_facing;

  public void CreateRoom() {
    CubeRoom new_room = new CubeRoom ();
    new_room.tiles = new List<FloorTile> ();
    contents.rooms.Add (new_room);
  }

  public void RemoveRoom(int index) {
    contents.rooms.RemoveAt(index);
  }

  public void AddFloorTileToRoom(int room_index, Vector2 position, Vector2 dimensions) {
    FloorTile new_tile = new FloorTile ();
    new_tile.position = position;
    new_tile.dimensions = dimensions;
    contents.rooms [room_index].tiles.Add (new_tile);
  }

  public void RemoveFloorTileFromRoom(int room_index, int floor_tile_index) {
    contents.rooms [room_index].tiles.RemoveAt (floor_tile_index);
  }
    
  public void AddDoor(Vector2 position, Direction facing) {
    DoorwayPosition new_door = new DoorwayPosition ();
    new_door.position = position;
    new_door.open_dir = facing;
    contents.doors.Add (new_door);
  }

  public void RemoveDoor(int room_index) {
    contents.doors.RemoveAt(room_index);
  }
}
