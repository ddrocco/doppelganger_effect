using UnityEngine;
using System.Collections;

public class ConfigurableConstants : MonoBehaviour {
  static ConfigurableConstants _instance;
  public static ConfigurableConstants INSTANCE {
    get {
      if (_instance != null) {
        return _instance;
      }
      _instance = GameObject.FindObjectOfType<ConfigurableConstants> ();
      if (_instance != null) {
        return _instance;
      }
      MasterInit master_init_instance = GameObject.FindObjectOfType<MasterInit>();
      _instance = master_init_instance.gameObject.AddComponent<ConfigurableConstants> ();
      return _instance;
    }
  }

  public float _PLAYER_LOOK_SENSITIVITY = 1f;
  public static float PLAYER_LOOK_SENSITIVITY {
    get {
      return INSTANCE._PLAYER_LOOK_SENSITIVITY;
    }
    set {
      INSTANCE._PLAYER_LOOK_SENSITIVITY = value;
    }
  }

  public float _PLAYER_LOOK_ANGLE_MAX = 60f;
  public static float PLAYER_LOOK_ANGLE_MAX {
    get {
      return INSTANCE._PLAYER_LOOK_ANGLE_MAX;
    }
    set {
      INSTANCE._PLAYER_LOOK_ANGLE_MAX = value;
    }
  }
  public static float PLAYER_LOOK_ANGLE_MIN {
    get {
      return -INSTANCE._PLAYER_LOOK_ANGLE_MAX;
    }
    set {
      INSTANCE._PLAYER_LOOK_ANGLE_MAX = -value;
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
}
