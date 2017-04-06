using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomObject : MonoBehaviour {
  public int id {
    get {
      return _id;
    }
    set {
      _id = value;
      gameObject.name = "RoomObject" + _id.ToString ();
      if (DebugConstants.ROOM_COLORATION_RULES == DebugConstants.RoomColorationPolicy.DEBUG_UNIQUE_BY_ID) {
        ColorifyByID ();
      }
    }
  }
  public int _id;

  Color _color;
  public Color COLOR {
    get {
      return _color;
    }
    set { 
      _color = value;
      UpdateColorInRenderers();
    }
  }

  

  public HashSet<int> adjacent_rooms = new HashSet<int>();

  public void ColorifyByID() {
    Colorify(Lib.Colorify (id));
  }

  public void Colorify(Color color) {
    COLOR = color;
  }

  /* PRIVATE METHODS */

  void UpdateColorInRenderers() {
    if (Application.isPlaying) {
      foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) {
        renderer.material.color = COLOR;
      }
    }
  }

  void Start() {
    UpdateColorInRenderers();
  }

  public RoomActivityState GetActivityState() {
    return GetActivityState(id);
  }

  public static RoomActivityState GetActivityState (int room_id) {
    RoomObject room = RoomCollection.ROOMS [room_id];
    int distance = RoomCollection.ROOM_DISTANCE_CHART[Player.CURRENT_OCCUPIED_ROOM][room_id];
    if (distance < Constants.ROOM_DISTANCE_TO_SLEEP) {
      return RoomActivityState.ACTIVE;
    } else {
      return RoomActivityState.SLEEPING;
    }
  }

  /* DEBUG UTILITY */
}
