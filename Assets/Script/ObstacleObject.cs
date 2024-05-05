using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    public bool isObstacle;
    public bool isLillyPad;

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            if (isObstacle && !isLillyPad)
            {
                Debug.Log("Player has collided with an obstacle.");
            }
            else if (isLillyPad)
            {
                Debug.Log("Player has landed on a lillypad.");
            }
        }
    }
}
