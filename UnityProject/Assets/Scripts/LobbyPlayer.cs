using UnityEngine;
using Photon.Pun;
public class LobbyPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
	[SerializeField]
	Rigidbody mRigidody = null;
	[SerializeField]
	TextMesh mText = null;
	GameManager mGameManager;
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
	public void Initialize(GameManager inGameManager)
	{
		mGameManager = inGameManager;
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
			if(mGameManager != null)
			{
				mGameManager.TurnEnd();
			}
		}
		Move();
		Camera.main.transform.parent.position = transform.position;
	}
}
