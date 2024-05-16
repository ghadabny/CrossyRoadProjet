using System.Collections;
using UnityEngine;

public class LogHandler : MonoBehaviour
{
    /*
    public float timeToStayOnLog = 50f; // Time in seconds the player will stay on the log

    private void OnTriggerEnter(Collider collider)
    {
        AttachPlayer(collider);
    }

    private void OnTriggerStay(Collider collider)
    {
        // This check ensures we try to attach the player only if needed
        if (!collider.GetComponent<Player>().IsOnLog)
        {
            AttachPlayer(collider);
        }
    }

    private void AttachPlayer(Collider collider)
    {
        var player = collider.GetComponent<Player>();
        if (player != null && !player.IsOnLog && !player.isHopping) // Ensure player is not hopping between logs.
        {
            player.CurrentLog = GetComponent<MovingObject>();
            collider.transform.SetParent(transform); // Set the log as the player's parent to move with the log.
            collider.transform.localPosition = Vector3.zero; // Optionally center the player on the log.
            player.IsOnLog = true;
            Debug.Log("Player attached to the log.");
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        var player = collider.GetComponent<Player>();
        if (player != null && player.IsOnLog && !player.isHopping) // Ensure they're not in mid-hop
        {
            StartDetachTimer(player);
        }
    }

    private void StartDetachTimer(Player player)
    {
        player.StartCoroutine(DetachAfterDelay(player));
    }

    private IEnumerator DetachAfterDelay(Player player)
    {
        yield return new WaitForSeconds(timeToStayOnLog);
        // Final check to ensure conditions haven't changed
        if (player.IsOnLog && !player.isHopping)
        {
            DetachPlayer(player);
        }
    }

    private void DetachPlayer(Player player)
    {
        if (player.IsOnLog && !player.isHopping) // Double-check to ensure state consistency
        {
            player.CurrentLog = null;
            player.transform.SetParent(null);
            player.IsOnLog = false;
            Debug.Log("Player detached from the log.");
        }
    }*/


}
