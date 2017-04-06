using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHeightHandler : MonoBehaviour {
  static float _player_target_height_adjustment;
  public static float PLAYER_TARGET_HEIGHT_ADJUSTMENT {
    get {
      return _player_target_height_adjustment;
    }
  }

  BoxCollider _collider;
  BoxCollider COLLIDER {
    get {
      if (_collider == null) {
        _collider = GetComponent<BoxCollider> ();
      }
      return _collider;
    }
  }

  // A series of points in local space from the PLAYER.
  List<Vector3> _detectors;
  public Vector3[] DETECTORS {
    get {
      return _detectors.ToArray();
    }
  }
  public int DETECTOR_COUNT {
    get {
      if (_detectors == null) {
        return 0;
      }
      return _detectors.Count;
    }
  }

  List<float> _detector_heights;
  public float[] DETECTOR_HEIGHTS {
    get {
      return _detector_heights.ToArray();
    }
  }

  void GenerateDetectors_Simple3x3() {
    _detectors = new List<Vector3> ();
    float xExtents = COLLIDER.bounds.extents.x;
    float yExtents = COLLIDER.bounds.extents.y;
    float zExtents = COLLIDER.bounds.extents.z;
    Vector3 bottom_central = Vector3.zero - yExtents * Vector3.up;

    Vector3 forward_edge = bottom_central + zExtents * transform.forward;
    Vector3 back_edge = bottom_central - zExtents * transform.forward;
    Vector3 right_edge = bottom_central + xExtents * transform.right;
    Vector3 left_edge = bottom_central - xExtents * transform.right;

    Vector3 forward_right = bottom_central + zExtents * transform.forward + xExtents * transform.right;
    Vector3 forward_left = bottom_central + zExtents * transform.forward - xExtents * transform.right;
    Vector3 back_right = bottom_central - zExtents * transform.forward + xExtents * transform.right;
    Vector3 back_left = bottom_central - zExtents * transform.forward - xExtents * transform.right;

    _detectors.Add (forward_left);
    _detectors.Add (forward_edge);
    _detectors.Add (forward_right);
    _detectors.Add (left_edge);
    _detectors.Add (bottom_central);
    _detectors.Add (right_edge);
    _detectors.Add (back_left);
    _detectors.Add (back_edge);
    _detectors.Add (back_right);
  }

  void GetDetectorHeights() {
    int i = 0;
    foreach (Vector3 detector_localpos in DETECTORS) {
      RaycastHit hit;
      if (Physics.Raycast (
        transform.position + (transform.rotation * detector_localpos),
        Vector3.down,
        out hit,
        Constants.PLAYER_FLOOR_DETECTION_DISTANCE,
        Constants.ROOM_DETECTION_CULLING_MASK))
      {
        _detector_heights[i] = hit.distance;
      } else {
        _detector_heights [i] = Constants.PLAYER_FLOOR_DETECTION_DISTANCE;
      }
      ++i;
    }
  }

  void CalculateTargetHeightSimple() {
    float total = 0f;
    foreach (float height in DETECTOR_HEIGHTS) {
      total += height;
    }
    float average = total / DETECTOR_COUNT;
    _player_target_height_adjustment = Constants.PLAYER_FLOAT_HEIGHT - average;
  }

  /* Calculates TargetHeight, with a higher weight assigned to higher terrain.
   * This is so that the player doesn't crash into stairs when trying to climb them,
   * and climbs faster than they descend. */
  void CalculateTargetHeightHighPreference() {
    float net_total = 0f;
    foreach (float height in DETECTOR_HEIGHTS) {
      if (height < Constants.PLAYER_FLOAT_HEIGHT) {
        net_total += Constants.PLAYER_STAIR_HEIGHT_BIAS_WEIGHT * (Constants.PLAYER_FLOAT_HEIGHT - height);
      } else {
        net_total += (Constants.PLAYER_FLOAT_HEIGHT - height);
      }
    }
    float net_average = net_total / DETECTOR_COUNT;
    _player_target_height_adjustment = net_average;
  }

  public void SetUp() {
    GenerateDetectors_Simple3x3 ();
    _detector_heights = new List<float>(new float[9]);
  }

  void HeightRaycasts() {
    if (DebugConstants.ALLOW_DEBUG_RAYS) {
      int i = 0;
      foreach (Vector3 detector_localpos in _detectors) {
        float ratio = DETECTOR_HEIGHTS [i] / Constants.PLAYER_FLOOR_DETECTION_DISTANCE;
        Debug.DrawRay (
          transform.position + (transform.rotation * detector_localpos),
          -Constants.PLAYER_FLOOR_DETECTION_DISTANCE * Vector3.up,
          new Color(1f - ratio, 0f, ratio));
      }
    }
  }

	// Use this for initialization
	void Start () {
    SetUp ();
	}
	
	// Update is called once per frame
	void Update () {
    GetDetectorHeights ();
    CalculateTargetHeightHighPreference ();
    HeightRaycasts ();
	}
}
