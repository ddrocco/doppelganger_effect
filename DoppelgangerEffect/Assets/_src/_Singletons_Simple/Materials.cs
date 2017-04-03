using UnityEngine;
using System.Collections;

public class Materials : MonoBehaviour {
  static Materials _instance;
  public static Materials INSTANCE {
    get {
      if (_instance != null) {
        return _instance;
      }
      _instance = GameObject.FindObjectOfType<Materials> ();
      if (_instance != null) {
        return _instance;
      }
      MasterInit master_init_instance = GameObject.FindObjectOfType<MasterInit>();
      _instance = master_init_instance.gameObject.AddComponent<Materials> ();
      return _instance;
    }
  }

  public Material _FLOOR_MATERIAL;
  public static Material FLOOR_MATERIAL {
    get {
      return INSTANCE._FLOOR_MATERIAL;
    }
    set {
      INSTANCE._FLOOR_MATERIAL = value;
    }
  }
}
