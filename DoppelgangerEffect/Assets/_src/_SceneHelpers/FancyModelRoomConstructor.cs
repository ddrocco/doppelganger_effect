using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FancyModelRoomConstructor : Constructor {
  public RoomCollection room_collection;
  public FancyModelRoomBlueprints blueprints;

  void Awake() {
    room_collection = new RoomCollection ();
  }

  public override bool HasBlueprints() {
    return (blueprints != null);
  }

  public override void LoadBlueprint() {
    string blueprint_json = Parser.LoadResourceTextfile (Parser.ROOM_GENERATOR_PATH, FULL_FILENAME);
    blueprints = JsonUtility.FromJson<FancyModelRoomBlueprints> (blueprint_json);
    Debug.Log ("Loaded blueprints from " + Parser.ROOM_GENERATOR_PATH + FULL_FILENAME + ".json");
  }

  public override void Construct() {
    Debug.Log ("Constructed room from blueprints");
  }

  public override void SaveBlueprint() {
    blueprints = new FancyModelRoomBlueprints ();
    RoomEdge edge0 = new RoomEdge ();
    edge0.direction = Direction.NORTH;
    edge0.length = 5;
    blueprints.edges.Add(edge0);
    RoomEdge edge1 = new RoomEdge ();
    edge0.direction = Direction.WEST;
    edge0.length = 4;
    blueprints.edges.Add(edge1);
    RoomEdge edge2 = new RoomEdge ();
    edge0.direction = Direction.EAST;
    edge0.length = 5;
    blueprints.edges.Add(edge2);
    RoomEdge edge3 = new RoomEdge ();
    edge0.direction = Direction.SOUTH;
    edge0.length = 4;
    blueprints.edges.Add(edge3);
    string blueprint_json = JsonUtility.ToJson (blueprints);
    Parser.SaveResourceTextfile (blueprint_json, Parser.ROOM_GENERATOR_PATH, FULL_FILENAME);
    Debug.Log ("Saved blueprints to " + Parser.ROOM_GENERATOR_PATH + FULL_FILENAME + ".json");
  }
}
