using UnityEngine;
using System.Collections;

public class MasterInit : MonoBehaviour {
  static MasterInit _main;

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
    } else if (!FindObjectOfType<PlayerStateHistory> ().ghost_prefab) {
      Debug.Log ("You need to attach a Ghost Prefab object to PlayerStateHistory!");
    }
  }

  public void UpdateSingletons() {
    _main = FindObjectOfType<MasterInit> ();
    PlayerStateHistory._main = FindObjectOfType<PlayerStateHistory> ();
  }

  void Awake() {
    UpdateSingletons ();
    SanityCheck ();
  }
}
