using UnityEngine;
public class LobbyPlayer : MonoBehaviour
{
	Transform mCameraHandle = null;
	public void Initialize(Transform inCameraHandle)
	{
		mCameraHandle = inCameraHandle;
	}
	void Update()
	{
		if(mCameraHandle != null)
		{
			mCameraHandle.position = transform.position;
		}
	}
}
