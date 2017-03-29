using UnityEngine;
using System.Collections;
using System.IO;


public class Parser {
  public readonly static string ROOM_GENERATOR_PATH = "";

  public static string LoadResourceTextfile(string path_prefix, string filename) {
    string file_path = path_prefix + filename;
    TextAsset target_file = Resources.Load<TextAsset> (file_path);
    if (target_file == null) {
      Debug.LogError ("File " + file_path + ".json not found.");
      return "";
    }
    return target_file.text;
  }

  public static void SaveResourceTextfile(string json_payload, string path_prefix, string filename) {
    string file_path = path_prefix + filename;

    TextAsset target_file = Resources.Load<TextAsset> (file_path);
    if (target_file != null) {
      Debug.LogError ("File " + file_path + ".json already has data.  Delete it manually if you're serious.");
      return;
    }

    using (FileStream fs = new FileStream (file_path, FileMode.Create)) {
      using (StreamWriter writer = new StreamWriter (fs)) {
        writer.Write (json_payload);
      }
    }
    #if UNITY_EDITOR
    UnityEditor.AssetDatabase.Refresh();
    #endif
  }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
