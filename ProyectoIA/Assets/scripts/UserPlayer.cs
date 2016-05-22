using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserPlayer : Player {


    // Use this for initialization
    void Start () {
        objs.Add(new Objetivo("salir", 10));
        objs.Add(new Objetivo("escapar", 20));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void getView()
    {
        for (int i = (int)currentPosition.x; i < GameManager.instance.mapSizex; i++)
        {
            GameManager.instance.map[i][(int)currentPosition.y].ColorOnView(0);
        }
        base.getView();
    }


    public override void TurnUpdate ()
	{
        if (Vector3.Distance(moveDestination, transform.position) > 0.1f)
        {
            transform.position += (moveDestination - transform.position).normalized * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(moveDestination, transform.position) <= 0.1f)
            {
                transform.position = moveDestination;
                GameManager.instance.nextTurn();
            }
        }

        base.TurnUpdate();
    }
}
