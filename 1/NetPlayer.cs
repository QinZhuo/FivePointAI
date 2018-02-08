using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class NetPlayer : NetworkBehaviour {
    // [SyncVar]
   
    public static int a = 1;
    public GameObject chess1;
    RaycastHit hit;
  
    public NetBoard board;
    [SyncVar]
    public ChessType chess;
    // Use this for initialization
    void Start()
    {
       //
        if (isServer)
        {
            chess = (ChessType)a;
            a++;
          
        }

      board = GameObject.Find("棋盘").GetComponent<NetBoard>();
        GameObject.Find("Button").GetComponent<Button>().onClick.AddListener(ResetChess);
    }

    public void ResetChess()
    {
        CmdResetChess();
    }
    [Command]
     void CmdResetChess()
    {
        board.ReSetChess();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (board.turn != chess) return;
        Play();

    }
 
    public virtual void Play()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (isLocalPlayer)
                    CmdPlay(new int[] { (int)(hit.point.x + 0.5f), (int)(hit.point.y + 0.5f) });
                //  print(hit.point.x + "," + hit.point.y);
            }
        }
    }
    [Command]
    public void CmdPlay(int[] pos)
    {
        board.Play(pos);
    }
}
