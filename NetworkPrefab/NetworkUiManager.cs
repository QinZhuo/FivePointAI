using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class NetworkUiManager : NetworkBehaviour {
    public Text text;
	// Use this for initialization
	void Start () {
		
	}
	public void GameEnd(Chess color)
    {
        text.text = color.ToString() + "胜！";
    }
    
	// Update is called once per frame
	void Update () {
		
	}
}
