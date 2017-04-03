using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Foe_Detection_Handler : MonoBehaviour {
  public int currentRoom;
  public GameObject taser;

  //For haphazard use:
  private Vector3 displacement;
  private bool playerSpotted = false;
  public float visionWidth = 75f;

  //Exclamation points:
  public bool isAttentive = false;

  //Player spotted:
  public bool isAggressive = false;
  [HideInInspector]
  public float timeSincePlayerSpotted = 10f;
  [HideInInspector]
  public float timeUntilPlayerLost = 1f;
  float baseSpeed;
  public float sprintMultiplier = 5f;

  public int jurisdictionZone;
  public Camera myCam;

  int cullingMask;

  float shoveDisorientationTime = 1f;
  float timeUntilOriented;

  public bool isDeaf = false;

  void Start () { 
    myCam = GetComponent<Camera>();
    myCam.enabled = false;

    timeUntilOriented = shoveDisorientationTime;
  }

  void Update () {
    displacement = PlayerPhysicsController.player.transform.position - transform.position;
    CalculateVisualDetection();
    React();
  }

  void CalculateVisualDetection() {
    if (Vector3.Distance(PlayerPhysicsController.player.transform.position, transform.position) < 21f) {
      myCam.enabled = true;
      Plane[] planes = GeometryUtility.CalculateFrustumPlanes(myCam);
      myCam.enabled = false;
      bool detected;
      if (GeometryUtility.TestPlanesAABB(planes, PlayerPhysicsController.player.GetComponent<Collider>().bounds)) {
        RaycastHit hit;
        Vector3 heading = PlayerPhysicsController.player.transform.position - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading.normalized;
        if (Physics.Raycast(transform.position, direction, out hit, distance, cullingMask)) {
          if (hit.collider.CompareTag("Player") == true) {
            detected = true;
          } else {
            detected = false;
          }
        } else {
          detected = false;
        }
      } else {
        detected = false;
      }

      if (detected) {
        playerSpotted = true;
      } else {
        playerSpotted = false;
      }
    } else {
      playerSpotted = false;
    }
  }

  void React() {
    if (playerSpotted) {
      PlayerSpotted();
      // MusicPlayer.Spotted(this);
    } else if (isAggressive) {
      GetComponentInParent<NavMeshAgent>().speed = baseSpeed * sprintMultiplier;
    }
  }

  void PlayerSpotted() {
    isAggressive = true;
    isDeaf = false;
    if (timeSincePlayerSpotted >= timeUntilPlayerLost) {
      //GetComponent<AudioSource>().clip = SelectRandomClip(AudioDefinitions.main.GuardSpotsPlayer);
      GetComponent<AudioSource>().Play();
    }
    taser.gameObject.SetActive(true);
    timeSincePlayerSpotted = 0f;
  }

  public static AudioClip SelectRandomClip(List<AudioClip> clips) {
    int i = Mathf.FloorToInt(Random.Range(0, clips.Count));
    return clips[i];
  }
}