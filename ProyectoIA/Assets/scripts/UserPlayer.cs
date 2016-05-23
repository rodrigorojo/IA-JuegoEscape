using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserPlayer : Player {


    // Use this for initialization
    void Start () {
        objs.Add(new Objetivo("salir", 10));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void goDown()
    {
        Vector2 v = new Vector2(0, 1);
        moveDestination = new Vector3((currentPosition.x) - Mathf.Floor(GameManager.instance.mapSizex / 2), 1.5f, -(currentPosition.y + 1) + Mathf.Floor(GameManager.instance.mapSizex / 2));
        previousPosition = currentPosition;
        currentPosition = currentPosition + v;
        GameManager.instance.map[(int)previousPosition.x][(int)previousPosition.y].mpl = false;
        GameManager.instance.map[(int)currentPosition.x][(int)currentPosition.y].mpl = true;
    }

    public override void goUp()
    {
        Vector2 v = new Vector2(0, -1);
        moveDestination = new Vector3((currentPosition.x) - Mathf.Floor(GameManager.instance.mapSizex / 2), 1.5f, -(currentPosition.y - 1) + Mathf.Floor(GameManager.instance.mapSizex / 2));
        previousPosition = currentPosition;
        currentPosition = currentPosition + v;
        GameManager.instance.map[(int)previousPosition.x][(int)previousPosition.y].mpl = false;
        GameManager.instance.map[(int)currentPosition.x][(int)currentPosition.y].mpl = true;
    }

    public override void goRight()
    {
        Vector2 v = new Vector2(1, 0);
        moveDestination = new Vector3((currentPosition.x + 1) - Mathf.Floor(GameManager.instance.mapSizex / 2), 1.5f, -(currentPosition.y) + Mathf.Floor(GameManager.instance.mapSizex / 2));
        previousPosition = currentPosition;
        currentPosition = currentPosition + v;
        GameManager.instance.map[(int)previousPosition.x][(int)previousPosition.y].mpl = false;
        GameManager.instance.map[(int)currentPosition.x][(int)currentPosition.y].mpl = true;
    }

    public override void goLeft()
    {
        Vector2 v = new Vector2(-1, 0);
        moveDestination = new Vector3((currentPosition.x - 1) - Mathf.Floor(GameManager.instance.mapSizex / 2), 1.5f, -(currentPosition.y) + Mathf.Floor(GameManager.instance.mapSizex / 2));
        previousPosition = currentPosition;
        currentPosition = currentPosition + v;
        GameManager.instance.map[(int)previousPosition.x][(int)previousPosition.y].mpl = false;
        GameManager.instance.map[(int)currentPosition.x][(int)currentPosition.y].mpl = true;
    }

    public override void colorView(int i, int n)
    {
        base.colorView(i, 0);
    }

	public override void getView()
	{

	}

	public override void chooseAction(){
		float horizontal = Input.GetAxis("Horizontal2");
		float vertical = Input.GetAxis("Vertical2");
		//print ("h: " + horizontal + " v: " + vertical);
		if (horizontal > 0)
			goRight ();
		else if (horizontal < 0)
			goLeft ();
		else if (vertical > 0)
			goUp ();
		else if(vertical < 0)
			goDown ();
		

	}

	public void salir(){
		
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

    }
}
