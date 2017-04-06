using UnityEngine;
using System.Collections;

public class MasterInit : MonoBehaviour {
  static MasterInit _instance;
  public static MasterInit INSTANCE {
    get {
      return _instance;
    }
  }

  /* List of Singletons:
   * MasterInit
   * ConfigurableConstants
   * Constants
   * DebugConstants
   * DebugLogging
   * PlayerStateHistory
  */

  public static void SanityCheck() {
    if (!FindObjectOfType<InControl.InControlManager> ()) {
      Debug.Log ("You need to create an InControl object!");
    }
    if (!FindObjectOfType<PlayerStateHistory> ()) {
      Debug.Log ("You need to create a PlayerStateHistory object!");
    } else if (!BuildingPrefabs.GHOST_PREFAB) {
      Debug.Log ("You need to attach a Ghost Prefab object to BuildingPrefabs!");
    }
  }

  public void UpdateSingletons() {
    _instance = FindObjectOfType<MasterInit> ();
  }

  void Awake() {
    UpdateSingletons ();
    SanityCheck ();
  }
}
