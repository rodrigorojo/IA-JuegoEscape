using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    public class Objetivo
    {
        public string name;
        public int priority;
        public Objetivo(string n, int p)
        {
            name = n;
            priority = p;
        }

        public void changePriority(int c)
        {
            priority += c;
        }
    }

    public List<Objetivo> objs = new List<Objetivo>();

    public Vector2 currentPosition = Vector2.zero;
    public Vector2 previousPosition = Vector2.zero;
	public Vector3 moveDestination;
	public float moveSpeed = 10.0f;
	
	void Awake () {
		moveDestination = transform.position;
	}

    public string maxPr()
    {
        string res = " ";
        int max = 0;
        for (int i = 0; i < objs.Count; i++)
        {
            if (objs[i].priority > max)
            {
                max = objs[i].priority;
                res = objs[i].name;
            }
        }
        return res;
    }


    public virtual void goDown()
    {
        

    }

    public virtual void goUp()
    {
        
    }

    public virtual void goRight()
    {
        
    }

    public virtual void goLeft()
    {
        
    }

    public virtual void colorView(int i, int n)
    {
        int cx1 = (int)currentPosition.x;
        int cx2 = (int)currentPosition.x;
        int cy1 = (int)currentPosition.y;
        int cy2 = (int)currentPosition.y;

        if(cx1 > 0 && cy1 > 0)
        {
            GameManager.instance.map[cx1 - 1][cy1 - 1].ColorOnView(n);
            GameManager.instance.map[cx1 - 1][cy1 - 1].lci = i;
        }

        if(cx1 > 0 && cy1 < GameManager.instance.mapSizey-1)
        {
            GameManager.instance.map[cx1 - 1][cy1 + 1].ColorOnView(n);
            GameManager.instance.map[cx1 - 1][cy1 + 1].lci = i;
        }

        if (cx1 < GameManager.instance.mapSizex-1 && cy1 > 0)
        {
            GameManager.instance.map[cx1 + 1][cy1 - 1].ColorOnView(n);
            GameManager.instance.map[cx1 + 1][cy1 - 1].lci = i;
        }

        if (cx1 < GameManager.instance.mapSizex - 1 && cy1 < GameManager.instance.mapSizey - 1)
        {
            GameManager.instance.map[cx1 + 1][cy1 + 1].ColorOnView(n);
            GameManager.instance.map[cx1 + 1][cy1 + 1].lci = i;
        }

        while (cx1 >= 0 && !GameManager.instance.map[cx1][(int)currentPosition.y].wall)
        {
            GameManager.instance.map[cx1][(int)currentPosition.y].ColorOnView(n);
            GameManager.instance.map[cx1][(int)currentPosition.y].lci = i;
            cx1--;
        }

        while (cx2 < GameManager.instance.mapSizex && !GameManager.instance.map[cx2][(int)currentPosition.y].wall)
        {
            GameManager.instance.map[cx2][(int)currentPosition.y].ColorOnView(n);
            GameManager.instance.map[cx2][(int)currentPosition.y].lci = i;
            cx2++;
        }

        while (cy1 >= 0 && !GameManager.instance.map[(int)currentPosition.x][cy1].wall)
        {
            GameManager.instance.map[(int)currentPosition.x][cy1].ColorOnView(n);
            GameManager.instance.map[(int)currentPosition.x][cy1].lci = i;
            cy1--;
        }

        while (cy2 < GameManager.instance.mapSizey && !GameManager.instance.map[(int)currentPosition.x][cy2].wall)
        {
            GameManager.instance.map[(int)currentPosition.x][cy2].ColorOnView(n);
            GameManager.instance.map[(int)currentPosition.x][cy2].lci = i;
            cy2++;
        }
    }

    public void cleanView(int i)
    {
        int cx1 = (int)previousPosition.x;
        int cx2 = (int)previousPosition.x;
        int cy1 = (int)previousPosition.y;
        int cy2 = (int)previousPosition.y;

        if (cx1 > 0 && cy1 > 0)
        {
            if(GameManager.instance.map[cx1 - 1][cy1 - 1].lci == i)
                GameManager.instance.map[cx1 - 1][cy1 - 1].ColorOnView(2);
        }

        if (cx1 > 0 && cy1 < GameManager.instance.mapSizey - 1)
        {
            if(GameManager.instance.map[cx1 - 1][cy1 + 1].lci == i)
                GameManager.instance.map[cx1 - 1][cy1 + 1].ColorOnView(2);
        }

        if (cx1 < GameManager.instance.mapSizex - 1 && cy1 > 0)
        {
            if (GameManager.instance.map[cx1 + 1][cy1 - 1].lci == i)
                GameManager.instance.map[cx1 + 1][cy1 - 1].ColorOnView(2);
        }

        if (cx1 < GameManager.instance.mapSizex - 1 && cy1 < GameManager.instance.mapSizey - 1)
        {
            if(GameManager.instance.map[cx1 + 1][cy1 + 1].lci == i)
                GameManager.instance.map[cx1 + 1][cy1 + 1].ColorOnView(2);
        }

        while (cx1 >= 0 && !GameManager.instance.map[cx1][(int)previousPosition.y].wall)
        {
            if(GameManager.instance.map[cx1][(int)previousPosition.y].lci == i)
                GameManager.instance.map[cx1][(int)previousPosition.y].ColorOnView(2);
            cx1--;
        }

        while (cx2 < GameManager.instance.mapSizex && !GameManager.instance.map[cx2][(int)previousPosition.y].wall)
        {
            if(GameManager.instance.map[cx2][(int)previousPosition.y].lci == i)
                GameManager.instance.map[cx2][(int)previousPosition.y].ColorOnView(2);
            cx2++;
        }

        while (cy1 >= 0 && !GameManager.instance.map[(int)previousPosition.x][cy1].wall)
        {
            if(GameManager.instance.map[(int)previousPosition.x][cy1].lci == i)
                GameManager.instance.map[(int)previousPosition.x][cy1].ColorOnView(2);
            cy1--;
        }

        while (cy2 < GameManager.instance.mapSizey && !GameManager.instance.map[(int)previousPosition.x][cy2].wall)
        {
            if(GameManager.instance.map[(int)previousPosition.x][cy2].lci == i)
                GameManager.instance.map[(int)previousPosition.x][cy2].ColorOnView(2);
            cy2++;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public virtual void TurnUpdate () {
		
	}
}
