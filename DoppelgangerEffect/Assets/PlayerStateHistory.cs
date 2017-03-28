using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStateHistory : MonoBehaviour {

  public GameObject ghost_prefab;

  public static PlayerStateHistory main;

  public List<LocationState> state_history = new List<LocationState>();

  int number_of_steps;
  int number_of_foes;

  void Awake() {
    main = this;
    number_of_steps = 0;
    number_of_foes = 0;
  }

  void Update() {
    float time = Time.time;
    if (time > number_of_steps * Constants.TIME_BETWEEN_GHOST_RECORDS) {
      number_of_steps++;
      state_history.Add (Player.main.GetLocationState ());
    }
    if (time > (number_of_foes + 1) * Constants.TIME_BETWEEN_GHOST_SPAWNS) {
      number_of_foes++;
      state_history.Add (Player.main.GetLocationState ());
      var new_foe = (GameObject)Instantiate (ghost_prefab, transform);
      Debug.Log ("New foe at " + time + " " + number_of_foes);
    }
  }
}
