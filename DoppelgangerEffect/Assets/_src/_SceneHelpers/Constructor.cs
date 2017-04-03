using UnityEngine;
using System.Collections;

public abstract class Constructor : MonoBehaviour {
  public string filename = "";

  public string FULL_FILENAME {
    get {
      return this.GetType ().ToString ().ToLower () + "_" + filename;
    }
  }

  public abstract bool HasBlueprints ();

  public abstract void LoadBlueprint ();

  public abstract void Construct ();

  public abstract void SaveBlueprint ();
}
