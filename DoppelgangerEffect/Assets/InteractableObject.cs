using UnityEngine;
using System.Collections;

public abstract class InteractableObject : MonoBehaviour {
  protected BoxCollider _coillider;
  public abstract void Interacted ();

  protected virtual void Start () {
    _coillider = GetComponent<BoxCollider> ();
  }
}
