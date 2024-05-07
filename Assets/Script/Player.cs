using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private Text scoreText;
    [SerializeField] private MovingObject currentLog;

    //private int score;
    private Animator animator;
    private Rigidbody rb;
    private bool isHopping;

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
            ScoreManager.instance.AddScore(1);//Vector3.forward + new Vector3(0, 0, CalculateZDifference())
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !isHopping)
            TryMove(Vector3.forward + new Vector3(0, 0, CalculateZDifference()));//Vector3.left
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isHopping)
            TryMove(-Vector3.forward + new Vector3(0, 0, CalculateZDifference()));//Vector3.right
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isHopping)
            TryMove(Vector3.left);//-Vector3.forward + new Vector3(0, 0, CalculateZDifference())

        
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
        yield return new WaitForSeconds(0.3f); // Animation duration
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
