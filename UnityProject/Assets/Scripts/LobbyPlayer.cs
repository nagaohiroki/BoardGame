using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
public class LobbyPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
	[SerializeField]
	Rigidbody mRigidody = null;
	[SerializeField]
	TextMesh mText = null;
	GameManager mGameManager;
	readonly List<bool> mCardList = new List<bool>();
	int mIndex;
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
		Camera.main.transform.parent.position = transform.position;
	}

	void InitializeCardList()
	{
		// カード初期化
		for(int i = 0; i < 14; i++)
		{
			mCardList.Add(false);
		}
	}
	void UpdateGame()
	{
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			--mIndex;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			++mIndex;
		}
		mIndex = (int)Mathf.Repeat(mIndex, mCardList.Count);
		string text = string.Empty;
	//	var hand = new List<mCardList>();
	//	foreach (var card in  mCardList)
	//	{
	//		if (card)
	//		{
	//			hand.Add(card);
	//		}
	//	}
		for(int i = 0; i < mCardList.Count; i++)
		{
			string arrow = i == mIndex ? ">" : " ";
			text += string.Format("{0}{1}:{2}\n", arrow, i, mCardList[i]);
		}
		if(mGameManager != null)
		{
			mGameManager.UpdateText(text);
		}
		if(Input.GetKeyDown(KeyCode.Return))
		{
			mCardList[mIndex] = true;
			if(mGameManager != null)
			{
				mGameManager.TurnEnd();
			}
		}
	}

	void ResetPosition()
	{
		if (mRigidody != null)
		{
			mRigidody.Sleep();
			mRigidody.MovePosition(new Vector3(0.0f, 1.0f, 0.0f));
		}
	}
	void Start()
	{
		mText.text = photonView.Owner.NickName;
		InitializeCardList();
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
			ResetPosition();
		}
		Move();
	//	UpdateGame();
	}
}
