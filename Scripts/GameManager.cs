using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class GameManager : MonoBehaviourPun
{
    [Header("Players")]
    public string playerPrefabPath;
    public Transform[] spawnPoints;
    public float respawnTime;
    public PlayerController[] players;

    private int playersInGame;

    // instance
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    [PunRPC]
    void ImInGame()
    {
        playersInGame++;

        if (playersInGame == PhotonNetwork.PlayerList.Length)
        {
            SpawnPlayer();
        }
        UnityEngine.Debug.Log("GM ImInGame is finished");   
    }

    void Start()
    {
        UnityEngine.Debug.Log("Beginning of Start func in GM");

        photonView.RPC("ImInGame", RpcTarget.AllBuffered);

        UnityEngine.Debug.Log("Num of players in playerlist is " + PhotonNetwork.PlayerList.Length);

        UnityEngine.Debug.Log("When setting up players, the num of playersInGame is " + playersInGame);

        players = new PlayerController[PhotonNetwork.PlayerList.Length];
    }

    void SpawnPlayer()
    {
        UnityEngine.Debug.Log("GM spawnPlayer was called");

        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefabPath, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);

        UnityEngine.Debug.Log("Finished instantiating player prefab in spawn player");
        // initialize the player
        playerObj.GetComponent<PhotonView>().RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
}
