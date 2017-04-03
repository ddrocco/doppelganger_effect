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

  /* DEBUG UTILITY */
}
