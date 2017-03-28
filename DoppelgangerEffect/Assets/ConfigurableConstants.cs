using UnityEngine;
using System.Collections;

public class ConfigurableConstants : MonoBehaviour {
  static ConfigurableConstants _main;

  public float _PLAYER_LOOK_SENSITIVITY = 1f;
  public static float PLAYER_LOOK_SENSITIVITY {
    get {
      return _main._PLAYER_LOOK_SENSITIVITY;
    }
    set {
      _main._PLAYER_LOOK_SENSITIVITY = value;
    }
  }

  public float _PLAYER_LOOK_ANGLE_MAX = 60f;
  public static float PLAYER_LOOK_ANGLE_MAX {
    get {
      return _main._PLAYER_LOOK_ANGLE_MAX;
    }
    set {
      _main._PLAYER_LOOK_ANGLE_MAX = value;
    }
  }
  public static float PLAYER_LOOK_ANGLE_MIN {
    get {
      return -_main._PLAYER_LOOK_ANGLE_MAX;
    }
    set {
      _main._PLAYER_LOOK_ANGLE_MAX = -value;
    }
  }

  protected static bool _MOUSE_LOCK_ENABLED = true;
  public static bool MOUSE_LOCK_ENABLED {
    get {
      return _MOUSE_LOCK_ENABLED;
    }
    set {
      _MOUSE_LOCK_ENABLED = value;
      if (_MOUSE_LOCK_ENABLED) {
        Cursor.lockState = CursorLockMode.Locked;
      } else {
        Cursor.lockState = CursorLockMode.None;
      }
    }
  }
  public void ToggleLockMouse() {
    MOUSE_LOCK_ENABLED = !MOUSE_LOCK_ENABLED;
  }

  protected static bool _MOUSE_HIDE_ENABLED = true;
  public static bool MOUSE_HIDE_ENABLED {
    get {
      return _MOUSE_HIDE_ENABLED;
    }
    set {
      _MOUSE_HIDE_ENABLED = value;
    }
  }
  public void ToggleShowMouse() {
    MOUSE_HIDE_ENABLED = !MOUSE_HIDE_ENABLED;
  }

  void Awake() {
    _main = this;
  }
}
