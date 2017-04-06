using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
  public static Player INSTANCE;
  PlayerPhysicsController controller;

  static LocationState _current_location_state;
  public static LocationState CURRENT_LOCATION_STATE {
    get {
      return _current_location_state;
    }
  }
  static LocationState _last_location_state;
  public static LocationState LAST_LOCATION_STATE {
    get {
      return _last_location_state;
    }
  }

  public static int CURRENT_OCCUPIED_ROOM {
    get {
      return _current_location_state.room_id;
    }
  }

  void InitializeComponents() {
    // Controller
    controller = GetComponent<PlayerPhysicsController> ();
    if (controller == null)
      controller = gameObject.AddComponent<PlayerPhysicsController>();
  }

  void Awake() {
    INSTANCE = this;
    InitializeComponents();
  }

	// Use this for initialization
	void Start () {
    _last_location_state = new LocationState ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void UpdateLocationState() {
    LocationState new_state;
    new_state.pos = transform.position;
    new_state.facing = transform.rotation;
    int new_room_id = GetRoomId ();
    new_state.room_id = new_room_id;

    _last_location_state = _current_location_state;
    _current_location_state = new_state;

    if (CURRENT_LOCATION_STATE.room_id != new_room_id) {
      PlayerStateHistory.PlayerRoomChangeEvent ();
    }
      
    DebugText.player_room_text = new_room_id.ToString () + " (" + Time.time.ToString("0") + ")";
  }

  int GetRoomId() {
    RaycastHit hit;
    if (Physics.Raycast (transform.position, Vector3.down, out hit, Constants.ROOM_DETECTION_RAYCAST_DISTANCE, Constants.ROOM_DETECTION_CULLING_MASK)) {
      return hit.transform.GetComponentInParent<RoomObject> ().id;
    }
    DebugLogging.DrawDebugRay(transform.position, new Vector3(0, -Constants.ROOM_DETECTION_RAYCAST_DISTANCE, 0));
    return -1;
  }
}
