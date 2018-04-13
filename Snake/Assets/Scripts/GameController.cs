using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	public GameObject snakePrefab;
	public Snake head;
	public Snake Tail;

	/// <summary>
	/// Defines the direction moving
	/// </summary>
	public int NESW;

	public Vector2 nexPos;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("TimeInvoke", 0, 0.5f);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TimerInvoke()
    {
        Movement();
    }

	/// <summary>
	/// Process the Movement
	/// </summary>
	private void Movement()
	{
		GameObject gameObject;

		nexPos = head.transform.position;

		switch (NESW) {
		case 0:
			nexPos = new Vector2 (nexPos.x, nexPos.y + 1);
			break;
		case 1:
			nexPos = new Vector2 (nexPos.x + 1, nexPos.y);
			break;
		case 2:
			nexPos = new Vector2 (nexPos.x, nexPos.y - 1);
			break;
		case 3:
			nexPos = new Vector2 (nexPos.x - 1, nexPos.y);
			break;
		default:
			break;
		}

		gameObject = Instantiate(snakePrefab, nexPos, transform.rotation);
	}
}
