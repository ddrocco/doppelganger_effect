using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {
  public static DebugText _main;

  public Text _player_room_text;
  public static string player_room_text {
    get {
      return _main._player_room_text.text;
    }
    set {
      _main._player_room_text.text = value;
    }
  }

  public Text _player_target_movement_text;
  public static string player_target_movement_text {
    get {
      return _main._player_target_movement_text.text;
    }
    set {
      _main._player_target_movement_text.text = value;
    }
  }


  void Awake() {
    _main = this;
    _player_room_text = GameObject.FindGameObjectWithTag ("PlayerRoomUIText").GetComponent<Text> ();
    _player_room_text.transform.SetParent(transform);
    _player_target_movement_text = GameObject.FindGameObjectWithTag ("PlayerTargetMovementUIText").GetComponent<Text> ();
    _player_target_movement_text.transform.SetParent(transform);
  }
}
