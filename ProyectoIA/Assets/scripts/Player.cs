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

    public virtual void getView()
    {
        
    }

    public virtual char Desicion()
    {
        return ' ';
    }

    public void cleanView()
    {
        int cx1 = (int)previousPosition.x;
        int cx2 = (int)previousPosition.x;
        int cy1 = (int)previousPosition.y;
        int cy2 = (int)previousPosition.y;

        while (cx1 >= 0 && !GameManager.instance.map[cx1][(int)previousPosition.y].wall)
        {
            GameManager.instance.map[cx1][(int)previousPosition.y].ColorOnView(2);
            cx1--;
        }

        while (cx2 < GameManager.instance.mapSizex && !GameManager.instance.map[cx2][(int)previousPosition.y].wall)
        {
            GameManager.instance.map[cx2][(int)previousPosition.y].ColorOnView(2);
            cx2++;
        }

        while (cy1 >= 0 && !GameManager.instance.map[(int)previousPosition.x][cy1].wall)
        {
            GameManager.instance.map[(int)previousPosition.x][cy1].ColorOnView(2);
            cy1--;
        }

        while (cy2 < GameManager.instance.mapSizey && !GameManager.instance.map[(int)previousPosition.x][cy2].wall)
        {
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
