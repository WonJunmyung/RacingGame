using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public float spotSpeed = 10.0f;
    public string[] tireInfo =
    {
        "기본 타이어, $0",
        "사막 타이어, $10000",
        "산악 타이어, $100000",
        "도시 타이어, $1000000"
    };
    public int num = 0;

    public Transform SpotCircle;
    public Text TextTire;
    public Text Gold;
    public GameData gameData;
    public GameObject Message;
    public GameObject[] tireMesh;
    public GameObject[] engineBuy;
    
    

    // Start is called before the first frame update
    void Start()
    {
        TextTire.text = tireInfo[num];
        gameData = GameObject.FindGameObjectWithTag("Data").GetComponent<GameData>();
        Gold.text = "$" + gameData.gold.ToString();
        SetTire(gameData.tireNum);
        for(int i = 0; i < gameData.engineNum; i++)
        {
            engineBuy[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpotCircle.Rotate(Vector3.up * spotSpeed * Time.deltaTime);
    }

    void SetTire(int num)
    {
        for (int i = 0; i < tireMesh.Length; i++)
        {
            if (num == i)
            {
                tireMesh[i].SetActive(true);
            }
            else
            {
                tireMesh[i].SetActive(false);
            }
        }
    }

    public void BtnLeft()
    {
        num--;
        if(num <= 0)
        {
            num = tireInfo.Length - 1;
        }
        TextTire.text = tireInfo[num];
        SetTire(num);
    }

    public void BtnRight()
    {
        num++;
        if (num >= tireInfo.Length)
        {
            num = 0;
        }
        TextTire.text = tireInfo[num];
        SetTire(num);
    }

    public void BtnBuy()
    {
        if(num != 0)
        {
            if(gameData.gold < gameData.tireCost[num] && !gameData.cheatStore)
            {
                Message.transform.GetChild(0).GetComponent<Text>().text = "골드가 부족합니다.";
                Message.SetActive(true);
                Invoke("Hide", 1.0f);
            }
            else
            {
                Message.transform.GetChild(0).GetComponent<Text>().text = "타이어가 변경되었습니다.";
                Message.SetActive(true);
                gameData.tireNum = num;
                if (!gameData.cheatStore)
                {
                    gameData.gold -= gameData.tireCost[num];
                    Gold.text = "$" + gameData.gold.ToString();

                }
                SetTire(num);
                Invoke("Hide", 1.0f);
            }
        }
    }
    void Hide()
    {
        Message.SetActive(false);
    }

    public void BtnStart()
    {

        gameData.ChangeScene(gameData.stageNum + 1);
    }

    public void BtnSixEngine()
    {
        if (gameData.gold < gameData.engineCost[1] && !gameData.cheatStore)
        {
            Message.transform.GetChild(0).GetComponent<Text>().text = "골드가 부족합니다.";
            Message.SetActive(true);
            Invoke("Hide", 1.0f);
        }
        else
        {
            Message.transform.GetChild(0).GetComponent<Text>().text = "엔진이 변경되었습니다.";
            Message.SetActive(true);
            gameData.engineNum = 1;
            if (!gameData.cheatStore)
            {
                gameData.gold -= gameData.engineCost[1];
                Gold.text = "$" + gameData.gold.ToString();
            }
            Invoke("Hide", 1.0f);
        }
    }
    public void BtnEightEngine()
    {
        if (gameData.gold < gameData.engineCost[2] && !gameData.cheatStore)
        {
            Message.transform.GetChild(0).GetComponent<Text>().text = "골드가 부족합니다.";
            Message.SetActive(true);
            Invoke("Hide", 1.0f);
        }
        else
        {
            Message.transform.GetChild(0).GetComponent<Text>().text = "엔진이 변경되었습니다.";
            Message.SetActive(true);
            gameData.engineNum = 2;
            if (!gameData.cheatStore)
            {
                gameData.gold -= gameData.engineCost[2];
                Gold.text = "$" + gameData.gold.ToString();
            }
            
            Invoke("Hide", 1.0f);
        }
    }
}
