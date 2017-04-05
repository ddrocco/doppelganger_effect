using UnityEngine;
using System.Collections;

public class PlaceholderAdjacencyConstructor : MonoBehaviour {
  protected BoxCollider _coillider {
    get {
      return GetComponent<BoxCollider> ();
    }
  }
  public RoomObject first;
  public RoomObject second;

  void Update () {
    DebugDrawAdjacencyDetectors ();
  }

  public void DebugDrawAdjacencyDetectors () {
    float yExtents = _coillider.bounds.extents.y;
    float zExtents = _coillider.bounds.extents.z;
    Vector3 bottom_forward_edge = transform.position - yExtents * transform.up + zExtents * transform.forward;
    Vector3 bottom_back_edge = transform.position - yExtents * transform.up - zExtents * transform.forward;
    Debug.DrawRay (bottom_forward_edge, -2f * transform.forward, Color.blue);
    Debug.DrawRay (bottom_back_edge, 2f * transform.forward, Color.blue);
  }

  public void PlaceholderAdjacencyDetection() {
    DebugDrawAdjacencyDetectors ();
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
      return;
    } else {
      transform.parent.gameObject.name = "Doorway_" + first.id.ToString() + "-" + second.id.ToString();
    }

    first.adjacent_rooms.Add (second.id);
    second.adjacent_rooms.Add (first.id);
  }
}
