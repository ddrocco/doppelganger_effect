using UnityEngine;
using System.Collections;

public class InteractableObjectBindable : InteractableObject {
  /* Interaction module variables */
  PlayerInteractor _interactor = null;
  public PlayerInteractor INTERACTOR {
    get {
      return _interactor;
    }
  }

  public override void Interacted () {

  }

  public void Bind (PlayerInteractor i) {
    _interactor = i;
  }

  public void Unbind () {
    _interactor = null;
  }
}
