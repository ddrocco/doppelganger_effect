using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RoomCollection : MonoBehaviour {
  static RoomCollection _instance;
  public static RoomCollection INSTANCE {
    get {
      if (_instance != null) {
        return _instance;
      }
      Debug.Log ("Instance was null.");
      _instance = GameObject.FindObjectOfType<RoomCollection> ();
      if (_instance != null) {
        return _instance;
      }
      GameObject room_collection_gameobj = GameObject.Instantiate (BuildingPrefabs.EMPTY_OBJECT);
      room_collection_gameobj.name = "RoomCollection";
      _instance = room_collection_gameobj.AddComponent<RoomCollection> ();
      return _instance;
    }
  }
  public static bool InstanceIsSet() {
    return (_instance != null);
  }

  /*
   * NOTE: All ints used below are IDs of rooms.
   * The ID corresponds to the place in the "rooms" list.
   * "Rooms" is immutable once the game begins.
   */
  static List<RoomObject> _rooms;
  public static List<RoomObject> ROOMS {
    get {
      if (_rooms == null) {
        _rooms = new List<RoomObject> ();
      }
      return _rooms;
    }
  }
  public void DestroyAllRooms() {
    foreach (RoomObject room in GameObject.FindObjectsOfType<RoomObject>()) {
      Lib.DestroyChildrenAndSelf (room.transform);
    }
    _rooms = new List<RoomObject> ();
  }
  public void ClearRooms() {
    _rooms = new List<RoomObject> ();
  }

  List<int[]> _room_distance_chart;
  public static int[][] ROOM_DISTANCE_CHART {
    get {
      return INSTANCE._room_distance_chart.ToArray ();
    }
  }
  HashSet<int> rooms_to_update;

  public void ColorRooms() {
    switch (DebugConstants.ROOM_COLORATION_RULES) {
    case DebugConstants.RoomColorationPolicy.DEBUG_UNIQUE_BY_ID:
      foreach (RoomObject room in ROOMS) {
        room.ColorifyByID ();
      }
      break;
    case DebugConstants.RoomColorationPolicy.DEBUG_BY_DISTANCE_TO_PLAYER:
      int player_id = Player.CURRENT_OCCUPIED_ROOM;
      if (player_id == -1) {
        foreach (RoomObject room in ROOMS) {
          room.Colorify (Color.black);
        }
      } else {
        foreach (RoomObject room in ROOMS) {
          int distance = _room_distance_chart [room.id] [player_id];
          if (distance == 0) {
            room.Colorify (Color.white);
          } else if (distance < Constants.ROOM_DISTANCE_TO_SLEEP) {
            float ratio = (float)distance / (float)Constants.ROOM_DISTANCE_TO_SLEEP;
            room.Colorify (new Color (1f - ratio, 1f - ratio, 1f));
          } else {
            room.Colorify (Color.black);
          }
        }
      }
      break;
    case DebugConstants.RoomColorationPolicy.REGULAR:
      foreach (RoomObject room in ROOMS) {
        room.Colorify (Color.gray);
      }
      break;
    }
    return;
  }

  void Start() {
    UpdateRooms ();
  }

  /* Room reloader methods. */

  public void UpdateRooms() {
    DetectRoomAdjacencies ();
    CalculateRoomDistances ();
    ColorRooms ();
  }

  public void RecalculateRoomIDsAndUpdate() {
    ClearRooms ();
    int curr_id = 0;
    RoomObject[] all_rooms = (RoomObject[])GameObject.FindObjectsOfType<RoomObject> ();
    foreach (RoomObject room in all_rooms) {
      room.id = curr_id++;
      room.gameObject.name = "Room" + room.id.ToString ();
      _rooms.Add (room);
      room.transform.SetParent(transform);
    }
    DetectRoomAdjacencies ();
    CalculateRoomDistances ();
    ColorRooms ();
  }

  public void DetectRoomAdjacencies() {
    foreach (RoomObject room in RoomCollection.ROOMS) {
      room.adjacent_rooms.Clear ();
    }
    foreach (PlaceholderAdjacencyConstructor pac in GameObject.FindObjectsOfType<PlaceholderAdjacencyConstructor>()) {
      pac.PlaceholderAdjacencyDetection ();
    }
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
    _room_distance_chart = new List<int[]> ();
    for (int i = 0; i < ROOMS.Count; ++i) {
      _room_distance_chart.Add(Enumerable.Repeat<int>(-1, ROOMS.Count).ToArray());
    }
    // Calculate distances for each node.
    for (int i = 0; i < ROOMS.Count; ++i) {
      HashSet<int> visited = new HashSet<int>();
      Queue<GraphQueueEntry> next_visits = new Queue<GraphQueueEntry>();
      visited.Add (i);
      next_visits.Enqueue (new GraphQueueEntry (i, 0));
      // BFS for each node.
      while (next_visits.Count > 0) {
        GraphQueueEntry current_node = next_visits.Dequeue();
        _room_distance_chart [i] [current_node.id] = current_node.distance;
        foreach (int j in ROOMS[current_node.id].adjacent_rooms) {
          if (visited.Contains (j)) {
            continue;
          }
          visited.Add (j);
          next_visits.Enqueue(new GraphQueueEntry(j, current_node.distance + 1));
        }
      }
      if (DebugConstants.ENABLE_PRINT_ROOM_DISTANCE_LIST) {
        string debug_message = "ID " + i.ToString () + " Room Distances: [";
        for (int j = 0; j < ROOMS.Count; ++j) {
          debug_message += _room_distance_chart [i] [j].ToString () + ", ";
        }
        debug_message += "]";
        debug_message += "\nVISITED_COUNT " + visited.Count + ", ADJACENCIES: " + ROOMS [i].adjacent_rooms.Count;
        Debug.Log("[DebugConstants.ENABLE_PRINT_ROOM_DISTANCE_LIST]\n" + debug_message);
      }
    }
  }

  void Awake() {
    if (ROOMS != null && ROOMS.Count > 0 && !InstanceIsSet()) {
      _instance = this;
    }
  }
}
