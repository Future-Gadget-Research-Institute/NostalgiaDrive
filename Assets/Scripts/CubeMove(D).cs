// Tears of a newbie


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour 
{
	public bool[] moveUp;
	public Vector3 startPosition;
	public List<Transform> currChild = new List<Transform>();

	void Start()
	{
		int children = transform.childCount;
		moveUp = new bool[children];
		for (int i = 0; i < children; i++)
		{
			if (i % 2 != 0)
			{
				moveUp[i] = true;
				startPosition = new Vector3(22.0f+i*21.0f, 1.3f, 0.4f);
				currChild.Add(transform.GetChild(i));
				currChild[i].position = startPosition;
			}
			else
			{
				moveUp[i] = false;
				startPosition = new Vector3(22.0f+i*21.0f, 31.5f, 0.4f);
				currChild.Add(transform.GetChild(i));
				currChild[i].position = startPosition;
			}
		}
	}

	void Update()
	{
		for (int i = 0; i < moveUp.Length; i++)
		{
			if (moveUp[i])
			{
				currChild[i].Translate(Vector3.up * Time.deltaTime * 20.0f);
			}
			else
			{
				currChild[i].Translate(Vector3.up * Time.deltaTime * (-20.0f));
			}
		}
	}
}
