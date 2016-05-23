using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPlayer : Player {


    // Use this for initialization
    void Start () {
        objs.Add(new Objetivo("buscar", 10));
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

    public override void getView()
    {
        int cx1 = (int)previousPosition.x;
        int cx2 = (int)previousPosition.x;
        int cy1 = (int)previousPosition.y;
        int cy2 = (int)previousPosition.y;

        if (cx1 > 0 && cy1 > 0)
        {
            if (GameManager.instance.map[cx1 - 1][cy1 - 1].mpl)
                objs.Add(new Objetivo("pesi", 20));
        }

        if (cx1 > 0 && cy1 < GameManager.instance.mapSizey - 1)
        {
            if (GameManager.instance.map[cx1 - 1][cy1 + 1].mpl)
                objs.Add(new Objetivo("peii", 20));
        }

        if (cx1 < GameManager.instance.mapSizex - 1 && cy1 > 0)
        {
            if (GameManager.instance.map[cx1 + 1][cy1 - 1].mpl)
                objs.Add(new Objetivo("pesd", 20));
        }

        if (cx1 < GameManager.instance.mapSizex - 1 && cy1 < GameManager.instance.mapSizey - 1)
        {
            if (GameManager.instance.map[cx1 + 1][cy1 + 1].mpl)
                objs.Add(new Objetivo("peid", 20));
        }

        while (cx1 >= 0 && !GameManager.instance.map[cx1][(int)previousPosition.y].wall)
        {
            if (GameManager.instance.map[cx1][(int)previousPosition.y].mpl)
                objs.Add(new Objetivo("pi", 20));
            cx1--;
        }

        while (cx2 < GameManager.instance.mapSizex && !GameManager.instance.map[cx2][(int)previousPosition.y].wall)
        {
            if (GameManager.instance.map[cx2][(int)previousPosition.y].mpl)
                objs.Add(new Objetivo("pd", 20));
            cx2++;
        }

        while (cy1 >= 0 && !GameManager.instance.map[(int)previousPosition.x][cy1].wall)
        {
            if (GameManager.instance.map[(int)previousPosition.x][cy1].mpl)
                objs.Add(new Objetivo("pa", 20));
            cy1--;
        }

        while (cy2 < GameManager.instance.mapSizey && !GameManager.instance.map[(int)previousPosition.x][cy2].wall)
        {
            if (GameManager.instance.map[(int)previousPosition.x][cy2].mpl)
                objs.Add(new Objetivo("ps", 20));
            cy2++;
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
