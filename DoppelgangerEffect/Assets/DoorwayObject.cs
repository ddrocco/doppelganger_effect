using UnityEngine;
using System.Collections;

public class DoorwayObject : InteractableObject {
  float smooth = 2.0f;
  float DoorOpenAngle = 90.0f;
  public bool open = false;
  private Vector3 defaultRot;
  private Vector3 openRot;


  protected void Start () {
    defaultRot = transform.eulerAngles;
    openRot = new Vector3(defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
  }

  public override void Interacted() {
    open = !open;
  }

  void FixedUpdate() {
    if (open) {
      //Open door
      transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.fixedDeltaTime * smooth);
    } else {
      //Close door
      transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.fixedDeltaTime * smooth);
    }
  }
}
