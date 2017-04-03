using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct FloorTile {
  public Vector2 position;
  public Vector2 dimensions;
}

public struct CubeRoom {
  public List<FloorTile> tiles;
}

[System.Serializable]
public class CubeRoomBlueprints : MonoBehaviour {
  public List<CubeRoom> rooms;

  public CubeRoomBlueprints() {
    rooms = new List<CubeRoom>();
  }

  /* User-facing Editor Components */
  public int room_index;
  public int floor_tile_index;
  public Vector2 new_floor_tile_position;
  public Vector2 new_floor_tile_dimensions;

  public void CreateRoom() {
    CubeRoom new_room = new CubeRoom ();
    new_room.tiles = new List<FloorTile> ();
    rooms.Add (new_room);
  }

  public void RemoveRoom(int index) {
    rooms.RemoveAt(index);
  }

  public void AddFloorTileToRoom(int room_index, Vector2 position, Vector2 dimensions) {
    FloorTile new_tile = new FloorTile ();
    new_tile.position = position;
    new_tile.dimensions = dimensions;
    rooms [room_index].tiles.Add (new_tile);
  }

  public void RemoveFloorTileFromRoom(int room_index, int floor_tile_index) {
    rooms [room_index].tiles.RemoveAt (floor_tile_index);
  }
}
