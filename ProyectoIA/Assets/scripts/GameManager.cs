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
	
	public int mapSizex;
    public int mapSizey;
	
	public List <List<Tile>> map = new List<List<Tile>>();
	public List <Player> players = new List<Player>();
    public List <Wall> walls = new List<Wall>();
	int currentPlayerIndex = 0;

	public int numEnem = 5; 
    public int numJug = 1;

	void Awake() {
		instance = this;
	}
	
	// Use this for initialization
	void Start () {		
		generateMap();
		generateWalls();
		generatePlayers();
       
	}

	// Update is called once per frame
	void Update () {
        // Debug.Log("turn: " + currentPlayerIndex);
        players [currentPlayerIndex].TurnUpdate ();
    }
	
	public void nextTurn() {
		if (currentPlayerIndex + 1 < players.Count) {
			currentPlayerIndex++;
		} else {
			currentPlayerIndex = 0;
		}
	}
	
	public void moveCurrentPlayer() {
		players [currentPlayerIndex].getView ();
		players[currentPlayerIndex].chooseAction();
        players[currentPlayerIndex].cleanView(currentPlayerIndex);
        players[currentPlayerIndex].colorView(currentPlayerIndex,0);
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

        for (int i = 0; i < numJug; i++)
        {
            UserPlayer player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3(0 - Mathf.Floor(mapSizex / 2), 1.5f, -0 + Mathf.Floor(mapSizex / 2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
            map[0][0].mpl = true;
            player.currentPosition = new Vector2(0, 0);
            players.Add(player);
        }

		for(int i = 0; i<numEnem; i++){
			int enemx = 0;
			int enemy = 0;
			AIPlayer aiplayer;
			while(map[enemx][enemy].wall || (enemx == 0 && enemy == 0) || enemx == -1 || enemy == -1){
				enemx = (int)Mathf.Round(Random.Range(1.0f, mapSizex-2));
				enemy = (int)Mathf.Round(Random.Range(1.0f, mapSizey-2));
			}
			aiplayer = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(enemx - Mathf.Floor(mapSizex/2),1.5f, -enemy + Mathf.Floor(mapSizex/2)), Quaternion.Euler(new Vector3()))).GetComponent<AIPlayer>();
			map[enemx][enemy].epl = true;
			aiplayer.currentPosition = new Vector2(enemx, enemy);
            aiplayer.previousPosition = new Vector2(enemx, enemy);
			players.Add(aiplayer);
			
		}
    }

    void generateWalls() {
        int limit = (int)Mathf.Round((mapSizex * mapSizey) / 10);

        for (int i = 0; i < limit; i++)
        {
            int m = -1;
            int n = -1;
            Wall wall0;
			while(m == -1 || n == -1 || (m==0 && n == 0) || (m == mapSizex-1 && n == mapSizey-1)) {
                m = (int)Mathf.Round(Random.Range(0.0f, mapSizex - 1));
                n = (int)Mathf.Round(Random.Range(0.0f, mapSizey - 1));
            }
            wall0 = ((GameObject)Instantiate(WallPrefab, new Vector3(m - Mathf.Floor(mapSizex / 2), 1.0f, -n + Mathf.Floor(mapSizex / 2)), Quaternion.Euler(new Vector3()))).GetComponent<Wall>();
            wall0.wallPosition = new Vector2(m, n);
            map[m][n].wall = true;
            walls.Add(wall0);
        }
    }
}
