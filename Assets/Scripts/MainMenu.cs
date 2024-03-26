using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameData gameData;
    public Text TextRank;

    // Start is called before the first frame update
    void Start()
    {
        gameData = GameObject.FindGameObjectWithTag("Data").GetComponent<GameData>();
        SetRank();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void BtnStart()
    {
        gameData.ChangeScene(1);
    }
    public void BtnExit()
    {
        Application.Quit();
    }

    void SetRank()
    {
        if(gameData.rank == "")
        {
            TextRank.text = "- 랭킹 정보가 없습니다 -";
        }
        else
        {
            string[] temp = gameData.rank.Split('|');
            TextRank.text = "";
            for(int i=0;i<temp.Length; i++)
            {
                TextRank.text += temp[i] + System.Environment.NewLine;
            }
        }
    }
}
