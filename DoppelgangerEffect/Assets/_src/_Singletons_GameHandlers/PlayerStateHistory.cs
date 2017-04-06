using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public struct RoomChangeEvent {
  public int step;
  public int room_id;
  public RoomChangeEvent(int s, int r) {
    step = s;
    room_id = r;
  }
}


public class PlayerStateHistory : MonoBehaviour {
  static PlayerStateHistory _instance;
  public static PlayerStateHistory INSTANCE {
    get {
      if (_instance != null) {
        return _instance;
      }
      Debug.Log ("Instance was null.");
      _instance = GameObject.FindObjectOfType<PlayerStateHistory> ();
      if (_instance != null) {
        return _instance;
      }
      GameObject foe_controller_gameobj = GameObject.Instantiate (BuildingPrefabs.EMPTY_OBJECT);
      foe_controller_gameobj.name = "FoeController";
      foe_controller_gameobj.transform.SetParent (MasterInit.INSTANCE.transform);
      _instance = foe_controller_gameobj.AddComponent<PlayerStateHistory> ();
      return _instance;
    }
  }

  static int _current_step;
  public static int CURRENT_STEP {
    get {
      return _current_step;
    }
  }
  static int _number_of_foes;
  public static int NUMBER_OF_FOES {
    get {
      return _number_of_foes;
    }
  }

  public static List<LocationState> state_history = new List<LocationState>();
  public static List<RoomChangeEvent> room_change_events = new List<RoomChangeEvent> ();

  static float _step_time_ratio;
  public static float STEP_TIME_RATIO {
    get {
      return _step_time_ratio;
    }
  }


  void Awake() {
    _instance = this;
    _current_step = 0;
    _number_of_foes = 0;
    room_change_events.Add (new RoomChangeEvent (1, 0));
  }

  void Update() {
    float time = Time.time;
    _step_time_ratio = (time % Constants.TIME_BETWEEN_GHOST_RECORDS) / Constants.TIME_BETWEEN_GHOST_RECORDS;
    if (time > _current_step * Constants.TIME_BETWEEN_GHOST_RECORDS) {
      _current_step++;
      Player.INSTANCE.UpdateLocationState ();
      state_history.Add (Player.CURRENT_LOCATION_STATE);
      room_change_events[room_change_events.Count - 1] = new RoomChangeEvent(CURRENT_STEP, Player.CURRENT_OCCUPIED_ROOM);
      foreach (FoeMetadataHandler foe in FindObjectsOfType<FoeMetadataHandler>()) {
        foe.NewStep ();
      }
    }
    if (time > (_number_of_foes + 1) * Constants.TIME_BETWEEN_GHOST_SPAWNS) {
      _number_of_foes++;
      var new_foe = (GameObject)Instantiate (BuildingPrefabs.GHOST_PREFAB, transform);
      Debug.Log ("New foe at " + time + " " + _number_of_foes);
    }
  }

  public static void PlayerRoomChangeEvent() {
    RoomChangeEvent new_change_event = new RoomChangeEvent ();
    new_change_event.room_id = Player.CURRENT_LOCATION_STATE.room_id;
    new_change_event.step = CURRENT_STEP + 1;
    room_change_events.Add (new_change_event);

    if (DebugConstants.ROOM_COLORATION_RULES == DebugConstants.RoomColorationPolicy.DEBUG_BY_DISTANCE_TO_PLAYER) {
      RoomCollection.INSTANCE.ColorRooms ();
    }
  }
}
