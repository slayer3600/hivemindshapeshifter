using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hivemind.Utilities;

public class PlayerController : MonoBehaviour {

    public float movementAmount = 1f;
    public float speed = 0.1f;
    public LevelData levelData;
    public LayerMask collideWith;

    private Vector3 newPosition;
    private Vector3 newMapPosition;

    // Use this for initialization
    void Start () {

        //newPosition = transform.position;

    }
	
	// Update is called once per frame
	void Update () {

        float x = 0;
        float y = 0;
        bool move = false;
        bool isMovementAllowed = false;
        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            x = movementAmount;
            y = 0;
            move = true;
            direction = Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            x = -movementAmount;
            y = 0;
            move = true;
            direction = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            x = 0;
            y = movementAmount;
            move = true;
            direction = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            x = 0;
            y = -movementAmount;
            move = true;
            direction = Vector3.down;
        }

        if (move)
        {
            
            //int mask = ~(1 << 8);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, movementAmount, collideWith.value);
            if (hit.collider != null)
            {
                
                GameObject go = hit.collider.gameObject;
                HiveMindTileBaseScript tileScript = go.GetComponent<HiveMindTileBaseScript>();

                Debug.Log("Hit: " + tileScript.TileType);

                isMovementAllowed = IsMovementAllowed(tileScript.TileType);

            }
            else
            {
                isMovementAllowed = true;
            }

            if (isMovementAllowed)
            {
                newPosition = new Vector3(transform.position.x + x, transform.position.y + y);
                transform.position = newPosition;
            }
        }
        
    }

    string GetTileTypeByPosition(Vector3 position)
    {

        int x = Mathf.Abs((int)position.x);
        int y = Mathf.Abs((int)position.y);

        //return currentMap[y].Substring(x, 1);
        return levelData.formattedMapData_layer1[y].Substring(x, 1);

    }

    bool IsMovementAllowed(string TileType)
    {

        if (TileType.IndexOfAny("UB".ToCharArray()) != -1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}