using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkChessBoard : NetworkBehaviour {

    [SyncVar]
    public int playerNumber = 0;
    public int[,] grid;
    [SyncVar]
    public Chess trun;
    public GameObject[] chessObject;
    public Stack<Transform> step;
  
    public float timer;
    public bool gameStart;

    public AIStudent aiStudent;
    public bool aiRemember;

    public void AiRemember(bool b)
    {
        aiRemember = b;
    }
    // Use this for initialization
    void Start()
    {
        playerNumber = 0;
        trun = Chess.黑棋;
        Time.timeScale = 1;
      
        grid = new int[15, 15];
        step = new Stack<Transform>();
        timer = 0;
        gameStart = true;
    }
    [Server]
    public bool PlayChess(int[] pos)
    {
        if (!gameStart) { return false; }
        pos[0] = Mathf.Clamp(pos[0], 0, 14);
        pos[1] = Mathf.Clamp(pos[1], 0, 14);
        GameObject temp;
        // timer = 0;
        // Debug.Log(pos[0]+","+pos[1]);
        if (grid[pos[0], pos[1]] != 0) return false;
        if (trun == Chess.黑棋)
        {
           // Debug.Log(pos[0] + "," + pos[1]);
            temp = Instantiate(chessObject[0], new Vector3(pos[0] - 7, pos[1] - 7), Quaternion.identity);
            NetworkServer.Spawn(temp);
            step.Push(temp.GetComponent<Transform>());

            grid[pos[0], pos[1]] = 1;
            if (aiRemember && aiStudent)
            {
                aiStudent.SetScoreRemeber(pos, 1);
            }
            if (CheckWiner(new int[2] { pos[0], pos[1] }, 1))
            {

                GameEnd(trun);
            }
            trun = Chess.白棋;
        }
        else if (trun == Chess.白棋)
        {
            temp = Instantiate(chessObject[1], new Vector3(pos[0] - 7, pos[1] - 7), Quaternion.identity);
            NetworkServer.Spawn(temp);
            step.Push(temp.GetComponent<Transform>());
            grid[pos[0], pos[1]] = 2;
            if (aiRemember && aiStudent)
            {
                aiStudent.SetScoreRemeber(pos, 2);
            }
            if (CheckWiner(new int[2] { pos[0], pos[1] }, 2))
            {
                GameEnd(trun);
            }
            trun = Chess.黑棋;
        }
        return true;
    }
   

    public void GameEnd(Chess winer)
    {
        gameStart = false;
        Debug.Log(winer + "胜!");
        if (aiRemember && aiStudent) aiStudent.RemeberWiner((int)winer);
        Time.timeScale = 0;

    }
    public bool CheckWiner(int[] pos, int chess)
    {
        bool temp = false;
        if (CheckOneLine(pos, new int[2] { 1, 1 }, chess))
        {
            return true;
        }
        if (CheckOneLine(pos, new int[2] { 0, 1 }, chess))
        {
            return true;
        }
        if (CheckOneLine(pos, new int[2] { 1, 0 }, chess))
        {
            return true;
        }
        if (CheckOneLine(pos, new int[2] { 1, -1 }, chess))
        {
            return true;
        }
        return temp;
    }
    bool CheckOneLine(int[] pos, int[] add, int chess)
    {
        int linkNum = 1;
        for (int i = add[0], j = add[1]; !((pos[0] + i > 14 || pos[0] + i < 0) || (pos[1] + j > 14 || pos[1] + j < 0)); i += add[0], j += add[1])
        {
            if ((i == 5 * add[0] && j == 5 * add[1])) break;
            if (chess == grid[pos[0] + i, pos[1] + j])
            {

                linkNum++;
            }
            else
            {
                break;
            }
        }
        for (int i = -add[0], j = -add[1]; !((pos[0] + i > 14 || pos[0] + i < 0) || (pos[1] + j > 14 || pos[1] + j < 0)); i -= add[0], j -= add[1])
        {
            if ((i == 5 * add[0] && j == 5 * add[1])) break;
            if (chess == grid[pos[0] + i, pos[1] + j])
            {
                linkNum++;

            }
            else
            {
                break;
            }
        }
        //  Debug.Log(linkNum);
        if (linkNum >= 5)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void Restart()
    {
       
        CmdRegretChess();
    }
    [Command]
    public void CmdRegretChess()
    {
        if (step.Count > 1)
        {
            Transform temp = step.Pop();
            grid[(int)(temp.position.x + 7), (int)(temp.position.y + 7)] = 0;
            Debug.Log("悔棋");
            Destroy(temp.gameObject);
            temp = step.Pop();
            grid[(int)(temp.position.x + 7), (int)(temp.position.y + 7)] = 0;
            Destroy(temp.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
}
