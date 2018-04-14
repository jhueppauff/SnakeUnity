using System;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Snake Next { get; set; }

    public static Action<String> Hit;

    void OnTriggerEnter(Collider collision)
    {
        if (Hit != null)
        {
            Hit(collision.transform.tag);
        }

        if (collision.tag.ToLowerInvariant() == "food")
        {
            Destroy(collision.gameObject);
        }
    }

    public void RemoveTail()
    {
        Destroy(this.gameObject);
    }
}
