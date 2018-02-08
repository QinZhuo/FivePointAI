using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMaxNode
{
    public int chess;
    public int[] pos;
    public List<MiniMaxNode> child;
    public float value;
}
public class MiniMaxAi : Player {
    protected SortedList<string, float> toScore;
    // Use this for initialization

    protected float CheckOneLine(int[,] grid, int[] pos, int[] add, int chess)
    {
        float score=0;
        
        string str = "";
        str += "a";
        int AllNum = 1;
        int LinkNum = 1;
        int li, lj;
        li = add[0]; lj = add[1];
        bool lstop = false, rstop = false;
        int ri = -add[0], rj = -add[1];
        bool lfirst = true;
        while (AllNum < 7 && (!lstop || !rstop))
        {
            if (lfirst)
            {
                if (((pos[0] + li <= 14 && pos[0] + li >= 0) && (pos[1] + lj <= 14 && pos[1] + lj >= 0)) && !lstop)
                {
                    //if ((li == 5 * add[0] && lj == 5 * add[1]))
                    //{
                    //    if(!rstop) lfirst = false; lstop = true;
                    //}
                    
                    if (chess == grid[pos[0] + li, pos[1] + lj])
                    {
                        str = "a" + str;
                        // if (!lstop) lfirst = true;
                        LinkNum++;
                        AllNum++;
                    }
                    else if (0 == grid[pos[0] + li, pos[1] + lj])
                    {

                        str = "_" + str;
                        if (!rstop) lfirst = false;

                        AllNum++;
                    }
                    else
                    {
                        if (!rstop) lfirst = false;
                        lstop = true;
                    }
                    li += add[0]; lj += add[1];
                }
                else
                {
                    if (!rstop) lfirst = false;
                    lstop = true;
                }
            }
            else
            {
                if (((pos[0] + ri <= 14 && pos[0] + ri >= 0) && (pos[1] + rj <= 14 && pos[1] + rj >= 0)) && !rstop && !lfirst)
                {
                    //if ((ri == -5 * add[0] && rj == -5 * add[1]))
                    //{
                    //    rstop = true; if (!lstop) lfirst =true;
                    //}

                    if (chess == grid[pos[0] + ri, pos[1] + rj])
                    {
                        // if (!rstop) lfirst = false;
                        str += "a";
                        LinkNum++;
                        AllNum++;
                    }
                    else if (0 == grid[pos[0] + ri, pos[1] + rj])
                    {
                        if (!lstop) lfirst = true;
                        str += "_";
                        AllNum++;
                    }
                    else
                    {
                        if (!lstop) lfirst = true;
                        rstop = true;
                    }
                    ri -= add[0]; rj -= add[1];
                }
                else
                {
                    if (!lstop) lfirst = true;
                    rstop = true;
                }
            }
        }

        string cmpTrue = "";
        foreach (var keyInfo in toScore)
        {
            if (str.Contains(keyInfo.Key))
            {
                if (cmpTrue != "")
                {
                    if (toScore[keyInfo.Key] > toScore[cmpTrue])
                    {
                        cmpTrue = keyInfo.Key;
                        if (LinkNum >= 4)
                        {

                        }
                    }
                }
                else
                {
                    cmpTrue = keyInfo.Key;
                }

            }
        }
        if (cmpTrue != "")
        {
            score += toScore[cmpTrue];

        }
        return score;

    }

    float GetScore(int[,] grid, int[] pos,int chess)
    {
        float score = 0;
        score += CheckOneLine(grid,pos, new int[2] { 0, 1 }, 1);
        score += CheckOneLine(grid,pos, new int[2] { 1, 0 }, 1);
        score += CheckOneLine(grid,pos, new int[2] { 1, 1 }, 1);
        score += CheckOneLine(grid,pos, new int[2] { -1, 1 }, 1);
        score += CheckOneLine(grid, pos, new int[2] { 0, 1 }, 2);
        score += CheckOneLine(grid, pos, new int[2] { 1, 0 }, 2);
        score += CheckOneLine(grid, pos, new int[2] { 1, 1 }, 2);
        score += CheckOneLine(grid, pos, new int[2] { -1, 1 }, 2);
        return score;
      
    }

    public override void PlayChess()
    {
        if (chessboard.step.Count == 0) { chessboard.PlayChess(new int[2] { 7, 7 });return; }
        //if (Input.GetKey(KeyCode.Space))
        {
            MiniMaxNode node = null;
            foreach (var item in GetList(chessboard.grid, (int)chessColor, true))
            {
               
                GreatTree(item, (int[,])chessboard.grid.Clone(),3, false);
                float a = float.MinValue, b = float.MaxValue;
                //tempitem.value = item.value;
                item.value += AlphaBeta(item,3, false, a, b);
                //item.value += Minimax(item,3, false);
                if (node != null )
                {
                    if(node.value < item.value)
                    node = item;
                }
                else
                {
                    node = item;
                }
                
               // Debug.Log(item.value + ":" + item.pos[0] + "," + item.pos[1]);
                
                
               // Debug.Log("==========="+ tempitem.value + ":" + item.pos[0] + "," + item.pos[1]);
            }
            chessboard.PlayChess(node.pos);
           // Debug.Log("____________________________");
        }
    }

    public void GreatTree(MiniMaxNode node, int[,] grid, int depth, bool myself)
    {
        if (depth == 0 || node.value == float.MaxValue)
        {
            return;
        }
        grid[node.pos[0], node.pos[1]] = node.chess;
        node.child = GetList(grid, node.chess, !myself);
        foreach (var child in node.child)
        {
            GreatTree(child, (int[,])grid.Clone(), depth - 1, !myself);
        }
        
    }



    List<MiniMaxNode> GetList(int[,] grid,int chess,bool myself)
    {
        List<MiniMaxNode> nodeList = new List<MiniMaxNode>();
        
        MiniMaxNode node;
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                int[] pos = new int[2] { i, j };
                if (grid[pos[0], pos[1]] != 0) continue;
                
                node = new MiniMaxNode();
                node.pos = pos;
                node.chess = chess;
                if (myself)
                    if(chess==1)
                    node.value = GetScore(grid, pos, 1);
                    else
                    {
                        node.value = GetScore(grid, pos, 2);
                    }
                else
                {
                    if(chess==1)
                    node.value = -GetScore(grid, pos, 2);
                    {
                        node.value = GetScore(grid, pos, 1);
                    }
                }


                if (nodeList.Count <4)
                {
                    nodeList.Add(node);
                }
                else
                {
                    foreach (var item in nodeList)
                    {
                        if (myself)
                        {
                            if (node.value > item.value)
                            {
                                nodeList.Remove(item);
                                nodeList.Add(node);
                                break;
                            }
                        }
                        else
                        {
                            if (node.value < item.value)
                            {
                                nodeList.Remove(item);
                                nodeList.Add(node);
                                break;
                            }
                        }
                        
                    }
                }
            }
        }
        
        return nodeList;
    }
    void Start () {
        if (chessColor != Chess.观战) Debug.Log(chessColor + "AI: LV Alpha-Beta");
        toScore = new SortedList<string, float>();

        toScore.Add("__a__", 10);



        toScore.Add("aa___", 100);                      //眠二
        toScore.Add("a_a__", 100);
        toScore.Add("___aa", 100);
        toScore.Add("__a_a", 100);
        toScore.Add("a__a_", 100);
        toScore.Add("_a__a", 100);
        toScore.Add("a___a", 100);



        toScore.Add("__aa__", 500);                     //活二
        toScore.Add("_a_a_", 500);
        toScore.Add("_a__a_", 500);

        toScore.Add("_aa__", 500);
        toScore.Add("__aa_", 500);




        toScore.Add("a_a_a", 1000);
        toScore.Add("aa__a", 1000);
        toScore.Add("_aa_a", 1000);
        toScore.Add("a_aa_", 1000);
        toScore.Add("_a_aa", 1000);
        toScore.Add("aa_a_", 1000);
        toScore.Add("aaa__", 1000);                      //眠三

        toScore.Add("_aa_a_", 8000);                     //跳活三
        toScore.Add("_a_aa_", 8000);

        toScore.Add("_aaa_", 10000);                    //活三



        toScore.Add("a_aaa", 15000);                     //冲四
        toScore.Add("aaa_a", 15000);                     //冲四
        toScore.Add("_aaaa", 15000);                    //冲四
        toScore.Add("aaaa_", 15000);                    //冲四
        toScore.Add("aa_aa", 15000);                    //冲四



        toScore.Add("_aaaa_", 1000000);                 //活四
        toScore.Add("aaaaa", float.MaxValue);             //连五
        
        
    }

    float AlphaBeta(MiniMaxNode node, int depth, bool myself, float alpha,  float beta)
    {
      //  float value, best;
        if (node.value == float.MaxValue || node.value == float.MinValue || depth == 0)
        {
            return node.value;
        }
        if (!myself)
        {
           // best = float.MaxValue;
            foreach (var child in node.child)
            {
                beta =Mathf.Min(beta, AlphaBeta(child, depth - 1, !myself, alpha,  beta));
                if (beta <= alpha)
                {
                    return beta;
                }
                
            }
            return beta;
        }
        else
        {
           // best = float.MinValue;
            foreach (var child in node.child)
            {
                alpha =Mathf.Max(alpha,  AlphaBeta(child, depth - 1, !myself,  alpha,  beta));

                if (alpha >= beta)
                {
                    return alpha;
                }
            }
            return alpha;
        }
       
    }


    float Minimax(MiniMaxNode node, int depth, bool myself)
    {
        float beta = float.MaxValue,alpha=float.MinValue;

        // float value, best;
        if (node.value == float.MaxValue || depth == 0)
        {
            return node.value;
        }
        if (!myself)
        {
            
            foreach (var child in node.child)
            {
                beta = Mathf.Min(beta, Minimax(child, depth - 1, !myself));
                

            }
            return beta;
        }
        else
        {
            foreach (var child in node.child)
            {
                alpha =Mathf.Max(alpha, Minimax(child, depth - 1, !myself));

             

            }
            return alpha;
        }
       
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
        {
           
            
        }
	}
}
