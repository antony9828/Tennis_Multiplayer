using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class CustomNetworkManager : NetworkManager
{

    public List<Racket> Players = new List<Racket>();

    #region SingleTon
    public static CustomNetworkManager Instance { get; private set; }
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void InstantiateBall()
    {
        GameObject ball = Instantiate(spawnPrefabs[0]);
        NetworkServer.Spawn(ball);
    }


    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        var currectPlayersCount = NetworkServer.connections.Count;
        if (currectPlayersCount <= startPositions.Count)
        {
            if (currectPlayersCount == 2)
                InstantiateBall();
            GameObject player = Instantiate(playerPrefab, startPositions[currectPlayersCount - 1].position, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
            Players.Add(player.GetComponent<Racket>());
        }
        else
        {
            conn.Disconnect();
        }
    }
}
