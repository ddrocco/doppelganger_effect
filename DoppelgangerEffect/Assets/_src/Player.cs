using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
  public static Player main;
  public Rigidbody body;
  PlayerController controller;

  void InitializeComponents() {
    // Controller
    controller = GetComponent<PlayerController> ();
    if (controller == null)
      controller = gameObject.AddComponent<PlayerController>();
    // Rigidbody
    body = GetComponent<Rigidbody>();
    if (body == null) {
      body = gameObject.AddComponent<Rigidbody> ();
    }
    body.useGravity = false;
  }

  void Awake() {
    main = this;
    InitializeComponents();
  }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public LocationState GetLocationState() {
    LocationState state;
    state.pos = transform.position;
    state.facing = transform.rotation;
    int room_id = GetRoomId ();
    state.room_id = room_id;
    DebugText.player_room_text = room_id.ToString () + " (" + Time.time.ToString("0") + ")";
    PlayerStateHistory.current_occupied_room = room_id;
    DebugLogging.PrintLocationState (state);
    return state;
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
