using UnityEngine;
using System.Collections;

public class BuildingPrefabs : MonoBehaviour {
  static BuildingPrefabs _instance;
  public static BuildingPrefabs INSTANCE {
    get {
      if (_instance != null) {
        return _instance;
      }
      _instance = GameObject.FindObjectOfType<BuildingPrefabs> ();
      if (_instance != null) {
        return _instance;
      }
      MasterInit master_init_instance = GameObject.FindObjectOfType<MasterInit>();
      _instance = master_init_instance.gameObject.AddComponent<BuildingPrefabs> ();
      return _instance;
    }
  }

  public GameObject _FloorTile;
  public static GameObject FLOOR_TILE {
    get {
      return INSTANCE._FloorTile;
    }
    set {
      INSTANCE._FloorTile = value;
    }
  }

  public GameObject _Doorway;
  public static GameObject DOORWAY {
    get {
      return INSTANCE._Doorway;
    }
    set {
      INSTANCE._Doorway = value;
    }
  }

  public GameObject _EmptyObject;
  public static GameObject EMPTY_OBJECT {
    get {
      return INSTANCE._EmptyObject;
    }
    set {
      INSTANCE._EmptyObject = value;
    }
  }

  public GameObject _GhostPrefab;
  public static GameObject GHOST_PREFAB {
    get {
      return INSTANCE._GhostPrefab;
    }
    set {
      INSTANCE._GhostPrefab = value;
    }
  }
}
