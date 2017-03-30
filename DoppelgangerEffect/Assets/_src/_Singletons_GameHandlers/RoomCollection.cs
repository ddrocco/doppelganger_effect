using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
  List<int[]> room_distance_chart;
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
    CalculateRoomDistances ();
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
    switch (DebugConstants.ROOM_COLORATION_RULES) {
    case DebugConstants.RoomColorationPolicy.DEBUG_UNIQUE_BY_ID:
      foreach (RoomObject room in rooms) {
        room.ColorifyByID ();
      }
      break;
    case DebugConstants.RoomColorationPolicy.DEBUG_BY_DISTANCE_TO_PLAYER:
      int curr_id = 0;
      int player_id = PlayerStateHistory.current_occupied_room;
      if (player_id == -1) {
        foreach (RoomObject room in rooms) {
          room.Colorify (Color.black);
          ++curr_id;
        }
      } else {
        foreach (RoomObject room in rooms) {
          int distance = room_distance_chart [curr_id] [player_id];
          if (distance == 0) {
            room.Colorify (Color.white);
          } else if (distance < Constants.ROOM_DISTANCE_TO_SLEEP) {
            float ratio = (float)distance / (float)Constants.ROOM_DISTANCE_TO_SLEEP;
            room.Colorify (new Color (1f - ratio, 1f - ratio, 1f));
          } else {
            room.Colorify (Color.black);
          }
          ++curr_id;
        }
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

  private struct GraphQueueEntry {
    public int id;
    public int distance;
    public GraphQueueEntry(int id, int distance) {
      this.id = id;
      this.distance = distance;
    }
  }

  void CalculateRoomDistances() {
    room_distance_chart = new List<int[]> ();
    for (int i = 0; i < rooms.Count; ++i) {
      room_distance_chart.Add(Enumerable.Repeat<int>(-1, rooms.Count).ToArray());
    }
    // Calculate distances for each node.
    for (int i = 0; i < rooms.Count; ++i) {
      HashSet<int> visited = new HashSet<int>();
      Queue<GraphQueueEntry> next_visits = new Queue<GraphQueueEntry>();
      visited.Add (i);
      next_visits.Enqueue (new GraphQueueEntry (i, 0));
      // BFS for each node.
      while (next_visits.Count > 0) {
        GraphQueueEntry current_node = next_visits.Dequeue();
        room_distance_chart [i] [current_node.id] = current_node.distance;
        foreach (int j in rooms[current_node.id].adjacent_rooms) {
          if (visited.Contains (j)) {
            continue;
          }
          visited.Add (j);
          next_visits.Enqueue(new GraphQueueEntry(j, current_node.distance + 1));
        }
      }
      if (DebugConstants.ENABLE_PRINT_ROOM_DISTANCE_LIST) {
        string debug_message = "ID " + i.ToString () + " Room Distances: [";
        for (int j = 0; j < rooms.Count; ++j) {
          debug_message += room_distance_chart [i] [j].ToString () + ", ";
        }
        debug_message += "]";
        debug_message += "\nVISITED_COUNT " + visited.Count + ", ADJACENCIES: " + rooms [i].adjacent_rooms.Count;
        Debug.Log("[DebugConstants.ENABLE_PRINT_ROOM_DISTANCE_LIST]\n" + debug_message);
      }
    }
  }
}
