using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	
	public GameObject TilePrefab;
	public GameObject UserPlayerPrefab;
	public GameObject AIPlayerPrefab;
    public GameObject WallPrefab;
    public GameObject GoalPrefab;
	
	public int mapSizex = 30;
    public int mapSizey = 20;

    private int enem0x;
    private int enem0y;
    private int enem1x;
    private int enem1y;
    private int enem2x;
    private int enem2y;

	
	List <List<Tile>> map = new List<List<Tile>>();
	List <Player> players = new List<Player>();
    List <Wall> walls = new List<Wall>();
	int currentPlayerIndex = 0;
	
	void Awake() {
		instance = this;
	}
	
	// Use this for initialization
	void Start () {		
		generateMap();
		generatePlayers();
        generateWalls();
	}
	
	// Update is called once per frame
	void Update () {
		
		players[currentPlayerIndex].TurnUpdate();
	}
	
	public void nextTurn() {
		if (currentPlayerIndex + 1 < players.Count) {
			currentPlayerIndex++;
		} else {
			currentPlayerIndex = 0;
		}
	}
	
	public void moveCurrentPlayer(Tile destTile) {
		players[currentPlayerIndex].moveDestination = destTile.transform.position + 1.5f * Vector3.up;
	}
	
	void generateMap() {
		map = new List<List<Tile>>();
		for (int i = 0; i < mapSizex; i++) {
			List <Tile> row = new List<Tile>();
			for (int j = 0; j < mapSizey; j++) {
                if (i == (mapSizex - 1) && j == (mapSizey - 1)) {
                    //Debug.Log("exit");
                     Goal exit = ((GameObject)Instantiate(GoalPrefab, new Vector3(i - Mathf.Floor(mapSizex / 2), 0, -j + Mathf.Floor(mapSizex / 2)), Quaternion.Euler(new Vector3()))).GetComponent<Goal>();
                     exit.gridPosition = new Vector2(i, j);
                     row.Add(exit);
                }
                else {
                    Tile tile = ((GameObject)Instantiate(TilePrefab, new Vector3(i - Mathf.Floor(mapSizex / 2), 0, -j + Mathf.Floor(mapSizex / 2)), Quaternion.Euler(new Vector3()))).GetComponent<Tile>();
                    tile.gridPosition = new Vector2(i, j);
                    row.Add(tile);
                }
			}
			map.Add(row);
		}
	}
	
	void generatePlayers() {
		UserPlayer player;
		
		player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3(0 - Mathf.Floor(mapSizex/2),1.5f, -0 + Mathf.Floor(mapSizex/2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
		
		players.Add(player);

        enem0x = (int)Mathf.Round(Random.Range(15.0f, 29.0f));
        enem0y = (int)Mathf.Round(Random.Range(0.0f, 9.0f));

        AIPlayer aiplayer = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(enem0x - Mathf.Floor(mapSizex/2),1.5f, -enem0y + Mathf.Floor(mapSizex/2)), Quaternion.Euler(new Vector3()))).GetComponent<AIPlayer>();
		
		players.Add(aiplayer);

        enem1x = (int)Mathf.Round(Random.Range(0.0f, 14.0f));
        enem1y = (int)Mathf.Round(Random.Range(10.0f, 19.0f));

        AIPlayer aiplayer2 = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(enem1x - Mathf.Floor(mapSizex / 2), 1.5f, -enem1y + Mathf.Floor(mapSizex / 2)), Quaternion.Euler(new Vector3()))).GetComponent<AIPlayer>();

        players.Add(aiplayer2);

        enem2x = (int)Mathf.Round(Random.Range(15.0f, 28.0f));
        enem2y = (int)Mathf.Round(Random.Range(10.0f, 18.0f));

        AIPlayer aiplayer3 = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(enem2x - Mathf.Floor(mapSizex / 2), 1.5f, -enem2y + Mathf.Floor(mapSizex / 2)), Quaternion.Euler(new Vector3()))).GetComponent<AIPlayer>();

        players.Add(aiplayer3);
    }

    void generateWalls() {

        for (int i = 0; i < 60; i++)
        {
            int m = -1;
            int n = -1;
            Wall wall0;
            while(m == -1 || m == enem0x || m == enem1x || m == enem2x) {
                m = (int)Mathf.Round(Random.Range(1.0f, 28.0f));
            }
            while(n == -1 || n == enem0y || n == enem1y || n == enem2y) {
                n = (int)Mathf.Round(Random.Range(1.0f, 18.0f));
            }
            wall0 = ((GameObject)Instantiate(WallPrefab, new Vector3(m - Mathf.Floor(mapSizex / 2), 1.0f, -n + Mathf.Floor(mapSizex / 2)), Quaternion.Euler(new Vector3()))).GetComponent<Wall>();

            walls.Add(wall0);
        }
    }
}
