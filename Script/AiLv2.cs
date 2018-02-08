using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiLv2 : SamepleAi {

	// Use this for initialization
	void Start () {
        if (chessColor != Chess.观战) Debug.Log(chessColor + "AI: LV" + 2);
        toScore = new SortedList<string, float>();
        
        



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

        toScore.Add("_aa_a_", 9000);                     //跳活三
        toScore.Add("_a_aa_", 9000);

        toScore.Add("_aaa_", 10000);                    //活三
       
       

        toScore.Add("a_aaa", 15000);                     //冲四
        toScore.Add("aaa_a", 15000);                     //冲四
        toScore.Add("_aaaa", 15000);                    //冲四
        toScore.Add("aaaa_", 15000);                    //冲四
        toScore.Add("aa_aa", 15000);                    //冲四
        


        toScore.Add("_aaaa_", 100000);                 //活四

        toScore.Add("aaaaa", 1000000000);             //连五
      
    }
    protected override void CheckOneLine(int[] pos, int[] add, int chess)
    {

        if (chessboard.grid[pos[0], pos[1] ]!=0)
        {
            score[pos[0], pos[1]] = 0;
            return;
        }
        string str = "";
        str += "a";
        int AllNum = 1;
        int LinkNum = 1;
        int li, lj;
        li = add[0]; lj = add[1];
        bool lstop = false,rstop = false;
        int ri = -add[0], rj = -add[1];
        bool lfirst=true;
        while (AllNum<7&&(!lstop||!rstop))
        {
            if (lfirst)
            {
                if (((pos[0] + li <= 14 && pos[0] + li >= 0) && (pos[1] + lj <= 14 && pos[1] + lj >= 0)) && !lstop)
                {
                    //if ((li == 5 * add[0] && lj == 5 * add[1]))
                    //{
                    //    if(!rstop) lfirst = false; lstop = true;
                    //}

                    if (chess == chessboard.grid[pos[0] + li, pos[1] + lj])
                    {
                        str = "a" + str;
                        // if (!lstop) lfirst = true;
                        LinkNum++;
                        AllNum++;
                    }
                    else if (0 == chessboard.grid[pos[0] + li, pos[1] + lj])
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

                    if (chess == chessboard.grid[pos[0] + ri, pos[1] + rj])
                    {
                        // if (!rstop) lfirst = false;
                        str += "a";
                        LinkNum++;
                        AllNum++;
                    }
                    else if (0 == chessboard.grid[pos[0] + ri, pos[1] + rj])
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

        string cmpTrue="";
        foreach (var keyInfo in toScore)
        {
            if (str.Contains( keyInfo.Key))                
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
            score[pos[0], pos[1]] += toScore[cmpTrue];
           
        }
       

    }
    // Update is called once per frame
    void Update () {
		
	}
}
