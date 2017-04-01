using UnityEngine;
using System.Collections;

public enum Direction {
  NORTH,
  EAST,
  SOUTH,
  WEST,
};

public struct LocationState {
  public Vector3 pos;
  public Quaternion facing;
  public int room_id;
}

public struct MovementState {
  public Vector3 movement;
  public float rotationX;
  public float rotationY;
  public MovementState(Vector3 movement, float rotationX, float rotationY) {
    this.movement = movement;
    this.rotationX = rotationX;
    this.rotationY = rotationY;
  }
}

public enum MovementType {
  WALKING,
  SPRINTING
}

public struct PlayerInput {
  public Vector3 movement;
  public float LRRotation;
  public float UDRotation;
  public bool sprint;
  public bool interact;
  public bool trigger_pause;
}

