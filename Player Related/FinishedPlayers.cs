using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class FinishedPlayers : NetworkBehaviour
{
    private SyncListString finishedPlayersList = new SyncListString();
    public SyncListString FinishedPlayersList { get { return finishedPlayersList; } }

    private SyncListFloat finishedPlayerTimesList = new SyncListFloat();
    //finished player time corresponding index to finished players list.
    public SyncListFloat FinishedPlayerTimesList { get { return finishedPlayerTimesList; } }

    [Command]
    public void CmdAddFinishedPlayer(GameObject player)
    {
        if (!player.CompareTag("Player"))
        {
            Debug.LogError("Must be a player to add to finished list.");
            return;
        }

        if (player.GetComponent<PlayerInfo>().Finished)
            return;

        finishedPlayersList.Add(player.GetComponent<PlayerInfo>().PlayerName);

        finishedPlayerTimesList.Add(player.GetComponent<PlayerTime>().RaceTime);
    }

}
