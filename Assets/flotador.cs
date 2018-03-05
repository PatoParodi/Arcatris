using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flotador : MonoBehaviour {

	public float waterLevel = 4;
	public float floatHeight = 2;
	public float bounceDamp = 0.05f;
	public Vector3 buoyancyCenterOffset;

	float forceFactor;
	Vector3 actionPoint;
	Vector3 upLift;

	private void Update()
	{
		actionPoint = transform.position + transform.TransformDirection(buoyancyCenterOffset);
		forceFactor = 1 - ((actionPoint.y - waterLevel) / floatHeight);

		if(forceFactor > 0)
		{
			upLift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody2D>().velocity.y * bounceDamp);
			GetComponent<Rigidbody2D>().AddForceAtPosition(upLift, actionPoint);
		}
	}

}
