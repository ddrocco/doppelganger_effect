using UnityEngine;
using System.Collections;

public class Lib : MonoBehaviour {
  public static Color Colorify(int i) {
    // A simple version.  Doesn't consider separate fractions.  105 unique combinations.
    float red = Mathf.PingPong((float)i/7f, 1f);
    float blue = Mathf.PingPong((float)i/5f, 1f);
    float green = Mathf.PingPong((float)i/3f, 1f);
    return new Color(red, green, blue);
  }

  public static float AngleClamp180(float value, float min, float max) {
    // A helper method for clamping doubly rotating objects.
    // 180 degrees is a FULL rotation.
    int quadrant = (int)(value)/90;
    float adjusted_value;
    if (value % 90f > 45f) {
      adjusted_value = (value % 90f) - 90f;
      quadrant += 1;
    } else {
      adjusted_value = value % 90f;
    }
    float clamped_adjusted_value = Mathf.Clamp (adjusted_value, min, max);
    return clamped_adjusted_value + 90f * quadrant;
  }

  public static T GetComponentInTree<T>(GameObject game_object) {
    if (game_object.GetComponent<T> () != null) {
      return game_object.GetComponent<T> ();
    } else {
      return game_object.GetComponentInParent<T> (); 
    }
  }

  public static Vector3 FlattenY(Vector3 input) {
    float m = input.magnitude;
    input.y = 0;
    return input.normalized * m;
  }

  public static void DestroyChildrenAndSelf(Transform transform) {
    foreach (Transform child_transform in transform) {
      DestroyChildrenAndSelf (child_transform);
    }
    GameObject.DestroyImmediate (transform.gameObject);
  }
}
