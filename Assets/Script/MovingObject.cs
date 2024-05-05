
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool isLog;

    private float minZ = -25.0f;  // Adjusted lower boundary based on half the scale length
    private float maxZ = 25.0f;   // Adjusted upper boundary based on half the scale length

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Check if the object has gone past the boundaries
        if (transform.position.z < minZ || transform.position.z > maxZ)
        {
            Destroy(gameObject);  // Destroy the object or deactivate it
        }
    }
}

