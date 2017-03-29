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
}
