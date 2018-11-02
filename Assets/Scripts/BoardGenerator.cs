using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hivemind.Utilities;
using System.Linq;

public class BoardGenerator : MonoBehaviour {

    public GameObject breakableWall;
    public GameObject unbreakableWall;
    public GameObject goal;
    public GameObject player;

    public TextAsset jsonFile;

    public int height = 11;
    public int width = 19;
    // Use this for initialization
    void Start () {

        CreateBoard();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateBoard()
    {

        LevelData myLevelData = JsonUtility.FromJson<LevelData>(jsonFile.text);
        //string layer1map = myLevelData.mapdata_layer1;
        //string layer1map = "UUUUUUUUUUUUUUUUUUUUCCCCCCCCCCCCCBBBBUUCBBBBBBBBBBBCBBBBUUCBBBBBBBBBBBCBBBBUUCBBBBBBBBBBBCBBBBUUCBCCCCCCCCCCCCCCCUUCBCBBBBBBBBBBBBBBUUCBCCCCCCCCPBBBBBBUUCBBBBBBBBBBBBBBBBUUCCCCCCCCCCCCCCCCFUUUUUUUUUUUUUUUUUUUU";
        myLevelData.formattedMapData_layer1 = Split(myLevelData.mapdata_layer1, width).ToArray();

        GameObject parent = new GameObject();
        parent.name = "Layer 1 Map";

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                string tyleType = myLevelData.formattedMapData_layer1[y].Substring(x,1);
                Vector3 tilePosition = new Vector3(x, y * -1, 0f);
                GameObject go;
                HiveMindTileBaseScript tileScript;
                PlayerController playerScript;
      
                switch (tyleType)
                {
                    case "C":
                        //Corridor
                        //Do nothing on purpose it is a corridor;
                        break;
                    case "B":
                        //Breakable tile
                        go = Instantiate(breakableWall, tilePosition, Quaternion.Euler(Vector3.zero), parent.transform);
                        tileScript = go.GetComponent<HiveMindTileBaseScript>();
                        tileScript.TileType = tyleType;
                        break;
                    case "F":
                        //Finish
                        go = Instantiate(goal, tilePosition, Quaternion.Euler(Vector3.zero));
                        tileScript = go.GetComponent<HiveMindTileBaseScript>();
                        tileScript.TileType = tyleType;
                        myLevelData.finishLocation = new Vector3(Mathf.Abs(tilePosition.x), Mathf.Abs(tilePosition.y), 0);
                        break;
                    case "P":
                        //Player
                        go = Instantiate(player, tilePosition, Quaternion.Euler(Vector3.zero));
                        playerScript = go.GetComponent<PlayerController>();
                        myLevelData.playerLocation = new Vector3(Mathf.Abs(tilePosition.x), Mathf.Abs(tilePosition.y), 0);
                        playerScript.levelData = myLevelData;
                        break;
                    case "S":
                        //Start
                        //go = Instantiate(breakableWall, tilePosition, Quaternion.Euler(Vector3.zero));
                        break;
                    case "U":
                        //Unbreakable tile
                        go = Instantiate(unbreakableWall, tilePosition, Quaternion.Euler(Vector3.zero), parent.transform);
                        tileScript = go.GetComponent<HiveMindTileBaseScript>();
                        tileScript.TileType = tyleType;
                        break;
                    default:
                        Debug.Log("An unexpected tyleType value was encountered in BoardGenerator.CreateBoard().");
                        break;
                }
               
            }
        }

    }

    static IEnumerable<string> Split(string str, int chunkSize)
    {
        return Enumerable.Range(0, str.Length / chunkSize)
            .Select(i => str.Substring(i * chunkSize, chunkSize));
    }
}
