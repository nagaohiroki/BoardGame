using UnityEngine;
using Photon.Pun;
public class LobbyPlayer : MonoBehaviourPun
{
	Transform mCameraHandle = null;
	[SerializeField]
	Rigidbody mRigidody = null;
	public void Initialize(Transform inCameraHandle)
	{
		mCameraHandle = inCameraHandle;
	}
	void Update()
	{
		if(!photonView.IsMine && PhotonNetwork.IsConnected)
		{
			return;
		}
		if(mCameraHandle != null)
		{
			mCameraHandle.position = transform.position;
		}
		Move();
	}
	void Move()
	{
		if (mRigidody == null)
		{
			return;
		}
		var vec = Vector3.zero;
		vec.x = Input.GetAxis("Horizontal");
		vec.z = Input.GetAxis("Vertical");
		mRigidody.AddForce(vec, ForceMode.VelocityChange);
	}
}
