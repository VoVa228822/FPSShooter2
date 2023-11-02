using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public TextMeshProUGUI HPText;

    public bool IsLocalPlayer;

    public int Health;
    [PunRPC]
    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            if (IsLocalPlayer)
            {
                RoomManage.Instance.RespawnPlayer();

                RoomManage.Instance.Death++;
                RoomManage.Instance.SetHashes();
            }


            Destroy(gameObject);
        }

        HPText.text = Health.ToString();
    }
}
