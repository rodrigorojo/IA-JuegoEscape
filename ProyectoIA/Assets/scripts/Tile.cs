using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	public Vector2 gridPosition = Vector2.zero;
    public bool wall;
    public bool mpl;
    public bool epl;
    public int lci;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void ColorOnView(int o)
    {
        if(o == 0)
            transform.GetComponent<Renderer>().material.color = Color.blue;
        else if(o == 1)
            transform.GetComponent<Renderer>().material.color = Color.red;
        else
            transform.GetComponent<Renderer>().material.color = Color.white;
    }
	
	void OnMouseEnter() {
		transform.GetComponent<Renderer>().material.color = Color.blue;
		
		//Debug.Log("my position is (" + gridPosition.x + "," + gridPosition.y);
	}
	
	void OnMouseExit() {
		transform.GetComponent<Renderer>().material.color = Color.white;
	}
	
	
	void OnMouseDown() {
        print("hay jugador: " + mpl);
        print("hay enemigo: " + epl);
		GameManager.instance.moveCurrentPlayer();
	}

	
}
