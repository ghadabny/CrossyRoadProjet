using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private Text scoreText;
    [SerializeField] private MovingObject currentLog;

    private int score;
    private Animator animator;
    private Rigidbody rb;
    private bool isHopping;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        score++;
        
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isHopping)
            TryMove(new Vector3(1, 0, CalculateZDifference()));
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !isHopping)
            TryMove(new Vector3(0, 0, 1));
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isHopping)
            TryMove(new Vector3(0, 0, -1));
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isHopping)
            TryMove(new Vector3(-1, 0, CalculateZDifference()));
    }

    private float CalculateZDifference()
    {
        return transform.position.z % 1 != 0 ? Mathf.Round(transform.position.z) - transform.position.z : 0;
    }

    private void TryMove(Vector3 difference)
    {
        Vector3 newPosition = transform.position + difference;
        Vector3 raycastOrigin = transform.position + difference.normalized * 0.1f;  // Start the ray just ahead of the player
        float rayLength = difference.magnitude + 0.1f;  // Slightly longer than the difference to catch all possible collisions

        if (!Physics.Raycast(raycastOrigin, difference.normalized, rayLength))
        {
            animator.SetTrigger("hop");
            isHopping = true;
            transform.position = newPosition;
            terrainGenerator.SpawnTerrain(false, transform.position);
            StartCoroutine(HandleHopDown());
        }
    }

    IEnumerator HandleHopDown()
    {
        yield return new WaitForSeconds(0.2f); // Wait for the peak of the hop
        rb.MovePosition(rb.position - new Vector3(0, 0.2f, 0)); // Move back down
        isHopping = false;
    }



    private void OnCollisionEnter(Collision collision)
    {
        var movingObject = collision.collider.GetComponent<MovingObject>();
        var obstacleObject = collision.collider.GetComponent<ObstacleObject>();

        if (movingObject != null)
        {
            if (movingObject.isLog)
            {
                currentLog = movingObject;  // Set the current log
                transform.parent = collision.collider.transform;  // Parent to the log
            }
        }
        else if (obstacleObject != null && obstacleObject.isObstacle)
        {
            isHopping = false;  // Stop hopping
            transform.parent = collision.collider.transform;  // Optionally parent to the obstacle, might not be needed
        }
        else
        {
            transform.parent = null;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        var movingObject = collision.collider.GetComponent<MovingObject>();
        if (movingObject == currentLog)
        {
            currentLog = null;  // Clear the current log
            transform.parent = null;  // Detach from the log
            Debug.Log("Left the log");
        }
    }


    public void FinishHop()
    {
        isHopping = false;
    }


}
