using UnityEngine;
using System.Collections;

public class MasterInit : MonoBehaviour {
  void Awake() {
    if (!GetComponentInChildren<InControl.InControlManager> ()) {
      Debug.Log ("You need to attach an InControl object!");
    }
    if (!GetComponentInChildren<PlayerStateHistory> ()) {
      Debug.Log ("You need to attach a PlayerStateHistory object!");
    } else if (!GetComponentInChildren<PlayerStateHistory> ().ghost_prefab) {
      Debug.Log ("You need to attach a Ghost Prefab object!");
    }
  }
}
