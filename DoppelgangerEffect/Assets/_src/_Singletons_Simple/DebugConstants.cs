using UnityEngine;
using System.Collections;

public class DebugConstants : MonoBehaviour {
  static DebugConstants _instance;
  public static DebugConstants INSTANCE {
    get {
      if (_instance != null) {
        return _instance;
      }
      _instance = GameObject.FindObjectOfType<DebugConstants> ();
      if (_instance != null) {
        return _instance;
      }
      MasterInit master_init_instance = GameObject.FindObjectOfType<MasterInit>();
      _instance = master_init_instance.gameObject.AddComponent<DebugConstants> ();
      return _instance;
    }
  }

  public bool _ALLOW_PRINT_PLAYER_INPUT = true;
  public static bool ALLOW_PRINT_PLAYER_INPUT {
    get {
      return INSTANCE._ALLOW_PRINT_PLAYER_INPUT;
    }
    set {
      INSTANCE._ALLOW_PRINT_PLAYER_INPUT = value;
    }
  }

  public bool _ALLOW_PRINT_WORLDSPACE_STATE = true;
  public static bool ALLOW_PRINT_WORLDSPACE_STATE {
    get {
      return INSTANCE._ALLOW_PRINT_WORLDSPACE_STATE;
    }
    set {
      INSTANCE._ALLOW_PRINT_WORLDSPACE_STATE = value;
    }
  }


  public bool _ALLOW_PRINT_LOCATION_STATE = true;
  public static bool ALLOW_PRINT_LOCATION_STATE {
    get {
      return INSTANCE._ALLOW_PRINT_LOCATION_STATE;
    }
    set {
      INSTANCE._ALLOW_PRINT_LOCATION_STATE = value;
    }
  }


  public bool _ENABLE_PRINT_ROOM_DISTANCE_LIST = true;
  public static bool ENABLE_PRINT_ROOM_DISTANCE_LIST {
    get {
      return INSTANCE._ENABLE_PRINT_ROOM_DISTANCE_LIST;
    }
    set {
      INSTANCE._ENABLE_PRINT_ROOM_DISTANCE_LIST = value;
    }
  }


  public bool _ENABLE_PLAYER_PHYSICS_MESSAGING = true;
  public static bool ENABLE_PLAYER_PHYSICS_MESSAGING {
    get {
      return INSTANCE._ENABLE_PLAYER_PHYSICS_MESSAGING;
    }
    set {
      INSTANCE._ENABLE_PLAYER_PHYSICS_MESSAGING = value;
    }
  }

  public bool _ALLOW_DEBUG_RAYS = true;
  public static bool ALLOW_DEBUG_RAYS {
    get {
      return INSTANCE._ALLOW_DEBUG_RAYS;
    }
    set {
      INSTANCE._ALLOW_DEBUG_RAYS = value;
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
      return INSTANCE._ROOM_COLORATION_RULES;
    }
    set {
      INSTANCE._ROOM_COLORATION_RULES = value;
      RoomCollection.INSTANCE.ColorRooms ();
    }
  }
}
