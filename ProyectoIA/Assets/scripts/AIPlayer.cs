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


		int cx1 = (int)currentPosition.x;
		int cx2 = (int)currentPosition.x;
		int cy1 = (int)currentPosition.y;
		int cy2 = (int)currentPosition.y;

		distUp =-1;
		distDown = -1;
		distLeft = -1;
		distRight = -1;


		if (cx1 > 0 && cy1 > 0)
		{
			if (GameManager.instance.map [cx1 - 1] [cy1 - 1].mpl)
				addObjetivo (new Objetivo("pesi", 20));
			//objs.//Add(new Objetivo("pesi", 20));
		}

		if (cx1 > 0 && cy1 < GameManager.instance.mapSizey - 1)
		{
			if (GameManager.instance.map[cx1 - 1][cy1 + 1].mpl)
				addObjetivo(new Objetivo("peii", 20));
		}

		if (cx1 < GameManager.instance.mapSizex - 1 && cy1 > 0)
		{
			if (GameManager.instance.map[cx1 + 1][cy1 - 1].mpl)
				addObjetivo(new Objetivo("pesd", 20));
		}

		if (cx1 < GameManager.instance.mapSizex - 1 && cy1 < GameManager.instance.mapSizey - 1)
		{
			if (GameManager.instance.map[cx1 + 1][cy1 + 1].mpl)
				addObjetivo(new Objetivo("peid", 20));
		}
		while (cx1 >= 0 && !GameManager.instance.map[cx1][(int)currentPosition.y].wall)
		{
			if (GameManager.instance.map [cx1] [(int)currentPosition.y].mpl) {
				objs.Add (new Objetivo ("pi", 15));
				lastDistancePlayer = (int) Mathf.Abs(currentPosition.x-cx1);
				lastDirectionPlayer = 0;
			}
			cx1--;
			distLeft++;

		}
		while (cx2 < GameManager.instance.mapSizex && !GameManager.instance.map[cx2][(int)currentPosition.y].wall)
		{
			if (GameManager.instance.map [cx2] [(int)currentPosition.y].mpl) {
				objs.Add (new Objetivo ("pd", 15));
				lastDistancePlayer = (int) Mathf.Abs (currentPosition.x - cx2);
				lastDirectionPlayer = 1;
			}
			cx2++;
			distRight++;

		}

		while (cy1 >= 0 && !GameManager.instance.map[(int)currentPosition.x][cy1].wall)
		{
			if (GameManager.instance.map [(int)currentPosition.x] [cy1].mpl) {
				objs.Add (new Objetivo ("pa", 15));
				lastDistancePlayer = (int) Mathf.Abs(currentPosition.y - cy1);
				lastDirectionPlayer = 2;
			}

			cy1--;
			distUp++;

		}

		while (cy2 < GameManager.instance.mapSizey && !GameManager.instance.map[(int)currentPosition.x][cy2].wall)
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

		print ("dist: " + lastDistancePlayer + " direc: " + lastDirectionPlayer);


	}

	public override void chooseAction(){
		string obj = maxobj ();
		print (obj);
		switch (obj) {
		case "pesi":
			if ((distUp == 0 || distUp > distLeft) && distLeft != 0) {
				goLeft ();			
			} else if ((distLeft == 0 || distUp < distLeft) && distUp != 0) {
				goUp ();
			} else {
				goPreviousPosition ();
			}
			break;
		case "peii":
			if ((distDown == 0 || distDown > distLeft)&& distLeft != 0) {
				goLeft ();
			} else if ((distLeft == 0 || distDown < distLeft) && distDown != 0) {
				goDown ();
			} else {
				goPreviousPosition ();

			}
			break;
		case "pesd":
			if ((distUp == 0 || distUp > distRight ) && distRight != 0) {
				goRight ();
			} else if ((distRight == 0 || distUp < distRight) && distDown != 0) {
				goDown ();
			} else {
				goPreviousPosition ();

			}
			break;
		case "peid":
			if ((distDown == 0 || distDown > distRight) && distRight != 0) {
				goRight ();
			} else if ((distRight == 0 || distDown < distRight) && distDown != 0) {
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

		int max = distMax ();
		switch(max){
			case 0:
				if(distUp != 0)
					goUp ();
				break;
			case 1:
				if(distRight != 0)
					goRight ();
				break;
			case 2:
				if(distDown != 0)
					goDown ();
				break;
			case 3:
				if(distLeft != 0)
					goLeft ();
				break;

			/*case 4:				
				goLeft ();
				lastDistancePlayer--;
				break;
			case 5:
				goRight ();
				lastDistancePlayer--;
				break;
			case 6: 
				goUp ();
				lastDistancePlayer--;
				break;
			case 7:
				goDown ();
				lastDistancePlayer--;
				break;*/
		}
	}

	public int distMax(){
		/*if (lastDistancePlayer != 0) {
			if(lastDirectionPlayer == 0)
				return 4;
			if (lastDirectionPlayer == 1)
				return 5;
			if (lastDirectionPlayer == 2)
				return 6;
			if (lastDirectionPlayer == 3)
				return 7;
				
		}*/
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
