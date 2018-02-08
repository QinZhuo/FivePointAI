using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class GameStart : NetworkBehaviour {
    public GameObject chess;
    public static GameObject chessboard;
	// Use this for initialization
	void Awake () {
        if (isServer)
        {
            GameObject temp = Instantiate(chess, new Vector3(7, 7, 0), Quaternion.identity);
            
            NetworkServer.Spawn(temp);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
