using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour 
{
	public GameObject player;
	public Vector3 offset;
	void Start () 
	{
		offset = transform.position - player.transform.position;
		offset.y = 0;
		offset.x += 5.0f;
	}

	void LateUpdate () 
	{
		transform.position = player.transform.position + offset;
	}
}
