﻿using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {
  public static Constants _main;

  public float _PLAYER_WALKSPEED = 1f;
  public static float PLAYER_WALKSPEED {
    get {
      return _main._PLAYER_WALKSPEED;
    }
    set {
      _main._PLAYER_WALKSPEED = value;
    }
  }

  public float _PLAYER_RUNSPEED = 2f;
  public static float PLAYER_RUNSPEED {
    get {
      return _main._PLAYER_RUNSPEED;
    }
    set {
      _main._PLAYER_RUNSPEED = value;
    }
  }

  public float _PLAYER_ACCELERATION_RATE = 1f;
  public static float PLAYER_ACCELERATION_RATE {
    get {
      return _main._PLAYER_ACCELERATION_RATE;
    }
    set {
      _main._PLAYER_ACCELERATION_RATE = value;
    }
  }

  public float _PLAYER_LR_ROTATION_RATE = 30f;
  public static float PLAYER_LR_ROTATION_RATE {
    get {
      return _main._PLAYER_LR_ROTATION_RATE;
    }
    set {
      _main._PLAYER_LR_ROTATION_RATE = value;
    }
  }

  public float _PLAYER_UD_ROTATION_RATE = 30f;
  public static float PLAYER_UD_ROTATION_RATE {
    get {
      return _main._PLAYER_UD_ROTATION_RATE;
    }
    set {
      _main._PLAYER_UD_ROTATION_RATE = value;
    }
  }

  public float _TIME_BETWEEN_GHOST_RECORDS = 0.1f;
  public static float TIME_BETWEEN_GHOST_RECORDS {
    get {
      return _main._TIME_BETWEEN_GHOST_RECORDS;
    }
    set {
      _main._TIME_BETWEEN_GHOST_RECORDS = value;
    }
  }


  public float _TIME_BETWEEN_GHOST_SPAWNS = 30f;
  public static float TIME_BETWEEN_GHOST_SPAWNS {
    get {
      return _main._TIME_BETWEEN_GHOST_SPAWNS;
    }
    set {
      _main._TIME_BETWEEN_GHOST_SPAWNS = value;
    }
  }


  public int _ROOM_DISTANCE_TO_SLEEP = 3;
  public static int ROOM_DISTANCE_TO_SLEEP {
    get {
      return _main._ROOM_DISTANCE_TO_SLEEP;
    }
    set {
      _main._ROOM_DISTANCE_TO_SLEEP = value;
    }
  }


  public float _ROOM_DETECTION_RAYCAST_DISTANCE = 5f;
  public static float ROOM_DETECTION_RAYCAST_DISTANCE {
    get {
      return _main._ROOM_DETECTION_RAYCAST_DISTANCE;
    }
    set {
      _main._ROOM_DETECTION_RAYCAST_DISTANCE = value;
    }
  }


  public int _ROOM_DETECTION_CULLING_MASK = 1 << 9;
  public static int ROOM_DETECTION_CULLING_MASK {
    get {
      return _main._ROOM_DETECTION_CULLING_MASK;
    }
    set {
      _main._ROOM_DETECTION_CULLING_MASK = value;
    }
  }

	void Awake() {
    _main = this;
	}
}
