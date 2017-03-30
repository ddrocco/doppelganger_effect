using UnityEngine;
using System.Collections;

public class DoorwayObject : MonoBehaviour {
  public RoomObject first;
  public RoomObject second;

  BoxCollider collider;
	// Use this for initialization
	void Start () {
    collider = GetComponent<BoxCollider> ();
    PlaceholderAdjacencyDetection ();
	}
	
	// Update is called once per frame
  void Update () {
    float yExtents = collider.bounds.extents.y;
    float zExtents = collider.bounds.extents.z;
    Vector3 bottom_forward_edge = transform.position - collider.bounds.extents.y * transform.up + collider.bounds.extents.z * transform.forward;
    Vector3 bottom_back_edge = transform.position - collider.bounds.extents.y * transform.up - collider.bounds.extents.z * transform.forward;
    Debug.DrawRay (bottom_forward_edge, -2f * transform.forward, Color.blue);
    Debug.DrawRay (bottom_back_edge, 2f * transform.forward, Color.blue);
	}

  void PlaceholderAdjacencyDetection() {
    float yExtents = collider.bounds.extents.y;
    float zExtents = collider.bounds.extents.z;
    Vector3 bottom_forward_edge = transform.position - collider.bounds.extents.y * transform.up + collider.bounds.extents.z * transform.forward;
    Vector3 bottom_back_edge = transform.position - collider.bounds.extents.y * transform.up - collider.bounds.extents.z * transform.forward;
    RaycastHit forward_hit;
    RaycastHit back_hit;
    if (Physics.Raycast (bottom_forward_edge, -transform.forward, out forward_hit, 2f, Constants.ROOM_DETECTION_CULLING_MASK)) {
      first = forward_hit.collider.GetComponentInParent<RoomObject> ();
    }
    if (Physics.Raycast (bottom_back_edge, transform.forward, out back_hit, 2f, Constants.ROOM_DETECTION_CULLING_MASK)) {
      second = back_hit.collider.GetComponentInParent<RoomObject> ();
    }

    if (first == null || second == null) {
      return;
    }

    first.adjacent_rooms.Add (second.id);
    second.adjacent_rooms.Add (first.id);
    if (DebugConstants.ENABLE_PRINT_ROOM_DISTANCE_LIST) {
      Debug.Log ("[DebugConstants.ENABLE_PRINT_ROOM_DISTANCE_LIST] ADJ: " + first.adjacent_rooms.Count + ", " + second.adjacent_rooms.Count);
    }
  }
}
