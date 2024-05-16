using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private Text scoreText;
    public MovingObject CurrentLog { get; set; }
    public GameObject specificCoin;
    public bool IsOnLog { get; set; }

    private Animator animator;
    private Rigidbody rb;
    public bool isHopping;

    private Vector3 lastPosition;
    private float movementThreshold = 2; // Threshold distance to consider significant movement
    private float checkInterval = 1.0f; // How often to check for movement in seconds
    private float lastCheckTime = 0;

    private int backStepsCount = 0; // Counter for backward steps
    private const int maxBackSteps = 3; // Maximum allowed backward steps

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (Time.time - lastCheckTime >= checkInterval)
        {
            lastCheckTime = Time.time;
            lastPosition = transform.position;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isHopping)
        {
            TryMove(Vector3.right, Vector3.zero);
            ScoreManager.instance.AddScore(1);
            ResetBackStepsCount();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !isHopping)
        {
            TryMove(Vector3.forward + new Vector3(0, 0, CalculateZDifference()), new Vector3(0, -90, 0));
            ResetBackStepsCount();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isHopping)
        {
            TryMove(-Vector3.forward + new Vector3(0, 0, CalculateZDifference()), new Vector3(0, 90, 0));
            ResetBackStepsCount();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isHopping)
        {
            if (backStepsCount < maxBackSteps)
            {
                TryMove(Vector3.left, new Vector3(0, 180, 0));
                backStepsCount++;
            }
            else
            {
                Debug.Log("Cannot move back more than 3 steps!");
            }
        }
    }

    private float CalculateZDifference()
    {
        return Mathf.Round(transform.position.z) - transform.position.z;
    }

    private void TryMove(Vector3 direction, Vector3 rotation)
    {
        Vector3 newPosition = transform.position + direction;

        // Check if the new position will collide with a tree
        if (!WillCollideWithTree(newPosition))
        {
            if (IsOnLog)
            {
                // Detach from the log immediately
                transform.parent = null;
                IsOnLog = false;
            }
            MoveToPosition(newPosition, rotation);
        }
        else
        {
            Debug.Log("Blocked by a tree!");
        }
    }

    private bool WillCollideWithTree(Vector3 position)
    {
        // Perform a small sphere cast at the target position to check for collisions with trees
        float radius = 0.3f; // Adjusted radius for a smaller, more precise check
        Collider[] hitColliders = Physics.OverlapSphere(position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Tree"))
            {
                return true;
            }
        }
        return false;
    }

    private void MoveToPosition(Vector3 position, Vector3 rotation)
    {
        animator.SetTrigger("hop");
        isHopping = true;
        StartCoroutine(MoveAndHandleHop(position, rotation));
    }

    IEnumerator MoveAndHandleHop(Vector3 newPosition, Vector3 newRotation)
    {
        transform.rotation = Quaternion.Euler(newRotation);
        yield return null;  // Ensure we have a frame to detach from the parent before moving
        rb.MovePosition(newPosition);
        terrainGenerator.SpawnTerrain(false, newPosition);
        yield return new WaitForSeconds(0.1f);  // Reduced delay for faster response
        isHopping = false;
    }

    public void FinishHop()
    {
        isHopping = false;
    }

    public bool hasNotMovedMuch()
    {
        return Vector3.Distance(transform.position, lastPosition) < movementThreshold;
    }

    private void ResetBackStepsCount()
    {
        backStepsCount = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        MovingObject movingObject = collision.collider.GetComponent<MovingObject>();
        if (movingObject != null && movingObject.isLog)
        {
            transform.SetParent(movingObject.transform);
            CurrentLog = movingObject;
            IsOnLog = true;
            Debug.Log("Player attached to log");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        MovingObject movingObject = collision.collider.GetComponent<MovingObject>();
        if (movingObject != null && movingObject.isLog)
        {
            transform.SetParent(null);
            CurrentLog = null;
            IsOnLog = false;
            Debug.Log("Player detached from log");
        }
    }
}
