using UnityEngine;
using System.Collections;

public class LapCounter : MonoBehaviour
{
    private int canLap = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !other.GetComponent<PlayerInfo>().isLocalPlayer)
            return;

        float direction = transform.InverseTransformVector(other.attachedRigidbody.velocity).z;

        if (direction > 0)
        //if the player is going forward relative to the lap counter.
        {
            canLap = 1;
        }
        else if (direction < 0)
        {
            canLap = -1;
        }
        else
        //edge case where you enter the trigger from the side
        {
            canLap = 0;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") || !other.GetComponent<PlayerInfo>().isLocalPlayer)
            return;

        float direction = transform.InverseTransformVector(other.attachedRigidbody.velocity).z;

        if (canLap > 0 && direction > 0)
        {
            other.GetComponent<PlayerInfo>().CmdAddLaps(1);
        }
        else if (canLap < 0 && direction < 0)
        {
            other.GetComponent<PlayerInfo>().CmdAddLaps(-1);
        }

        canLap = 0;//Shouldn't do anything, but just in case.
    }
}