﻿using UnityEngine;
using System.Collections;

public class Walking : MonoBehaviour 
{
	public static Walking instance;

	public float walkspeed = 3F;
	public float runspeed = 7F;
	public float dist;

	public Cursor cursor;

	private float speed;
	private Vector3 pos;
	private Transform tr;


	void Awake()
	{
		instance = this;
	}
	
	void Start ()
	{
		//Here we set the Values from the current position	
		pos = GameManagerScript.control.overworldPos;
		transform.position = pos;
		tr = transform;
	}
	
	void Update () 
	{
		GetSpeed ();
		Movement ();
		Interact ();
	}

	private void GetSpeed ()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			speed = runspeed;
		}
		else
		{
			speed = walkspeed;
		}
	}
	
	private void Movement() 
	{
		//If we press any Key we will add a direction to the position ...
		//using Vector3.'anydirection' will add 1 to that direction
		
		
		//But we Check if we are at the new Position, before we can add some more
		//it will prevent to move before you are at your next 'tile'
		if ((Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)) && tr.position == pos /*&& CheckTarget (Vector3.right)*/) 
		{
			Debug.Log("going right!");
			pos += Vector3.right;
		}
		else if ((Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)) && tr.position == pos /*&& CheckTarget (Vector3.left)*/) 
		{
			Debug.Log("going left!");
			pos += Vector3.left;
		}
		else if ((Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)) && tr.position == pos /*&& CheckTarget (Vector3.up)*/) 
		{
			Debug.Log("going up!!");
			pos += Vector3.up;
		}
		else if ((Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow)) && tr.position == pos /*&& CheckTarget (Vector3.down)*/) 
		{
			Debug.Log("going down!");
			pos += Vector3.down;
		} 
		//Here you will move Towards the new position ...
		transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
		
		
	}

	private bool CheckTarget (Vector3 dir)
	{
		RaycastHit2D obst;
		obst = Physics2D.Raycast (transform.position, dir, 1F);
		dist = obst.distance;
		if (dist > 0.6F)
		{
			Debug.Log("returning true");
			return true;
		}
		else
		{
			Debug.Log("returning false");
			return false;
		}
	}

	private void Interact ()
	{
		if (Input.GetKeyDown(KeyCode.Space) && tr.position == pos)
		{
			cursor.Interact ();
		}
	}
}





