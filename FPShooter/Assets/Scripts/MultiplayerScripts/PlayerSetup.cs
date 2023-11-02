using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;


public class PlayerSetup : MonoBehaviour
{
    public PlayerMove Movement;

    public  GameObject Camera;

    public GameObject NickNameHolder;

    public string NickName;

    public TextMeshPro NameText;
    public void IsLocalPlayer()
    {
        NickNameHolder.SetActive(false);
        Movement.enabled = true;
        Camera.SetActive(true);
    }

    [PunRPC]
    public void SetNickName(string Name)
    {
        NickName = Name;

        NameText.text = NickName;
    }
}