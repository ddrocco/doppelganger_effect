using UnityEngine;
using System.Collections;

public struct LocationState {
  public Vector3 pos;
  public Quaternion facing;
}

public struct WorldspaceState {
  public Vector3 current_position;
  public Vector3 current_movement;
  public Vector3 target_movement;
  public float current_rotationX;
  public float target_rotationX;
  public float current_rotationY;
  public float target_rotationY;
}

public struct PlayerInput {
  public Vector3 movement;
  public float xRotation;
  public float yRotation;
  public bool sprint;
  public bool interact;
  public bool trigger_pause;
}

