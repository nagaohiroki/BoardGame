using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class GameManager : MonoBehaviourPunCallbacks
{
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
		PhotonNetwork.LoadLevel("Room" + PhotonNetwork.CurrentRoom.PlayerCount);
	}
}
