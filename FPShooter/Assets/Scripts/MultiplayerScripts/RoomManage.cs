using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomManage : MonoBehaviourPunCallbacks
{
    public static RoomManage Instance;

    public GameObject Player;

    public Transform[] SpawnPoint = new Transform[5];

    public GameObject RoomCamera;

    public GameObject PlayerNickNameRoom;

    public GameObject ConnectionRoom;

    string NickName = "Player";

    [HideInInspector]
    public int Kills = 0;
    public int Death = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeNickName(string Name)
    {
        NickName = Name;
    }

    public void JoinRoomButtonPressed()
    {
        Debug.Log("Connection going");

        PhotonNetwork.ConnectUsingSettings();

        PlayerNickNameRoom.SetActive(false);

        ConnectionRoom.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to Server");

        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        Debug.Log("Join Lobby");

        PhotonNetwork.JoinOrCreateRoom("Room", null, null);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("We in a Room");

        RoomCamera.SetActive(false);

        RespawnPlayer();
    }
    public void RespawnPlayer()
    {
        int random = Random.Range(0, SpawnPoint.Length);
        GameObject _Player = PhotonNetwork.Instantiate(Player.name, SpawnPoint[random].position, Quaternion.identity);
        _Player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _Player.GetComponent<PlayerHealth>().IsLocalPlayer = true;

        _Player.GetComponent<PhotonView>().RPC("SetNickName", RpcTarget.AllBuffered, NickName);
        PhotonNetwork.LocalPlayer.NickName = NickName;
    }
    public void SetHashes()
    {
        try
        {
            Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;

            hash["Kills"] = RoomManage.Instance.Kills;
            hash["Death"] = RoomManage.Instance.Death;

            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
        catch
        {

        }
    }
}
