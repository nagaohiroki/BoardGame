using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class GameManager : MonoBehaviourPunCallbacks
{
	[SerializeField]
	GameObject mLobbyPlayerPrefab = null;
	[SerializeField]
	Text mTelop = null;
	[SerializeField]
	Text mDebugLog = null;
	Dictionary<string, bool> mTurn;
	public override void OnLeftRoom()
	{
		SceneManager.LoadScene("Login");
	}
	public override void OnJoinedRoom()
	{
		Debug.LogWarning("ログイン");
	}
	public override void OnPlayerEnteredRoom(Player other)
	{
		// 他のプレイヤーが入ってきた
		Debug.LogWarning("ログイン" + other.NickName);
	}
	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}
	public void TurnEnd()
	{
		photonView.RPC("TurnNext", RpcTarget.Others);
	}
   
	[PunRPC]
	void TurnNext()
	{
		mDebugLog.text = string.Empty;
		foreach(var player in PhotonNetwork.CurrentRoom.Players)
		{
			var text =  player.Key + ":" + player.Value.NickName + ":" + player.Value.UserId + "\n";
			mDebugLog.text += text;
		}
	}
	void LoadArena()
	{
		PhotonNetwork.LoadLevel("Room1");
	}
	void Start()
	{
		if(!PhotonNetwork.IsConnected)
		{
			return;
		}
		var go = PhotonNetwork.Instantiate(mLobbyPlayerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
		var lobbyPlayer = go.GetComponent<LobbyPlayer>();
		lobbyPlayer.Initialize(this);
	}
	void Update()
	{
		if(!PhotonNetwork.IsConnected)
		{
			OnLeftRoom();
			return;
		}
	}
	void SetTelop(string inTelop)
	{
		if(string.IsNullOrEmpty(inTelop))
		{
			mTelop.gameObject.SetActive(false);
			return;
		}
		mTelop.gameObject.SetActive(true);
		mTelop.text = inTelop;
	}
}
