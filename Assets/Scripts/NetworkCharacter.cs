using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour
{

	protected Vector3 realPosition = Vector3.zero;
	protected Quaternion realRotation = Quaternion.identity;
	protected float lastUpdateTime = 0.1f;

	// Update is called once per frame
	void Update()
	{	
		SmoothMove ();
	}

	protected void SmoothMove() {
		if (photonView.isMine)
		{
			//Do nothing -- we are moving
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position, realPosition, lastUpdateTime);
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, lastUpdateTime);
		}
	}

	protected void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			//This is OUR player.We need to send our actual position to the network
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else
		{
			//This is someone else's player.We need to receive their positin (as of a few millisecond ago, and update our version of that player.
			realPosition = (Vector3)stream.ReceiveNext();
			realRotation = (Quaternion)stream.ReceiveNext();
		}
	}
}
