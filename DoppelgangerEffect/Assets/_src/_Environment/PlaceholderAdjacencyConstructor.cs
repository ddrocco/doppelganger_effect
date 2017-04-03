using UnityEngine;
using System.Collections;

public class PlaceholderAdjacencyConstructor : MonoBehaviour {
  protected BoxCollider _coillider;
  public RoomObject first;
  public RoomObject second;

  void Start () {
    _coillider = GetComponent<BoxCollider> ();
    // PlaceholderAdjacencyDetection ();
	}

  void Update () {
    float yExtents = _coillider.bounds.extents.y;
    float zExtents = _coillider.bounds.extents.z;
    Vector3 bottom_forward_edge = transform.position - yExtents * transform.up + zExtents * transform.forward;
    Vector3 bottom_back_edge = transform.position - yExtents * transform.up - zExtents * transform.forward;
    Debug.DrawRay (bottom_forward_edge, -2f * transform.forward, Color.blue);
    Debug.DrawRay (bottom_back_edge, 2f * transform.forward, Color.blue);
  }

  public void PlaceholderAdjacencyDetection() {
    float yExtents = _coillider.bounds.extents.y;
    float zExtents = _coillider.bounds.extents.z;
    Vector3 bottom_forward_edge = transform.position - yExtents * transform.up + zExtents * transform.forward;
    Vector3 bottom_back_edge = transform.position - yExtents * transform.up - zExtents * transform.forward;
    RaycastHit forward_hit;
    RaycastHit back_hit;
    if (Physics.Raycast (bottom_forward_edge, -transform.forward, out forward_hit, 2f, Constants.ROOM_DETECTION_CULLING_MASK)) {
      first = forward_hit.collider.GetComponentInParent<RoomObject> ();
    }
    if (Physics.Raycast (bottom_back_edge, transform.forward, out back_hit, 2f, Constants.ROOM_DETECTION_CULLING_MASK)) {
      second = back_hit.collider.GetComponentInParent<RoomObject> ();
    }

    if (first == null || second == null) {
      Debug.Log (gameObject.name + " was not adjacent to two rooms.");
      return;
    } else {
      Debug.Log (gameObject.name + " registering adjacency between " + first.id + " and " + second.id);
    }

    first.adjacent_rooms.Add (second.id);
    second.adjacent_rooms.Add (first.id);
    if (DebugConstants.ENABLE_PRINT_ROOM_DISTANCE_LIST) {
      Debug.Log ("[DebugConstants.ENABLE_PRINT_ROOM_DISTANCE_LIST] ADJ: " + first.adjacent_rooms.Count + ", " + second.adjacent_rooms.Count);
    }
  }
}
