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
  int _id;
  public HashSet<int> adjacent_rooms;

  public void ColorifyByID() {
    Colorify(Lib.Colorify (id));
  }

  public void Colorify(Color color) {
    foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) {
      renderer.material.color = color;
    }
  }

  /* DEBUG UTILITY */
}
