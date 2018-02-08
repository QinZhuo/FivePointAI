using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Chess chessColor;
    public Chessboard chessboard;
    RaycastHit hit;
   
    // Use this for initialization
    void Start () {
        //timer = float.MaxValue;
    }
	public virtual void PlayChess()
    {   
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit)){
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                 if(chessboard.PlayChess(new int[2] { (int)(hit.point.x+7.5f), (int)(hit.point.y+7.5f) }))
                chessboard.timer = 0;
            }
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        
        if (chessColor == chessboard.trun &&(chessboard.timer >=0.3f))
        {
            PlayChess();
            
        }
        
        
    }
}
