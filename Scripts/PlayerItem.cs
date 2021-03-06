using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;


public class PlayerItem : MonoBehaviourPunCallbacks
{
	public TextMeshProUGUI playerName;
	public Image backroundImage;
	public Color highlightColor;
	public GameObject LeftButton;
	public GameObject RightButton;
	Player player;

	ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

	public Image playerAvatar;
	public Sprite[] avatars;

    public void SetPlayerInfo(Player _player)
    {
    	playerName.text = _player.NickName;
    	player = _player;

        UpdatePlayerItem(player);
        
    }

    public void ApplyLocalChanges()
    {
    	backroundImage.color = highlightColor;
    	LeftButton.SetActive(true);
    	RightButton.SetActive(true);
    }

    public void OnClickLeftArrow()
    {
    	if((int)playerProperties["playerAvatar"] == 0)
    	{
    		playerProperties["playerAvatar"] = avatars.Length - 1;
            print(playerProperties["playerAvatar"]);
    	}
    	else
    	{
    		playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
            print(playerProperties["playerAvatar"]);
    	}
    	
    	PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickRightArrow()
    {
    	if((int)playerProperties["playerAvatar"] == avatars.Length - 1)
    	{
    		playerProperties["playerAvatar"] = 0;
            print(playerProperties["playerAvatar"]);
    	}
    	else
    	{
    		playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
            print(playerProperties["playerAvatar"]);
    	}
    	
    	PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
    	if(player == targetPlayer)
    	{
    		UpdatePlayerItem(targetPlayer);
    	}
    }

    void UpdatePlayerItem(Player player)
    {
    	if(player.CustomProperties.ContainsKey("playerAvatar"))
    	{
    		playerAvatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];
    		playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
            
    	}
    	else
    	{
    		playerProperties["playerAvatar"] = 0;
    	}
    }
}
