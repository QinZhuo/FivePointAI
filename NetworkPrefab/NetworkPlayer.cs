using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {
    [SyncVar]
    public Chess chessColor;
    public NetworkChessBoard chessboard;
    RaycastHit hit;
    
    

    // Use this for initialization
    void Start()
    {
        chessboard = GameObject.Find("棋盘").GetComponent<NetworkChessBoard>();
        if (isLocalPlayer)
        {
            CmdSetColor();
            GameObject.Find("Ip地址").GetComponent<Text>().text ="本机ip：" +Network.player.ipAddress;
        }
        //timer = float.MaxValue;
    }

    [Command]
    void CmdSetColor()
    {
        chessboard.playerNumber++;
        if (chessboard.playerNumber == 1)
        {
            chessColor = Chess.黑棋;
        }
        else if(chessboard.playerNumber == 2)
        {
            chessColor = Chess.白棋;
        }
        else
        {
            chessColor = Chess.观战;
        }
    }
    public void PlayChess()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                CmdPlayChess(new int[2] { (int)(hit.point.x + 7.5f), (int)(hit.point.y + 7.5f) });   
            }
        }
    }
    [Command]
    public void CmdPlayChess(int[] pos)
    {
        if (chessboard.PlayChess(pos)) chessboard.timer = 0; ;

    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (chessColor == chessboard.trun &&isLocalPlayer&&chessboard.playerNumber>1)
        {
            PlayChess();

        }


    }
}
