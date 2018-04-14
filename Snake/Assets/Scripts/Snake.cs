using UnityEngine;

public class Snake : MonoBehaviour
{
    public Snake Next { get; set; }

    public void RemoveTail()
    {
        Destroy(this.gameObject);
    }
}
