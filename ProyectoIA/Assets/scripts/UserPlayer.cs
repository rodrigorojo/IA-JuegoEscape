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

    public override void getView()
    {
        int cx1 = (int)currentPosition.x;
        int cx2 = (int)currentPosition.x;
        int cy1 = (int)currentPosition.y;
        int cy2 = (int)currentPosition.y;

        while (cx1 >= 0 && !GameManager.instance.map[cx1][(int)currentPosition.y].wall)
        {
            GameManager.instance.map[cx1][(int)currentPosition.y].ColorOnView(0);
            cx1--;
        }

        while (cx2 < GameManager.instance.mapSizex && !GameManager.instance.map[cx2][(int)currentPosition.y].wall)
        {
            GameManager.instance.map[cx2][(int)currentPosition.y].ColorOnView(0);
            cx2++;
        }

        while (cy1 >= 0 && !GameManager.instance.map[(int)currentPosition.x][cy1].wall)
        {
            GameManager.instance.map[(int)currentPosition.x][cy1].ColorOnView(0);
            cy1--;
        }

        while (cy2 < GameManager.instance.mapSizey && !GameManager.instance.map[(int)currentPosition.x][cy2].wall)
        {
            GameManager.instance.map[(int)currentPosition.x][cy2].ColorOnView(0);
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
