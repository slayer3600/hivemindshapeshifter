using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTileScript : HiveMindTileBaseScript
{

    private GameUIManager gameUIManager;

    // Use this for initialization
    void Start () {
        gameUIManager = GameObject.Find("UIManager").GetComponent<GameUIManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("You reached the goal!");
        gameUIManager.OpenWinPanel();
    }
}

