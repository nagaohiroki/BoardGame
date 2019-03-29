using Photon.Pun;
using UnityEngine;
public class LobbyPlayer : MonoBehaviourPunCallbacks
{
	[SerializeField]
	TextMesh mUserName = null;
	void Start()
	{
		if(photonView.Owner != null)
		{
			mUserName.text = photonView.Owner.NickName;
		}
	}
	void Update()
	{
		if(!photonView.IsMine)
		{
			return;
		}
		var vec = Vector3.zero;
		vec.x = Input.GetAxis("Horizontal");
		vec.z = Input.GetAxis("Vertical");
		transform.Translate(vec.normalized);
	}
}
