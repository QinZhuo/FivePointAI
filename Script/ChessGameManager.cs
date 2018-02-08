using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChessGameManager : MonoBehaviour {
    public Player[] players;
	// Use this for initialization
    void Awake()
    {
        if (players.Length > 0)
        {
            int player1 = PlayerPrefs.GetInt("Player1"), player2 = PlayerPrefs.GetInt("Player2");
            for (int i = 0; i < players.Length; i++)
            {
                if(i== player1)
                {
                    players[i].chessColor = Chess.黑棋;
                }
                else if(i==player2)
                {
                    players[i].chessColor = Chess.白棋;
                }
                else
                {
                    players[i].chessColor = Chess.观战;
                }
            }
           
           
           
        }
    }
    public void ChangeColor()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].chessColor == Chess.白棋)
            {
                SetPlayer1(i);
            }
            else if (players[i].chessColor == Chess.黑棋)
            {
                SetPlayer2(i);
            }
            else
            {
                players[i].chessColor = Chess.观战;
            }
        }
        Application.LoadLevel(Application.loadedLevel);
    }
	void Start () {
		
	}
	public void SetPlayer1(int p1)
    {
        PlayerPrefs.SetInt("Player1", p1);
    }
    public void SetPlayer2(int p2)
    {
        PlayerPrefs.SetInt("Player2", p2);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void NetworkStart()
    {
        SceneManager.LoadScene(2);
    }
    public void Return()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
