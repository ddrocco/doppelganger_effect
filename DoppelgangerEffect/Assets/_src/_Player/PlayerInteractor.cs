using UnityEngine;
using System.Collections;

public class PlayerInteractor : MonoBehaviour {
  private InteractableObjectBindable _bound_object = null;
  public InteractableObjectBindable BOUND_OBJECT {
    get {
      return _bound_object;
    }
  }

  public void Toggle() {
    if (BOUND_OBJECT) {
      BOUND_OBJECT.Unbind ();
      _bound_object = null;
    }
    DebugLogging.DrawDebugRay (
      transform.position,
      Constants.PLAYER_INTERACTION_DISTANCE * transform.forward);
    
    RaycastHit hit;
    if (Physics.Raycast (transform.position, transform.forward, out hit, Constants.PLAYER_INTERACTION_DISTANCE, Constants.INTERACTABLE_CULLING_MASK)) {
      InteractableObjectBindable obj_bindable = Lib.GetComponentInTree<InteractableObjectBindable> (hit.collider.gameObject);
      if (!obj_bindable) {
        InteractableObject obj = Lib.GetComponentInTree<InteractableObject> (hit.collider.gameObject);
        obj.Interacted ();
        return;
      } else {
        _bound_object = obj_bindable;
        BOUND_OBJECT.Bind (this);
        return;
      }
    } else {
      //Eventually we'll play a sound or something here.  Maybe draw a raycast.
    }
  }
}
