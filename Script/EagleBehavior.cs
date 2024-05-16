using UnityEngine;
using UnityEngine.SceneManagement;

public class Eagle : MonoBehaviour
{
    public GameObject playerGameObject;  // Assign the player GameObject in the editor
    private Player player;               // Reference to the Player component
    public float swoopDownTime ;    // Time in seconds before the eagle starts its swoop
    private float timer = 0f;            // Timer to track inactivity

    public float swoopSpeed;        // Speed at which the eagle moves towards the player
    public Transform swoopTarget;        // Position the eagle aims for when swooping
    private bool isSwooping = false;     // Flag to check if the eagle is currently swooping

    void Start()
    {
        if (playerGameObject != null)
        {
            player = playerGameObject.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player component not found on the assigned playerGameObject!");
            }
            // Set the initial position of the eagle above the player, off-screen
            transform.position = new Vector3(playerGameObject.transform.position.x, 20f, playerGameObject.transform.position.z);
        }
        else
        {
            Debug.LogError("Player GameObject has not been assigned in the inspector!");
        }
    }

    void Update()
    {
       
            Debug.Log("Current Speed: " + swoopSpeed);
          Debug.Log("Distance to Target: " + Vector3.Distance(transform.position, swoopTarget.position));

            
        
        if (player != null && player.hasNotMovedMuch())
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        if (timer >= swoopDownTime && !isSwooping)
        {
            StartSwooping();
        }

        if (isSwooping)
        {
            TransformToTarget();
        }
    }

    private void StartSwooping()
    {
        Debug.Log("Eagle has started swooping!");
        isSwooping = true;
    }

    private void TransformToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, swoopTarget.position, swoopSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, swoopTarget.position) < 1.0f)
        {
            CatchPlayer();
        }
    }


    private void CatchPlayer()
    {
        Debug.Log("Eagle has caught the player!");
        isSwooping = false;
        // Trigger game over or reset player position
        SceneManager.LoadScene("GameOver");
    }
}
