using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct RoomEdge {
  public Direction direction;
  public int length;
}

public class RoomCollection : MonoBehaviour {
  public static RoomCollection _main;

  /*
   * NOTE: All ints used below are IDs of rooms.
   * The ID corresponds to the place in the "rooms" list.
   * "Rooms" is immutable once the game begins.
   */

  public List<RoomObject> rooms;
  HashSet<int> rooms_to_update;

  public void UpdateRooms() {
    // Destructively resets Rooms.  Existing ID mappings will break.
    if (!Application.isEditor) {
      Debug.Log ("UpdateRooms failed; can only be called from Edit mode.");
    }
    rooms = new List<RoomObject> ();
    int curr_id = 0;
    RoomObject[] all_rooms = (RoomObject[])GameObject.FindObjectsOfType<RoomObject> ();
    foreach (RoomObject room in all_rooms) {
      room.id = curr_id++;
      rooms.Add (room);
      room.transform.parent = transform;
    }
    ColorRooms ();
  }
    
  public void SafeUpdateRooms() {
    // Safely updates Rooms.  Preserves existing ID Mappings.
    int count = 0;
    foreach (RoomObject room in rooms) {
      room.id = count++;
      Debug.Log("Assigned " + room.id.ToString() + ".");
      ++count;
    }
    ColorRooms ();
  }

  public void ColorRooms() {
    Debug.LogError ("Called.");
    switch (DebugConstants.ROOM_COLORATION_RULES) {
    case DebugConstants.RoomColorationPolicy.DEBUG_UNIQUE_BY_ID:
      foreach (RoomObject room in rooms) {
        room.ColorifyByID ();
      }
      break;
    case DebugConstants.RoomColorationPolicy.DEBUG_BY_DISTANCE_TO_PLAYER:
      int curr_id = 0;
      foreach (RoomObject room in rooms) {
        if (curr_id == PlayerStateHistory.current_occupied_room) {
          room.Colorify (Color.white);
        } else {
          room.Colorify (Color.black);
        }
        ++curr_id;
        // TODO: Improve to make it by distance.
      }
      break;
    case DebugConstants.RoomColorationPolicy.REGULAR:
      foreach (RoomObject room in rooms) {
        room.Colorify (Color.gray);
      }
      break;
    }
    return;
  }

  void Awake() {
    _main = this;
  }


  void Start() {
    UpdateRooms ();
  }
}
