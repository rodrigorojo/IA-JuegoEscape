using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPlayer : Player {


    // Use this for initialization
    void Start () {
        //objs.Add(new Objetivo("buscar", 10));
	}

	// Update is called once per frame
	void Update () {

	}

	public override void goPreviousPosition()
	{
		moveDestination = new Vector3((previousPosition.x) - Mathf.Floor(GameManager.instance.mapSizex / 2), 1.5f, -(previousPosition.y) + Mathf.Floor(GameManager.instance.mapSizex / 2));
		GameManager.instance.map[(int)currentPosition.x][(int)currentPosition.y].epl = false;
		GameManager.instance.map[(int)previousPosition.x][(int)previousPosition.y].epl = true;

		Vector2 tmp;
		tmp = previousPosition;
		previousPosition = currentPosition;
		currentPosition = tmp;

	}

    public override void goDown()
    {
        Vector2 v = new Vector2(0, 1);
        moveDestination = new Vector3((currentPosition.x) - Mathf.Floor(GameManager.instance.mapSizex / 2), 1.5f, -(currentPosition.y + 1) + Mathf.Floor(GameManager.instance.mapSizex / 2));
        previousPosition = currentPosition;
        currentPosition = currentPosition + v;
        GameManager.instance.map[(int)previousPosition.x][(int)previousPosition.y].epl = false;
        GameManager.instance.map[(int)currentPosition.x][(int)currentPosition.y].epl = true;
    }

    public override void goUp()
    {
        Vector2 v = new Vector2(0, -1);
        moveDestination = new Vector3((currentPosition.x) - Mathf.Floor(GameManager.instance.mapSizex / 2), 1.5f, -(currentPosition.y - 1) + Mathf.Floor(GameManager.instance.mapSizex / 2));
        previousPosition = currentPosition;
        currentPosition = currentPosition + v;
        GameManager.instance.map[(int)previousPosition.x][(int)previousPosition.y].epl = false;
        GameManager.instance.map[(int)currentPosition.x][(int)currentPosition.y].epl = true;
    }

    public override void goRight()
    {
        Vector2 v = new Vector2(1, 0);
        moveDestination = new Vector3((currentPosition.x + 1) - Mathf.Floor(GameManager.instance.mapSizex / 2), 1.5f, -(currentPosition.y) + Mathf.Floor(GameManager.instance.mapSizex / 2));
        previousPosition = currentPosition;
        currentPosition = currentPosition + v;
        GameManager.instance.map[(int)previousPosition.x][(int)previousPosition.y].epl = false;
        GameManager.instance.map[(int)currentPosition.x][(int)currentPosition.y].epl = true;
    }

    public override void goLeft()
    {
        Vector2 v = new Vector2(-1, 0);
        moveDestination = new Vector3((currentPosition.x - 1) - Mathf.Floor(GameManager.instance.mapSizex / 2), 1.5f, -(currentPosition.y) + Mathf.Floor(GameManager.instance.mapSizex / 2));
        GameManager.instance.map[(int)currentPosition.x][(int)currentPosition.y].epl = false;
        previousPosition = currentPosition;
        currentPosition = currentPosition + v;
        GameManager.instance.map[(int)previousPosition.x][(int)previousPosition.y].epl = false;
        GameManager.instance.map[(int)currentPosition.x][(int)currentPosition.y].epl = true;
    }

    public override void colorView(int i, int n)
    {
        base.colorView(i, 1);
    }

	int lastDistancePlayer;
	int lastDirectionPlayer;
	public override void getView()
	{
		objs.Clear ();
		objs.Add(new Objetivo("buscar", 10));


		int cx1 = (int)currentPosition.x - 1;
		int cx2 = (int)currentPosition.x + 1;
		int cy1 = (int)currentPosition.y - 1;
		int cy2 = (int)currentPosition.y + 1;

		distUp = 0;
		distDown = 0;
		distLeft = 0;
		distRight = 0;


		if (cx1 > 0 && cy1 > 0)
		{
			if (GameManager.instance.map [cx1] [cy1].mpl)
				objs.Add (new Objetivo("pesi", 20));
		}

		if (cx1 > 0 && cy2 < GameManager.instance.mapSizey - 1)
		{
			if (GameManager.instance.map[cx1][cy2].mpl)
				objs.Add (new Objetivo("peii", 20));
		}

		if (cx2 < GameManager.instance.mapSizex - 1 && cy1 > 0)
		{
			if (GameManager.instance.map[cx2][cy1].mpl)
				objs.Add (new Objetivo("pesd", 20));
		}

		if (cx2 < GameManager.instance.mapSizex - 1 && cy2 < GameManager.instance.mapSizey - 1)
		{
			if (GameManager.instance.map[cx2][cy2].mpl)
				objs.Add (new Objetivo("peid", 20));
		}

		while (cx1 >= 0 && !GameManager.instance.map[cx1][(int)currentPosition.y].wall && !GameManager.instance.map[cx1][(int)currentPosition.y].epl)
		{
			if (GameManager.instance.map [cx1] [(int)currentPosition.y].mpl) {
				objs.Add (new Objetivo ("pi", 15));
				lastDistancePlayer = (int) Mathf.Abs(currentPosition.x-cx1);
				lastDirectionPlayer = 0;
			}
			cx1--;
			distLeft++;

		}
		while (cx2 < GameManager.instance.mapSizex && !GameManager.instance.map[cx2][(int)currentPosition.y].wall && !GameManager.instance.map[cx2][(int)currentPosition.y].epl)
		{
			if (GameManager.instance.map [cx2] [(int)currentPosition.y].mpl) {
				objs.Add (new Objetivo ("pd", 15));
				lastDistancePlayer = (int) Mathf.Abs (currentPosition.x - cx2);
				lastDirectionPlayer = 1;
			}
			cx2++;
			distRight++;

		}

		while (cy1 >= 0 && !GameManager.instance.map[(int)currentPosition.x][cy1].wall && !GameManager.instance.map[(int)currentPosition.x][cy1].epl)
		{
			if (GameManager.instance.map [(int)currentPosition.x] [cy1].mpl) {
				objs.Add (new Objetivo ("pa", 15));
				lastDistancePlayer = (int) Mathf.Abs(currentPosition.y - cy1);
				lastDirectionPlayer = 2;
			}

			cy1--;
			distUp++;

		}

		while (cy2 < GameManager.instance.mapSizey && !GameManager.instance.map[(int)currentPosition.x][cy2].wall && !GameManager.instance.map[(int)currentPosition.x][cy2].epl)
		{
			if (GameManager.instance.map [(int)currentPosition.x] [cy2].mpl) {
				objs.Add (new Objetivo ("ps", 15));
				lastDistancePlayer = (int) Mathf.Abs(currentPosition.y - cy2);
				lastDirectionPlayer = 3;
			}

			cy2++;
			distDown++;

		}
		if (lastDistancePlayer > 0) {
			if(lastDirectionPlayer == 0)
				objs.Add(new Objetivo("pi",12));
			if (lastDirectionPlayer == 1)
				objs.Add(new Objetivo("pd",12));
			if (lastDirectionPlayer == 2)
				objs.Add(new Objetivo("pa",12));
			if (lastDirectionPlayer == 3)
				objs.Add(new Objetivo("ps",12));
		}

		//print ("distu: " + distUp + " distd: " + distDown + " distr: " + distRight + " distl: " + distLeft);

	}

	public override void chooseAction(){
		string obj = maxobj ();
		print (obj);
		switch (obj) {
		case "pesi":
			if (distLeft != 0) {
				goLeft ();
			} else if (distUp != 0) {
				goUp ();
			} else {
				goPreviousPosition ();
			}
			break;
		case "peii":
			if (distDown != 0) {
				goDown ();
			} else if (distLeft != 0) {
				goLeft ();
			} else {
				goPreviousPosition ();

			}
			break;
		case "pesd":
			if (distRight != 0) {
				goRight ();
			} else if (distUp != 0) {
				goUp ();
			} else {
				goPreviousPosition ();
			}
			break;
		case "peid":
			if (distRight != 0) {
				goRight ();
			} else if (distDown != 0) {
				goDown ();
			} else {
				goPreviousPosition ();
			}
			break;
		case "pi":
			if (distLeft != 0) {
				goLeft ();
			} else {
				goPreviousPosition ();
			}
			lastDistancePlayer--;
			break;
		case "pd":
			if (distRight != 0) {
				goRight ();
			} else {
				goPreviousPosition ();
			}
			lastDistancePlayer--;
			break;
		case "pa":
			if (distUp != 0) {
				goUp ();
			} else {
				goPreviousPosition ();
			}
			lastDistancePlayer--;
			break;
		case "ps":
			if (distDown != 0) {
				goDown ();
			} else {
				goPreviousPosition ();
			}
			lastDistancePlayer--;
			break;
		case "buscar":
			buscar ();
			break;
		case "":
			break;
		}
    }

	public void buscar(){
        float r = Random.Range(0.0f, 10.0f);
        if (r < 3)
            goMin();
        else
            goMax();
    }

    public void goMax()
    {
        int max = distMax();
        switch (max)
        {
            case 0:
                if (distUp != 0)
                    goUp();
                break;
            case 1:
                if (distRight != 0)
                    goRight();
                break;
            case 2:
                if (distDown != 0)
                    goDown();
                break;
            case 3:
                if (distLeft != 0)
                    goLeft();
                break;
        }
    }

    public void goMin()
    {
        int min = distMin();
        switch (min)
        {
            case 0:
                goUp();
                break;
            case 1:
                goRight();
                break;
            case 2:
                goDown();
                break;
            case 3:
                goLeft();
                break;
            case 4:
                goPreviousPosition();
                break;
        }
    }

    public int distMax(){
		if (distUp > distDown && distUp > distLeft && distUp > distRight) {
			return 0;
		} else if (distRight > distLeft && distRight > distDown) {
			return 1;
		} else if (distDown > distLeft ) {
			return 2;
		}else{
			return 3;
		}
	}

    public int distMin()
    {
        if (distUp < distDown && distUp < distLeft && distUp < distRight && distUp != 0)
        {
            return 0;
        }
        else if (distRight < distLeft && distRight < distDown && distRight != 0)
        {
            return 1;
        }
        else if (distDown < distLeft && distDown != 0)
        {
            return 2;
        }
        else if (distLeft != 0)
        {
            return 3;
        }
        else
        {
            return 4;
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
