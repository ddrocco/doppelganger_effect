using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public struct RoomEdge {
  public Direction direction;
  public int length;
}

[System.Serializable]
public class FancyModelRoomBlueprints : MonoBehaviour {
  public List<RoomEdge> edges = new List<RoomEdge>();
}
