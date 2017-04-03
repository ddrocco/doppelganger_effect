using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {
  static Constants _instance;
  public static Constants INSTANCE {
    get {
      if (_instance != null) {
        return _instance;
      }
      _instance = GameObject.FindObjectOfType<Constants> ();
      if (_instance != null) {
        return _instance;
      }
      MasterInit master_init_instance = GameObject.FindObjectOfType<MasterInit>();
      _instance = master_init_instance.gameObject.AddComponent<Constants> ();
      return _instance;
    }
  }

  public float _PLAYER_WALKSPEED = 1f;
  public static float PLAYER_WALKSPEED {
    get {
      return INSTANCE._PLAYER_WALKSPEED;
    }
    set {
      INSTANCE._PLAYER_WALKSPEED = value;
    }
  }

  public float _PLAYER_RUNSPEED = 2f;
  public static float PLAYER_RUNSPEED {
    get {
      return INSTANCE._PLAYER_RUNSPEED;
    }
    set {
      INSTANCE._PLAYER_RUNSPEED = value;
    }
  }

  public float _PLAYER_ACCELERATION_RATE = 1f;
  public static float PLAYER_ACCELERATION_RATE {
    get {
      return INSTANCE._PLAYER_ACCELERATION_RATE;
    }
    set {
      INSTANCE._PLAYER_ACCELERATION_RATE = value;
    }
  }

  public float _PLAYER_LR_ROTATION_RATE = 30f;
  public static float PLAYER_LR_ROTATION_RATE {
    get {
      return INSTANCE._PLAYER_LR_ROTATION_RATE;
    }
    set {
      INSTANCE._PLAYER_LR_ROTATION_RATE = value;
    }
  }

  public float _PLAYER_UD_ROTATION_RATE = 30f;
  public static float PLAYER_UD_ROTATION_RATE {
    get {
      return INSTANCE._PLAYER_UD_ROTATION_RATE;
    }
    set {
      INSTANCE._PLAYER_UD_ROTATION_RATE = value;
    }
  }

  public float _TIME_BETWEEN_GHOST_RECORDS = 0.1f;
  public static float TIME_BETWEEN_GHOST_RECORDS {
    get {
      return INSTANCE._TIME_BETWEEN_GHOST_RECORDS;
    }
    set {
      INSTANCE._TIME_BETWEEN_GHOST_RECORDS = value;
    }
  }


  public float _TIME_BETWEEN_GHOST_SPAWNS = 30f;
  public static float TIME_BETWEEN_GHOST_SPAWNS {
    get {
      return INSTANCE._TIME_BETWEEN_GHOST_SPAWNS;
    }
    set {
      INSTANCE._TIME_BETWEEN_GHOST_SPAWNS = value;
    }
  }


  public int _ROOM_DISTANCE_TO_SLEEP = 3;
  public static int ROOM_DISTANCE_TO_SLEEP {
    get {
      return INSTANCE._ROOM_DISTANCE_TO_SLEEP;
    }
    set {
      INSTANCE._ROOM_DISTANCE_TO_SLEEP = value;
    }
  }


  public float _ROOM_DETECTION_RAYCAST_DISTANCE = 5f;
  public static float ROOM_DETECTION_RAYCAST_DISTANCE {
    get {
      return INSTANCE._ROOM_DETECTION_RAYCAST_DISTANCE;
    }
    set {
      INSTANCE._ROOM_DETECTION_RAYCAST_DISTANCE = value;
    }
  }


  public int _ROOM_DETECTION_CULLING_MASK = 1 << 8;
  public static int ROOM_DETECTION_CULLING_MASK {
    get {
      return INSTANCE._ROOM_DETECTION_CULLING_MASK;
    }
    set {
      INSTANCE._ROOM_DETECTION_CULLING_MASK = value;
    }
  }


  public int _INTERACTABLE_CULLING_MASK = 1 << 9;
  public static int INTERACTABLE_CULLING_MASK {
    get {
      return INSTANCE._INTERACTABLE_CULLING_MASK;
    }
    set {
      INSTANCE._INTERACTABLE_CULLING_MASK = value;
    }
  }


  public float _PLAYER_INTERACTION_DISTANCE = 3f;
  public static float PLAYER_INTERACTION_DISTANCE {
    get {
      return INSTANCE._PLAYER_INTERACTION_DISTANCE;
    }
    set {
      INSTANCE._PLAYER_INTERACTION_DISTANCE = value;
    }
  }
}
