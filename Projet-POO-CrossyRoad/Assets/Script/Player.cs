using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private Text scoreText;
    [SerializeField] private MovingObject currentLog;
    public GameObject specificCoin;

    //private int score;
    private Animator animator;
    private Rigidbody rb;
    private bool isHopping;
    private bool isOnLog = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.UpArrow) && !isHopping)
        {
            TryMove(Vector3.right);
            ScoreManager.instance.AddScore(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !isHopping)
            TryMove(Vector3.forward + new Vector3(0, 0, CalculateZDifference()));
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isHopping)
            TryMove(-Vector3.forward + new Vector3(0, 0, CalculateZDifference()));
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isHopping)
            TryMove(Vector3.left);

        
    }

    private float CalculateZDifference()
    {
        return Mathf.Round(transform.position.z) - transform.position.z;
    }

    private void TryMove(Vector3 direction)
    {
        Vector3 newPosition = transform.position + direction;
        if (!Physics.Raycast(transform.position, direction, direction.magnitude))
        {
            MoveToPosition(newPosition);
        }
    }

    private void MoveToPosition(Vector3 position)
    {
        animator.SetTrigger("hop");
        isHopping = true;
        StartCoroutine(MoveAndHandleHop(position));
    }

    IEnumerator MoveAndHandleHop(Vector3 newPosition)
    {
        rb.MovePosition(newPosition);
        terrainGenerator.SpawnTerrain(false, newPosition);
        yield return new WaitForSeconds(0.3f); 
        isHopping = false;
        DetachFromLog();
        
    }

    private void DetachFromLog()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
            currentLog = null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Entered trigger with: {other.gameObject.name}, Tag: {other.tag}");
        if (other.CompareTag("CrossyCoin"))
        {
            Debug.Log("Coin collected");
            ScoreManager.instance.AddCoins(1);
            Destroy(other.gameObject);
        }
        else
        {
            Debug.Log($"Ignored trigger with: {other.gameObject.name}, Tag: {other.tag}");
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        var movingObject = collision.collider.GetComponent<MovingObject>();
        var obstacleObject = collision.collider.GetComponent<ObstacleObject>();

        if (movingObject != null)
        {
            if (movingObject.isLog)
            {
                currentLog = movingObject;
                transform.parent = collision.collider.transform;
                isOnLog = true; // Set isOnLog to true when the player is on the log
            }
        }
        else if (obstacleObject != null && obstacleObject.isObstacle)
        {
            if (!isOnLog) // Check if not on log before processing death or stop
            {
                isHopping = false;
                transform.parent = collision.collider.transform;
            }
        }
        else
        {
            transform.parent = null;
            isOnLog = false; // Reset when not colliding with a log
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        var movingObject = collision.collider.GetComponent<MovingObject>();
        if (movingObject == currentLog)
        {
            currentLog = null;
            transform.parent = null;
            isOnLog = false; // Reset isOnLog when exiting the log
            Debug.Log("Left the log");
        }
    }

    private void CheckIfPlayerCanDie()
    {
        if (!isOnLog)
        {
            // Logic to kill the player
            Debug.Log("Player died");
        }
    }


    public void FinishHop()
    {
        isHopping = false;
    }


}
