using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Launcher : MonoBehaviourPunCallbacks
{
	[SerializeField]
	byte mMaxPlayerPerRoom = 0;
	[SerializeField]
	Text mUserName = null;
	[SerializeField]
	GameObject mLoginForm = null;
	bool mIsConnecting;
	public override void OnConnectedToMaster()
	{
		if(mIsConnecting)
		{
			PhotonNetwork.JoinRandomRoom();
		}
	}
	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		var room = new RoomOptions();
		room.MaxPlayers = mMaxPlayerPerRoom;
		PhotonNetwork.CreateRoom(null, room);
	}
	public override void OnJoinedRoom()
	{
		if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
		{
			PhotonNetwork.LoadLevel("Room1");
		}
	}
	public void Connect()
	{
		mIsConnecting = true;
		if(mUserName == null || string.IsNullOrEmpty(mUserName.text))
		{
			return;
		}
		PhotonNetwork.LocalPlayer.NickName = mUserName.text;
		if(PhotonNetwork.IsConnected)
		{
			PhotonNetwork.JoinRandomRoom();
		}
		else
		{
			PhotonNetwork.GameVersion = "1";
			PhotonNetwork.ConnectUsingSettings();
		}
		if(mLoginForm != null)
		{
			mLoginForm.SetActive(false);
		}
	}
	void Awake()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
	}
}
