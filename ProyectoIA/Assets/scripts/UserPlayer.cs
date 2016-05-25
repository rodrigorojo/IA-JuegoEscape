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
        transform.GetComponent<Renderer>().material.color = Color.blue;
    }

    public override void goUp()
    {
        Vector2 v = new Vector2(0, -1);
        moveDestination = new Vector3((currentPosition.x) - Mathf.Floor(GameManager.instance.mapSizex / 2), 1.5f, -(currentPosition.y - 1) + Mathf.Floor(GameManager.instance.mapSizex / 2));
        previousPosition = currentPosition;
        currentPosition = currentPosition + v;
        GameManager.instance.map[(int)previousPosition.x][(int)previousPosition.y].mpl = false;
        GameManager.instance.map[(int)currentPosition.x][(int)currentPosition.y].mpl = true;
        transform.GetComponent<Renderer>().material.color = Color.blue;
    }

    public override void goRight()
    {
        Vector2 v = new Vector2(1, 0);
        moveDestination = new Vector3((currentPosition.x + 1) - Mathf.Floor(GameManager.instance.mapSizex / 2), 1.5f, -(currentPosition.y) + Mathf.Floor(GameManager.instance.mapSizex / 2));
        previousPosition = currentPosition;
        currentPosition = currentPosition + v;
        GameManager.instance.map[(int)previousPosition.x][(int)previousPosition.y].mpl = false;
        GameManager.instance.map[(int)currentPosition.x][(int)currentPosition.y].mpl = true;
        transform.GetComponent<Renderer>().material.color = Color.blue;
    }

    public override void goLeft()
    {
        Vector2 v = new Vector2(-1, 0);
        moveDestination = new Vector3((currentPosition.x - 1) - Mathf.Floor(GameManager.instance.mapSizex / 2), 1.5f, -(currentPosition.y) + Mathf.Floor(GameManager.instance.mapSizex / 2));
        previousPosition = currentPosition;
        currentPosition = currentPosition + v;
        GameManager.instance.map[(int)previousPosition.x][(int)previousPosition.y].mpl = false;
        GameManager.instance.map[(int)currentPosition.x][(int)currentPosition.y].mpl = true;
        transform.GetComponent<Renderer>().material.color = Color.blue;
    }

    public override void colorView(int i, int n)
    {
        //base.colorView(i, 0);
    }

	public override void getView()
	{
        transform.GetComponent<Renderer>().material.color = Color.green;
        int cx1 = (int)currentPosition.x;
		int cx2 = (int)currentPosition.x;
		int cy1 = (int)currentPosition.y;
		int cy2 = (int)currentPosition.y;

		distUp =-1;
		distDown = -1;
		distLeft = -1;
		distRight = -1;

		while (cx1 >= 0 && !GameManager.instance.map[cx1][(int)currentPosition.y].wall)
		{
			cx1--;
			distLeft++;

		}

		while (cx2 < GameManager.instance.mapSizex && !GameManager.instance.map[cx2][(int)currentPosition.y].wall)
		{
			cx2++;
			distRight++;

		}

		while (cy1 >= 0 && !GameManager.instance.map[(int)currentPosition.x][cy1].wall)
		{
			cy1--;
			distUp++;

		}

		while (cy2 < GameManager.instance.mapSizey && !GameManager.instance.map[(int)currentPosition.x][cy2].wall)
		{
			cy2++;
			distDown++;

		}
	}

	public override void chooseAction(){

		float horizontal = Input.GetAxis("Horizontal2");
		float vertical = Input.GetAxis("Vertical2");
		//print ("h: " + horizontal + " v: " + vertical);
		if (horizontal > 0  && distRight > 0)
			goRight ();
		else if (horizontal < 0 && distLeft > 0)
			goLeft ();
		else if (vertical > 0 && distUp > 0)
			goUp ();
		else if(vertical < 0 && distDown  > 0)
			goDown ();
    }

    public override void checkStatus () {
      if (GameManager.instance.map[(int)currentPosition.x][(int)currentPosition.y].epl)
      {
        GameManager.instance.lose = true;
      }
      if (GameManager.instance.map[(int)currentPosition.x][(int)currentPosition.y].salida)
      {
        GameManager.instance.win = true;
      }
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
