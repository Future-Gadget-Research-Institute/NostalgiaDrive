using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsInstance : MonoBehaviour 
{
	Transform player;
	public List<GameObject> unitList = new List<GameObject>();
	public int space = 21;
	void Start () 
	{
		player = GameObject.Find ("Player").transform;
		foreach (Transform temp in transform)
		{
			unitList.Add (temp.gameObject);
		}

//		int children = transform.childCount;
//		for (int i = 0; i < children; i++)
//		{
//			unitList.Add (transform.GetChild (i).gameObject);
//		}


	}
	
	void Update () 
	{
		if (unitList[0].transform.position.x < player.position.x - space)
		{
			unitList[0].transform.position = unitList [unitList.Count - 1].transform.position + Vector3.right * space;
			unitList.Insert (unitList.Count,unitList[0]);
			unitList.RemoveAt (0);
		}
	}
}
