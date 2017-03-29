using UnityEngine;
using System.Collections;
using InControl;

/* KEYBOARD CONTROLS
 * 
 * IJKL - movement
 * Mouse - look around
 * Tab - switch between Stan's mouse controls and Q's mouse controls
 * Left click - interact
 * O - sprint
 * 
*/

public enum ControlScheme {
  UNKNOWN,
  MOUSE_AND_KEYBOARD,
  XBOX,
  RIFT,
};

public class InputMapper : MonoBehaviour {

  private ControlScheme control_scheme = ControlScheme.UNKNOWN;
  private InputDevice device;
  private PlayerController player_controller;

  /* Simple Generic Methods */

  void Start() {
    InitializeInputDevice ();
    if (control_scheme == ControlScheme.UNKNOWN) {
      Debug.LogError ("UNKNOWN CONTROL SCHEME!");
    }
    player_controller = PlayerController.player;
  }

  void Update () {
    PlayerInput input = ReceiveInputFromDevice ();
    player_controller.UpdateInput (input);
  }

  /* Input Mapping Methods */

  void InitializeInputDevice() {
    device = InputManager.ActiveDevice;
    control_scheme = ControlScheme.XBOX;
    foreach (var Device in InputManager.Devices) {
      if (Device.Name.Contains("Unknown")) {
        control_scheme = ControlScheme.MOUSE_AND_KEYBOARD;
      }
    }
    if (InputManager.Devices.Count == 0) {
      control_scheme = ControlScheme.MOUSE_AND_KEYBOARD;
    }

    switch(control_scheme) {
    case ControlScheme.MOUSE_AND_KEYBOARD:
      Debug.Log ("Mouse and Keyboard controls active");
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      break;
    case ControlScheme.XBOX:
      Debug.Log ("XBOX controls active");
      break;
    }
  }

  PlayerInput ReceiveInputFromDevice() {
    switch(control_scheme) {
    case ControlScheme.MOUSE_AND_KEYBOARD:
      return GetKeyboardInput ();
    case ControlScheme.XBOX:
      return GetXBoxInput ();
    case ControlScheme.RIFT:
      Debug.LogError ("RIFT CONTROL SCHEME!");
      break;
    case ControlScheme.UNKNOWN:
      Debug.LogError ("UNKNOWN CONTROL SCHEME!");
      break;
    }
    return new PlayerInput ();
  }

  /* Input Methods */

  PlayerInput GetKeyboardInput() {
    PlayerInput return_struct = new PlayerInput();
    if (Input.GetKeyDown(KeyCode.Mouse0)) { // Left Click
      return_struct.interact = true;
    }
    if (Input.GetKeyDown(KeyCode.U)) { // Interact
      return_struct.interact = true;
    }
    return_struct.movement = Vector3.zero;
    return_struct.xRotation = Input.GetAxis("Mouse X") * Constants.PLAYER_ROTATION_RATE * ConfigurableConstants.PLAYER_LOOK_SENSITIVITY;
    return_struct.yRotation = Input.GetAxis("Mouse Y") * Constants.PLAYER_ROTATION_RATE * ConfigurableConstants.PLAYER_LOOK_SENSITIVITY;
    return return_struct;
  }

  PlayerInput GetXBoxInput() {
    PlayerInput return_struct = new PlayerInput();
    if (device.Action1.WasPressed) { // A button on Xbox
      return_struct.interact = true;
    }
    if (device.Action2.WasPressed) { // B button on Xbox
      return_struct.interact = true;
    }
    if (device.Action3.WasPressed) { // X button on Xbox
      return_struct.interact = true;
    }
    if (device.Action4.WasPressed) { // Y button on Xbox
      return_struct.interact = true;
    }
    return_struct.movement = new Vector3 (device.LeftStickX.Value, 0f, device.LeftStickY.Value);
    return_struct.xRotation = device.RightStickX.Value;
    return_struct.yRotation = device.RightStickY.Value;
    return return_struct;
  }
}
