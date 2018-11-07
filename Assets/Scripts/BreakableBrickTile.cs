using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBrickTile : HiveMindTileBaseScript{

    private ParticleSystem brokenBricks;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCollider;

	// Use this for initialization
	void Start () {

        brokenBricks = gameObject.GetComponent<ParticleSystem>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Break()
    {

        Debug.Log("Brick broken.");
        sprite.enabled = false;
        boxCollider.enabled = false;
        brokenBricks.Play(true);
        Destroy(gameObject, 1f);

    }
}
