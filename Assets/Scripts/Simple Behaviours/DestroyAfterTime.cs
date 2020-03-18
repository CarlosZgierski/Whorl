
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float DestroyInTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyThis", DestroyInTime);
    }

    // Update is called once per frame
    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
