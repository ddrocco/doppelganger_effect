using UnityEngine;
using System.Collections;



public class FoeMetadataHandler : MonoBehaviour {
  public RoomActivityState state;

  int _birth_step;
  public int BIRTH_STEP {
    get {
      return _birth_step;
    }
  }

  LocationState _last_loc_state;
  public LocationState LAST_LOC_STATE {
    get {
      return _last_loc_state;
    }
  }

  LocationState _next_loc_state;
  public LocationState NEXT_LOC_STATE {
    get {
      return _next_loc_state;
    }
  }

  int next_change_event = 1;
  int change_event_index = -1;
  int current_room = 0;

  void NewLocation(int newest_location_idx) {
    _last_loc_state = NEXT_LOC_STATE;
    _next_loc_state = PlayerStateHistory.state_history[newest_location_idx];
  }

  void Awake() {
    _birth_step = PlayerStateHistory.CURRENT_STEP;
    _last_loc_state.pos = transform.position;
    _last_loc_state.facing = transform.rotation;
    _next_loc_state = PlayerStateHistory.state_history[0];
  }

  public void NewStep () {
    int step = PlayerStateHistory.CURRENT_STEP - BIRTH_STEP;
    NewLocation (PlayerStateHistory.CURRENT_STEP - BIRTH_STEP);
    if (step == next_change_event) {
      RoomChangeEvent room_change_event = PlayerStateHistory.room_change_events [change_event_index];
      next_change_event = room_change_event.step;
      current_room = room_change_event.room_id;
      state = RoomObject.GetActivityState (room_change_event.room_id);
    }
  }

  void Update () {
    transform.position =
      _next_loc_state.pos * PlayerStateHistory.STEP_TIME_RATIO
      + _last_loc_state.pos * (1 - PlayerStateHistory.STEP_TIME_RATIO);
    transform.rotation = Quaternion.Lerp(_next_loc_state.facing, _last_loc_state.facing, PlayerStateHistory.STEP_TIME_RATIO);
  }
}
