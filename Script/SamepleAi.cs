using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamepleAi : Player {
    protected float[,] score=new float[15,15];
	// Use this for initialization
	protected SortedList<string,float> toScore;
    void Start()
    {
        if(chessColor!=Chess.观战) Debug.Log(chessColor+"AI: LV"+1);
        toScore = new SortedList<string, float>();
        toScore.Add("_aa_", 100);
        toScore.Add("aa_", 50);
        toScore.Add("_aa", 50);


        toScore.Add("_aaa_", 1000);
        toScore.Add("aaa_",500);
        toScore.Add("_aaa", 500);


        toScore.Add("_aaaa_", 10000);
        toScore.Add("_aaaa", 1000);
        toScore.Add("aaaa_", 1000);


        toScore.Add("aaaaa", int.MaxValue);
        toScore.Add("aaaaa_", int.MaxValue);
        toScore.Add("_aaaaa_", int.MaxValue);
        toScore.Add("_aaaaa", int.MaxValue);
    }
    public override void  PlayChess()
    {
       
        if (chessboard.step.Count == 0)
        {
            chessboard.PlayChess(new int[2] { 7,7 });return;
        }
        //Debug.Log("多态");
        float max = 0;
        int[] maxPos = new int[2]{ 0,0};
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                
                SetScore(new int[2] { i, j });
                if((score[i,j]>=max) && chessboard.grid[i, j] == 0)
                {
                    maxPos[0] = i;
                    maxPos[1] = j;
                    max = score[i, j];
                }
            }
        }
        if (chessboard.PlayChess(maxPos))
        {
            chessboard.timer = 0;
        }
    }
   
    void SetScore(int[] pos)
    {
        score[pos[0], pos[1]] = 0;
       // score[pos[0], pos[1]] = 0;
        CheckOneLine(pos, new int[2] { 0, 1 }, 1);
        CheckOneLine(pos, new int[2] { 1, 0 }, 1);
        CheckOneLine(pos, new int[2] { 1, 1 }, 1);
        CheckOneLine(pos, new int[2] { -1, 1 }, 1);

        CheckOneLine(pos, new int[2] { 0, 1 }, 2);
        CheckOneLine(pos, new int[2] { 1, 0 }, 2);
        CheckOneLine(pos, new int[2] { 1, 1 }, 2);
        CheckOneLine(pos, new int[2] { -1, 1 }, 2);
    }
    // Update is called once per frame
    protected virtual void  CheckOneLine(int[] pos, int[] add, int chess)
    {
        string str = "";
        str += "a";
        //int linkNum = 1;
        int li, lj;
        


        for (li = add[0], lj = add[1]; !((pos[0] + li > 14 || pos[0] + li < 0) || (pos[1] + lj > 14 || pos[1] + lj < 0)); li += add[0], lj += add[1])
        {
            if ((li == 5 * add[0] && lj == 5 * add[1])) break;
            if (chess == chessboard.grid[pos[0] + li, pos[1] + lj])
            {
                str = "a"+str;
                //linkNum++;
            }
            else if(0 == chessboard.grid[pos[0] + li, pos[1] + lj])
            {
                str = "_" + str;
                break;
            }
            else
            {
                break;
            }
        }
        int ri, rj;
        for (ri = -add[0], rj = -add[1]; !((pos[0] + ri > 14 || pos[0] + ri < 0) || (pos[1] + rj > 14 || pos[1] + rj < 0)); ri -= add[0], rj -= add[1])
        {
            if ((ri == 5 * add[0] && rj == 5 * add[1])) break;
            if (chess == chessboard.grid[pos[0] + ri, pos[1] + rj])
            {
                //    linkNum++;
                str +="a";
            }
            else if (0 == chessboard.grid[pos[0] + ri, pos[1] + rj])
            {
                str += "_" ;
                break;
            }
            else
            {
                break;
            }
        }
       // Debug.Log(str);
        if (toScore.ContainsKey(str))
        {
            score[pos[0], pos[1]] += toScore[str];

        }
        

        //int[] l=new int[3], r=new int[3];
        //  Debug.Log(linkNum);
        //if (linkNum > 5)
        //{
        //    score[pos[0], pos[1], chess - 1] += 100000;//  连五
        //}
        //else if (linkNum == 4)
        //{
        //    li += add[0]; lj += add[1];
        //    if (0 == chessboard.grid[pos[0] + li, pos[1] + lj])
        //    {
        //        l[0] = true;
        //    }
        //    ri -= add[0]; rj -= add[1];
        //    if (0 == chessboard.grid[pos[0] + ri, pos[1] + rj])
        //    {
        //        r[0] = true;
        //    }
        //    if (l[0] && r[0])
        //    {
        //        score[pos[0], pos[1], chess - 1] += 10000;//活四    
        //    }
        //    else if(l[0]||r[0])
        //    {
        //        score[pos[0], pos[1], chess - 1] += 5000;//死四
        //    }
        //}
        //else if (linkNum == 3)
        //{
        //    li += add[0]; lj += add[1];
        //    if (0 == chessboard.grid[pos[0] + li, pos[1] + lj])
        //    {
        //        l[0] = true;
        //    }
        //    li += add[0]; lj += add[1];
        //    if (0 == chessboard.grid[pos[0] + li, pos[1] + lj])
        //    {
        //        l[1] = true;
        //    }
        //    ri -= add[0]; rj -= add[1];
        //    if (0 == chessboard.grid[pos[0] + ri, pos[1] + rj])
        //    {
        //        r[0] = true;
        //    }
        //    ri -= add[0]; rj -= add[1];
        //    if (0 == chessboard.grid[pos[0] + ri, pos[1] + rj])
        //    {
        //        r[1] = true;
        //    }
        //    if (l[0]&&r[0])
        //    {
        //        if (l[1] && r[1])
        //        {
        //            score[pos[0], pos[1], chess - 1] += 7000;//活三
        //        }
        //        else if(l[1]||r[1])
        //        {
        //            score[pos[0], pos[1], chess - 1] += 6000;//
        //        }
        //        else 
        //        {
        //            score[pos[0], pos[1], chess - 1] += 5000;
        //        }
        //    }
        //    else if (l || r)
        //    {
        //        score[pos[0], pos[1], chess - 1] += 5000;
        //    }
        //}
    }
}
