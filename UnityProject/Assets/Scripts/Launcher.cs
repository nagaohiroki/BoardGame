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
	// ------------------------------------------------------------------------
	/// @brief ログインボタンをおしたときの動作
	// ------------------------------------------------------------------------
	public void Connect()
	{
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
	public override void OnConnectedToMaster()
	{
		PhotonNetwork.JoinRandomRoom();
		Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
	}
	public override void OnDisconnected(DisconnectCause cause)
	{
	}
	// 🍜😺
	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		var room = new RoomOptions();
		room.MaxPlayers = mMaxPlayerPerRoom;
		PhotonNetwork.CreateRoom(null, room);
	}
}
