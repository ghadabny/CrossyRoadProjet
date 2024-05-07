
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool isLog;

    private float minZ = -25.0f;  
    private float maxZ = 25.0f;   

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.z < minZ || transform.position.z > maxZ)
        {
            Destroy(gameObject);  
        }
    }
}

