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

	
	public List <List<Tile>> map = new List<List<Tile>>();
	public List <Player> players = new List<Player>();
    public List <Wall> walls = new List<Wall>();
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
        Debug.Log("turn: " + currentPlayerIndex);
        players[currentPlayerIndex].TurnUpdate();
	}
	
	public void nextTurn() {
		if (currentPlayerIndex + 1 < players.Count) {
			currentPlayerIndex++;
		} else {
			currentPlayerIndex = 0;
		}
	}
	
	public void moveCurrentPlayer() {
        //players[currentPlayerIndex].getView();
        players[currentPlayerIndex].goRight();
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

        UserPlayer player = ((GameObject)Instantiate(UserPlayerPrefab, new Vector3(0 - Mathf.Floor(mapSizex/2),1.5f, -0 + Mathf.Floor(mapSizex/2)), Quaternion.Euler(new Vector3()))).GetComponent<UserPlayer>();
        map[0][0].mpl = true;
        player.currentPosition = new Vector2(0, 0);
		players.Add(player);

        enem0x = (int)Mathf.Round(Random.Range(mapSizex/2, mapSizex-1));
        enem0y = (int)Mathf.Round(Random.Range(0.0f, (mapSizey/2)-1));
        AIPlayer aiplayer = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(enem0x - Mathf.Floor(mapSizex/2),1.5f, -enem0y + Mathf.Floor(mapSizex/2)), Quaternion.Euler(new Vector3()))).GetComponent<AIPlayer>();
        map[enem0x][enem0y].epl = true;
        aiplayer.currentPosition = new Vector2(enem0x, enem0y);
		players.Add(aiplayer);

        enem1x = (int)Mathf.Round(Random.Range(0.0f, (mapSizex/2)-1));
        enem1y = (int)Mathf.Round(Random.Range(mapSizey/2, mapSizey-1));
        AIPlayer aiplayer2 = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(enem1x - Mathf.Floor(mapSizex / 2), 1.5f, -enem1y + Mathf.Floor(mapSizex / 2)), Quaternion.Euler(new Vector3()))).GetComponent<AIPlayer>();
        map[enem1x][enem1y].epl = true;
        aiplayer2.currentPosition = new Vector2(enem1x, enem1y);
        players.Add(aiplayer2);

        enem2x = (int)Mathf.Round(Random.Range(mapSizex/2, mapSizex-2));
        enem2y = (int)Mathf.Round(Random.Range(mapSizey/2, mapSizey-2));
        AIPlayer aiplayer3 = ((GameObject)Instantiate(AIPlayerPrefab, new Vector3(enem2x - Mathf.Floor(mapSizex / 2), 1.5f, -enem2y + Mathf.Floor(mapSizex / 2)), Quaternion.Euler(new Vector3()))).GetComponent<AIPlayer>();
        map[enem2x][enem2y].epl = true;
        aiplayer3.currentPosition = new Vector2(enem2x, enem2y);
        players.Add(aiplayer3);
    }

    void generateWalls() {
        int limit = (int)Mathf.Round((mapSizex * mapSizey) / 10);

        for (int i = 0; i < limit; i++)
        {
            int m = -1;
            int n = -1;
            Wall wall0;
            while(m == -1 || m == enem0x || m == enem1x || m == enem2x) {
                m = (int)Mathf.Round(Random.Range(1.0f, mapSizex - 2));
            }
            while(n == -1 || n == enem0y || n == enem1y || n == enem2y) {
                n = (int)Mathf.Round(Random.Range(1.0f, mapSizey - 2));
            }
            wall0 = ((GameObject)Instantiate(WallPrefab, new Vector3(m - Mathf.Floor(mapSizex / 2), 1.0f, -n + Mathf.Floor(mapSizex / 2)), Quaternion.Euler(new Vector3()))).GetComponent<Wall>();
            wall0.wallPosition = new Vector2(m, n);
            map[m][n].wall = true;
            walls.Add(wall0);
        }
    }
}
