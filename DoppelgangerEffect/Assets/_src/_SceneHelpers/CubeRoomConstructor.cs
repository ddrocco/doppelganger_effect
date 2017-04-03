using UnityEngine;
using System.Collections;

public class CubeRoomConstructor : Constructor {
  public RoomCollection room_collection;
  public CubeRoomBlueprints blueprints;

  void Awake() {
    room_collection = gameObject.AddComponent<RoomCollection>();
  }

  public override bool HasBlueprints() {
    return (blueprints != null);
  }

  public override void LoadBlueprint() {
    string blueprint_json = Parser.LoadResourceTextfile (Parser.ROOM_GENERATOR_PATH, FULL_FILENAME);
    blueprints = JsonUtility.FromJson<CubeRoomBlueprints> (blueprint_json);
    Debug.Log ("Loaded blueprints from " + Parser.ROOM_GENERATOR_PATH + FULL_FILENAME + ".json");
  }

  public override void Construct() {
    Debug.Log ("Constructed room from blueprints");
  }

  public override void SaveBlueprint() {
    string blueprint_json = JsonUtility.ToJson (blueprints);
    Parser.SaveResourceTextfile (blueprint_json, Parser.ROOM_GENERATOR_PATH, FULL_FILENAME);
    Debug.Log ("Saved blueprints to " + Parser.ROOM_GENERATOR_PATH + FULL_FILENAME + ".json");
  }
}
