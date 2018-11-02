using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    public GameObject player;

    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;

    public Vector3 maxCameraPosition;
    public Vector3 minCameraPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);

        //bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPosition.x, maxCameraPosition.x),
                                        Mathf.Clamp(transform.position.y, minCameraPosition.y, maxCameraPosition.y),
                                        Mathf.Clamp(transform.position.z, minCameraPosition.z, maxCameraPosition.z));
		
	}
}
