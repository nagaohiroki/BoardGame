using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
public class GameManager : MonoBehaviourPunCallbacks
{
	[SerializeField]
	GameObject mLobbyPlayerPrefab = null;
	[SerializeField]
	Transform mCameraHandle = null;
	public override void OnLeftRoom()
	{
		SceneManager.LoadScene(0);
	}
	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		if(PhotonNetwork.IsMasterClient)
		{
			LoadArena();
		}
	}
	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		OnPlayerLeftRoom(otherPlayer);
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
		var player = PhotonNetwork.Instantiate(mLobbyPlayerPrefab.name, new Vector3(0f,5f,0f), Quaternion.identity, 0);
		LobbyPlayer lobbyPlayre = player.GetComponent<LobbyPlayer>();
		lobbyPlayre.Initialize(mCameraHandle);
	}
	void Update()
	{
		if(!PhotonNetwork.IsConnected)
		{
			OnLeftRoom();
		}

	}
}
