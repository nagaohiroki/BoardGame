using UnityEngine;
using Photon.Pun;
public class LobbyPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
	[SerializeField]
	Rigidbody mRigidody = null;
	[SerializeField]
	TextMesh mText = null;
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.IsWriting)
		{
			stream.SendNext(mText.gameObject.activeSelf);
		}
		else
		{
			mText.gameObject.SetActive((bool)stream.ReceiveNext());
		}
	}
	void Move()
	{
		if(mRigidody == null)
		{
			return;
		}
		var vec = Vector3.zero;
		vec.x = Input.GetAxis("Horizontal");
		vec.z = Input.GetAxis("Vertical");
		mRigidody.AddForce(vec, ForceMode.VelocityChange);
	}
	void Start()
	{
		mText.text = photonView.Owner.NickName;
	}
	void Update()
	{
		if(!photonView.IsMine && PhotonNetwork.IsConnected)
		{
			return;
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			mText.gameObject.SetActive(!mText.gameObject.activeSelf);
		}
		Move();
		Camera.main.transform.parent.position = transform.position;
	}
}
