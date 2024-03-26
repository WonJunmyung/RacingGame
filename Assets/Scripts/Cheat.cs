using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cheat : MonoBehaviour
{
    public GameData gameData;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        gameData = GameObject.FindGameObjectWithTag("Data").GetComponent<GameData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            //Time.timeScale = 0;

        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            //Time.timeScale = 0;
            gameData.cheatStore = !gameData.cheatStore;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            //Time.timeScale = 0;
            gameData.ChangeScene(gameData.sceneNum);

        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            //Time.timeScale = 0;
            if(gameData.stageNum +1 > gameData.maxStageNum)
            {
                gameData.stageNum = 1;
            }
            else
            {
                gameData.stageNum++;
            }
            gameData.ChangeScene(gameData.stageNum + 1);

        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }

        }
    }
}
