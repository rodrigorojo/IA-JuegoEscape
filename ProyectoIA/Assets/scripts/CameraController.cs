using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private GameManager instance;

	// Use this for initialization
	void Start () {
        instance = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        if (instance.mapSizex == instance.mapSizey)
        {
            transform.position = new Vector3(0 ,instance.mapSizex, 0);
        }
        else if (instance.mapSizex > instance.mapSizey)
        {
            int cz = (int)Mathf.Floor(instance.mapSizex / instance.mapSizey);
            transform.position = new Vector3(0, instance.mapSizex, cz * 5);
        }
        else
        {
            int cz = (int)Mathf.Floor(instance.mapSizey / instance.mapSizex);
            transform.position = new Vector3(0, instance.mapSizey, cz * -5);
        }
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveDepth = Input.GetAxis("Mouse ScrollWheel");

        Vector3 movement = new Vector3(moveHorizontal, moveDepth, moveVertical);

        transform.position = transform.position + (movement *0.5f);
    }
}
