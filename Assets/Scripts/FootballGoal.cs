using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballGoal : MonoBehaviour
{
  private List<Material> materials = new List<Material>();
  private Rigidbody rbody;
  private float startY;

  private void Awake()
  {
    startY = transform.position.y;
    foreach (var r in GetComponentsInChildren<Renderer>())
    {
      materials.Add(r.material);
    }
    rbody = GetComponent<Rigidbody>();
  }

  public void Reset()
  {
    HasScored = false;

    foreach (var m in materials)
    {
      m.color = Color.white;
    }

    rbody.velocity = Vector3.zero;

    this.transform.rotation = Quaternion.identity;

    // Random position
    this.transform.position = new Vector3(Random.Range(-5f, 6f),
                                          startY,
                                          Random.Range(-3f, 3f));
  }

  private void OnTriggerEnter(Collider collision)
  {
    if (collision.tag == "Player")
    {
      HasScored = true;

      foreach (var m in materials)
      {
        m.color = Color.green;
      }
    }
  }

  public bool HasScored
  {
    get;
    private set;
  }
}
