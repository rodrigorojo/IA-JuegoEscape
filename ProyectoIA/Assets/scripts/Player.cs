using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

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

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public virtual void TurnUpdate () {
		
	}
}
