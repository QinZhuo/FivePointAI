using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour {
    public Transform lastChess;
    public Chessboard chessboard;
    public Text stepText;
    public Text timerText;
    // Use this for initialization
    void Start () {
		
	}
	public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
	// Update is called once per frame
	void Update () {
        if (chessboard.step.Count > 0)
        {
            lastChess.transform.position = chessboard.step.Peek().position+new Vector3(0,0,-3);
        }
        stepText.text = "第" + (chessboard.step.Count+1) + "手";
        timerText.text = "计时："+chessboard.timer.ToString(".0");
    }
}
