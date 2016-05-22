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
	public Vector3 moveDestination;
	public float moveSpeed = 10.0f;
	
	void Awake () {
		moveDestination = transform.position;
	}

    public void goUp() {
        Vector2 v = new Vector2(0, 1);
        moveDestination = new Vector3((currentPosition.x) - Mathf.Floor(GameManager.instance.mapSizex / 2), 1.5f, -(currentPosition.y + 1) + Mathf.Floor(GameManager.instance.mapSizex / 2));
        currentPosition = currentPosition + v;
    }

    public void goDown()
    {
        Vector2 v = new Vector2(0, -1);
        moveDestination = new Vector3((currentPosition.x) - Mathf.Floor(GameManager.instance.mapSizex / 2), 1.5f, -(currentPosition.y - 1) + Mathf.Floor(GameManager.instance.mapSizex / 2));
        currentPosition = currentPosition + v;
    }

    public void goRight()
    {
        Vector2 v = new Vector2(1, 0);
        moveDestination = new Vector3((currentPosition.x + 1) - Mathf.Floor(GameManager.instance.mapSizex / 2), 1.5f, -(currentPosition.y) + Mathf.Floor(GameManager.instance.mapSizex / 2));
        currentPosition = currentPosition + v;
    }

    public void goLeft()
    {
        Vector2 v = new Vector2(-1, 0);
        moveDestination = new Vector3((currentPosition.x - 1) - Mathf.Floor(GameManager.instance.mapSizex / 2), 1.5f, -(currentPosition.y) + Mathf.Floor(GameManager.instance.mapSizex / 2));
        currentPosition = currentPosition + v;
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
        for (int i = (int)currentPosition.x; i < GameManager.instance.mapSizex; i++)
        {
            GameManager.instance.map[i][(int)currentPosition.y].ClearView();
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
