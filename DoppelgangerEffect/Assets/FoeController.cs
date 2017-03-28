using UnityEngine;
using System.Collections;

public class FoeController : MonoBehaviour {
  float birth_time;
  int current_step;

  LocationState last_loc_state;
  LocationState next_loc_state;

  void NewLocation(int newest_location_idx) {
    last_loc_state = next_loc_state;
    next_loc_state = PlayerStateHistory.main.state_history[newest_location_idx];
  }

  void Awake() {
    birth_time = Time.time;
    current_step = -1;
    last_loc_state.pos = transform.position;
    last_loc_state.facing = transform.rotation;
    next_loc_state = PlayerStateHistory.main.state_history[0];
  }

	// Update is called once per frame
	void Update () {
    float r_time = Time.time - birth_time / Constants.TIME_BETWEEN_GHOST_RECORDS;
    if (r_time > current_step) {
      current_step++;
      NewLocation (current_step);
    }
    transform.position = next_loc_state.pos * r_time + last_loc_state.pos * (1 - r_time);
    transform.rotation = Quaternion.Lerp(next_loc_state.facing, last_loc_state.facing, r_time);
	}
}
