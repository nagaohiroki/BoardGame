//using System.Collections;
//using System.Collections.Generic;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviourPunCallbacks
{
	[SerializeField]
	Text mUserName = null;
	[SerializeField]
	GameObject mLoginForm = null;
	[SerializeField]
	LobbyPlayer mLobbyPlayer = null;

	//ログインボタンを押したときに実行される
	public void Connect()
	{
		if(string.IsNullOrEmpty(mUserName.text))
		{
			return;
		}
		if(!PhotonNetwork.IsConnected)
		{
			PhotonNetwork.GameVersion = "1";
			PhotonNetwork.ConnectUsingSettings();
		}
	}
	public override void OnConnectedToMaster()
	{
		Debug.Log("マスターがルームに入りました。");
		PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
	}

	public override void OnJoinedRoom()
	{
		if(mUserName != null)
		{
			PhotonNetwork.LocalPlayer.NickName = mUserName.text;
		}
		Debug.Log(PhotonNetwork.LocalPlayer.NickName + "がルームに入りました");
		mLoginForm.SetActive(false);
		LoginPlayer();
	}
	void LoginPlayer()
	{
		PhotonNetwork.Instantiate(mLobbyPlayer.name, Vector3.zero, Quaternion.identity, 0);
	}
}
