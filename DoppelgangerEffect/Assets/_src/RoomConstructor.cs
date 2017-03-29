using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomConstructor : MonoBehaviour {
  public RoomCollection room_collection;
  public RoomBlueprints blueprint;
  public string filename = "";

  public void LoadBlueprint() {
    string blueprint_json = Parser.LoadResourceTextfile (Parser.ROOM_GENERATOR_PATH, filename);
    blueprint = JsonUtility.FromJson<RoomBlueprints> (blueprint_json);
    Debug.Log ("Loaded blueprints from " + Parser.ROOM_GENERATOR_PATH + filename + ".json");
  }

  public RoomObject Construct() {
    return new RoomObject ();
    Debug.Log ("Constructed room from blueprints");
  }

  public void SaveBlueprint() {
    blueprint = new RoomBlueprints ();
    RoomEdge edge0 = new RoomEdge ();
    edge0.direction = Direction.NORTH;
    edge0.length = 5;
    blueprint.edges.Add(edge0);
    RoomEdge edge1 = new RoomEdge ();
    edge0.direction = Direction.WEST;
    edge0.length = 4;
    blueprint.edges.Add(edge1);
    RoomEdge edge2 = new RoomEdge ();
    edge0.direction = Direction.EAST;
    edge0.length = 5;
    blueprint.edges.Add(edge2);
    RoomEdge edge3 = new RoomEdge ();
    edge0.direction = Direction.SOUTH;
    edge0.length = 4;
    blueprint.edges.Add(edge3);
    string blueprint_json = JsonUtility.ToJson (blueprint);
    Parser.SaveResourceTextfile (blueprint_json, Parser.ROOM_GENERATOR_PATH, filename);
    Debug.Log ("Saved blueprints to " + Parser.ROOM_GENERATOR_PATH + filename + ".json");
  }
}
