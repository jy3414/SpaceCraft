using UnityEngine;
using System.Collections;

public class FollowUV : MonoBehaviour {

	public float parralax = 2f;

	void Update () {

		MeshRenderer mr = GetComponent<MeshRenderer>();

		Material mat = mr.material;

		Vector2 offset = mat.mainTextureOffset;

		offset.x = transform.position.x / 4f / parralax;
		offset.y = transform.position.y / 4f / parralax;

		mat.mainTextureOffset = offset;

	}

}
