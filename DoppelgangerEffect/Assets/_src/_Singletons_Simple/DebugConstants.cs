using UnityEngine;
using System.Collections;

public class DebugConstants : MonoBehaviour {
  public static DebugConstants _main;

  public bool _ALLOW_PRINT_PLAYER_INPUT = true;
  public static bool ALLOW_PRINT_PLAYER_INPUT {
    get {
      return _main._ALLOW_PRINT_PLAYER_INPUT;
    }
    set {
      _main._ALLOW_PRINT_PLAYER_INPUT = value;
    }
  }

  public bool _ALLOW_PRINT_WORLDSPACE_STATE = true;
  public static bool ALLOW_PRINT_WORLDSPACE_STATE {
    get {
      return _main._ALLOW_PRINT_WORLDSPACE_STATE;
    }
    set {
      _main._ALLOW_PRINT_WORLDSPACE_STATE = value;
    }
  }


  public bool _ALLOW_PRINT_LOCATION_STATE = true;
  public static bool ALLOW_PRINT_LOCATION_STATE {
    get {
      return _main._ALLOW_PRINT_LOCATION_STATE;
    }
    set {
      _main._ALLOW_PRINT_LOCATION_STATE = value;
    }
  }

  public bool _ALLOW_DEBUG_RAYS = true;
  public static bool ALLOW_DEBUG_RAYS {
    get {
      return _main._ALLOW_DEBUG_RAYS;
    }
    set {
      _main._ALLOW_DEBUG_RAYS = value;
    }
  }

  public enum RoomColorationPolicy {
    REGULAR,
    DEBUG_UNIQUE_BY_ID,
    DEBUG_BY_DISTANCE_TO_PLAYER
  };

  public RoomColorationPolicy _ROOM_COLORATION_RULES = RoomColorationPolicy.REGULAR;
  public static RoomColorationPolicy ROOM_COLORATION_RULES {
    get {
      return _main._ROOM_COLORATION_RULES;
    }
    set {
      _main._ROOM_COLORATION_RULES = value;
      RoomCollection._main.ColorRooms ();
    }
  }

  void Awake() {
    _main = this;
  }
}
