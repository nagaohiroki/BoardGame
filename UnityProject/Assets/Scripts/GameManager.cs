using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine;
public class GameManager : MonoBehaviourPunCallbacks
{
	[SerializeField]
	GameObject mLobbyPlayerPrefab = null;
	public override void OnLeftRoom()
	{
		SceneManager.LoadScene("Login");
	}
	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
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
		PhotonNetwork.Instantiate(mLobbyPlayerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
	}
	void Update()
	{
		if(!PhotonNetwork.IsConnected)
		{
			OnLeftRoom();
		}
	}
}
