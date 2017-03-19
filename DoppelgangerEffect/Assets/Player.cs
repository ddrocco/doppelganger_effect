using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
  public static Player main;
  public Rigidbody body;
  PlayerController controller;

  void InitializeComponents() {
    // Controller
    controller = GetComponent<PlayerController> ();
    if (controller == null)
      controller = gameObject.AddComponent<PlayerController>();
    // Rigidbody
    body = GetComponent<Rigidbody>();
    if (body == null)
      body = gameObject.AddComponent<Rigidbody> ();
    body.useGravity = false;
  }

  void Awake() {
    main = this;
    InitializeComponents();
  }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public LocationState GetLocationState() {
    LocationState state;
    state.pos = transform.position;
    state.facing = transform.rotation;
    return state;
  }
}
