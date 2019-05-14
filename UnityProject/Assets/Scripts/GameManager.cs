using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviourPunCallbacks
{
	[SerializeField]
	GameObject mLobbyPlayerPrefab = null;
	[SerializeField]
	Text mTelop = null;
	public override void OnLeftRoom()
	{
		SceneManager.LoadScene("Login");
	}
	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}
	public void Win()
	{
		mTelop.gameObject.SetActive(true);
		mTelop.text = "You Win";
		photonView.RPC("Lose", RpcTarget.Others);
	}
	[PunRPC]
	void Lose()
	{
		mTelop.gameObject.SetActive(true);
		mTelop.text = "You Lose";
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
		}
	}
}
