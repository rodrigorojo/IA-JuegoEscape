using UnityEngine;
using System.Collections;

public class UserPlayer : Player {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public override void TurnUpdate ()
	{
        transform.position += (moveDestination - transform.position).normalized * moveSpeed * Time.deltaTime;

        base.TurnUpdate ();
	}
}
