using UnityEngine;
using System.Linq;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] GameObject PlayerHolder;

    [Header("Options")]
    [SerializeField] float RefreshRate;

    [Header("UI")]
    [SerializeField] GameObject[] Slots;

    [SerializeField] TextMeshProUGUI[] ScoreTexts;

    [SerializeField] TextMeshProUGUI[] NameTexts;

    [SerializeField] TextMeshProUGUI[] KDTexts;

    private void Start()
    {
        InvokeRepeating(nameof(Refresh), 1, RefreshRate);
    }
    public void Refresh()
    {
        foreach(var slot in Slots)
        {
            slot.SetActive(false);
        }

        var SortedPlayerList = 
            (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player).ToList();

        int i = 0;
        foreach (var player in SortedPlayerList)
        {
            Slots[i].SetActive(true);

            if (player.NickName == "")
                player.NickName = "Unnamed";

            NameTexts[i].text = player.NickName;
            ScoreTexts[i].text = player.GetScore().ToString();

            if (player.CustomProperties["Kills"] != null)
            {
                KDTexts[i].text = player.CustomProperties["Kills"] + "/" + player.CustomProperties["Death"];
            }
            else
            {
                KDTexts[i].text = "0/0";
            } 

            i++;
        }
    }
    private void Update()
    {
        PlayerHolder.SetActive(Input.GetKey(KeyCode.Tab));
    }
}
