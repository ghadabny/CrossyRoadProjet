

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
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !isHopping)
            TryMove(Vector3.forward + new Vector3(0, 0, CalculateZDifference()), new Vector3(0, -90, 0));
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isHopping)
            TryMove(-Vector3.forward + new Vector3(0, 0, CalculateZDifference()), new Vector3(0, 90, 0));
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isHopping)
            TryMove(Vector3.left, new Vector3(0, 180, 0));
    }

    private float CalculateZDifference()
    {
        return Mathf.Round(transform.position.z) - transform.position.z;
    }

    private void TryMove(Vector3 direction, Vector3 rotation)
    {
        Vector3 newPosition = transform.position + direction;
        if (!Physics.Raycast(transform.position, direction, direction.magnitude))
        {
            MoveToPosition(newPosition, rotation);
        }
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
        rb.MovePosition(newPosition);
        terrainGenerator.SpawnTerrain(false, newPosition);
        yield return new WaitForSeconds(0.3f);
        isHopping = false;

        if (CurrentLog == null)
        {
            transform.parent = null;
            IsOnLog = false;
        }
    }

    public void FinishHop()
    {
        isHopping = false;
    }

    public bool hasNotMovedMuch()
    {
        return Vector3.Distance(transform.position, lastPosition) < movementThreshold;
    }
}



