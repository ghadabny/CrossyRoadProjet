using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private Text scoreText;
    public MovingObject CurrentLog { get; set; }
    public GameObject specificCoin;

    private Animator animator;
    private Rigidbody rb;
    public bool isHopping;
    private bool isAttachedToLog;
    private bool isMovementDisabled = false;  // Flag to disable movement

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
        if (isMovementDisabled) return;  // Do not allow movement if disabled

        if (Time.time - lastCheckTime >= checkInterval)
        {
            lastCheckTime = Time.time;
            lastPosition = transform.position;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isHopping)
        {
            TryMove(Vector3.right, Quaternion.Euler(0, 0, 0));
            ScoreManager.instance.AddScore(1);
            ResetBackStepsCount();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !isHopping)
        {
            TryMove(Vector3.forward + new Vector3(0, 0, CalculateZDifference()), Quaternion.Euler(0, -90, 0));
            ResetBackStepsCount();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isHopping)
        {
            TryMove(-Vector3.forward + new Vector3(0, 0, CalculateZDifference()), Quaternion.Euler(0, 90, 0));
            ResetBackStepsCount();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isHopping)
        {
            if (backStepsCount < maxBackSteps)
            {
                TryMove(Vector3.left, Quaternion.Euler(0, 180, 0));
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

    private void TryMove(Vector3 direction, Quaternion rotation)
    {
        Vector3 newPosition = transform.position + direction;

        // Check if the new position will collide with a tree
        if (!WillCollideWithTree(newPosition))
        {
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

    private void MoveToPosition(Vector3 position, Quaternion rotation)
    {
        animator.SetTrigger("hop");
        isHopping = true;
        if (isAttachedToLog)
        {
            DetachFromLog();
        }
        StartCoroutine(MoveAndHandleHop(position, rotation));
    }

    IEnumerator MoveAndHandleHop(Vector3 newPosition, Quaternion newRotation)
    {
        yield return null;  // Ensure we have a frame to detach from the parent before moving
        rb.MovePosition(newPosition);
        rb.MoveRotation(newRotation);
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
        Debug.Log("Collision detected with " + collision.collider.name);

        MovingObject movingObject = collision.collider.GetComponent<MovingObject>();
        if (movingObject != null && movingObject.isLog)
        {
            AttachToLog(collision.collider.transform);
            Debug.Log("Player attached to log");
        }
        else if (collision.gameObject.CompareTag("CrossyCoin"))
        {
            CollectCoin(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        MovingObject movingObject = collision.collider.GetComponent<MovingObject>();
        if (movingObject != null && movingObject.isLog)
        {
            DetachFromLog();
            Debug.Log("Player detached from log");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger with: " + other.gameObject.name);
        if (other.CompareTag("CrossyCoin"))
        {
            CollectCoin(other.gameObject);
        }
    }

    private void CollectCoin(GameObject coin)
    {
        Debug.Log("Collecting and destroying coin: " + coin.name);
        Destroy(coin);
        ScoreManager.instance.AddCoins(1);
    }

    private void AttachToLog(Transform logTransform)
    {
        CurrentLog = logTransform.GetComponent<MovingObject>();
        isAttachedToLog = true;
        StartCoroutine(FollowLog());
    }

    private void DetachFromLog()
    {
        isAttachedToLog = false;
        CurrentLog = null;
        transform.parent = null; // Detach from the log's transform
    }

    private IEnumerator FollowLog()
    {
        while (isAttachedToLog && CurrentLog != null)
        {
            Vector3 logPosition = CurrentLog.transform.position;
            transform.position = new Vector3(logPosition.x, transform.position.y, logPosition.z);
            yield return null;  // Wait for the next frame
        }
    }

    public void DisableMovement()
    {
        isMovementDisabled = true;
    }

    public void EnableMovement()
    {
        isMovementDisabled = false;
    }
}
