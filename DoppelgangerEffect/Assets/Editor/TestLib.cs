using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class TestLib : MonoBehaviour {

  [Test]
  public void AngleClamp180_0() {
    Assert.AreEqual (
      0f,
      Lib.AngleClamp180 (0f, -30f, 30f));
  }

  [Test]
  public void AngleClamp180_15() {
    Assert.AreEqual (
      15f,
      Lib.AngleClamp180 (15f, -30f, 30f));
  }

  [Test]
  public void AngleClamp180_N15() {
    Assert.AreEqual (
      -15f,
      Lib.AngleClamp180 (-15f, -30f, 30f));
  }

  [Test]
  public void AngleClamp180_55() {
    Assert.AreEqual (
      60f,
      Lib.AngleClamp180 (55f, -30f, 30f));
  }

  [Test]
  public void AngleClamp180_100() {
    Assert.AreEqual (
      100f,
      Lib.AngleClamp180 (100f, -30f, 30f));
  }

  [Test]
  public void AngleClamp180_200() {
    Assert.AreEqual (
      200f,
      Lib.AngleClamp180 (200f, -30f, 30f));
  }

  [Test]
  public void AngleClamp180_220() {
    Assert.AreEqual (
      210f,
      Lib.AngleClamp180 (220f, -30f, 30f));
  }

  [Test]
  public void AngleClamp180_325() {
    Assert.AreEqual (
      330f,
      Lib.AngleClamp180 (325f, -30f, 30f));
  }
}
