using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public WheelCollider[] wheels = new WheelCollider[4];
    public GameObject[] wheelMesh = new GameObject[4];
    // 엔진이 내는 순간적인 힘, 단위 kgmㆍm => 1m의 줄에 중량 1kg의 물체를 달아 회전시키는 힘
    public float maxToque = 30;
    public float steeringMax = 4;
    public float angle = 0;
    public float kmh;
    public GameData gameData;
    public GameObject detination;
    public float minDistance = 5.0f;
    public GameObject GameClear;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -1, 0);
        gameData = GameObject.FindGameObjectWithTag("Data").GetComponent<GameData>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(new Vector2(this.transform.position.x, this.transform.position.z), new Vector2(detination.transform.position.x, detination.transform.position.z)) < minDistance)
        {
            GameClear.SetActive(true);
            gameData.SetStop(0);
            
        }
    }

    public void SetCar()
    {
        
        if(gameData != null)
        {
            maxToque = gameData.maxToque;

            switch (gameData.engineNum)
            {
                case 0: // 기본
                    break;
                case 1: //  터보
                    maxToque += 50;
                    break;
                case 2:
                    maxToque += 100;
                    break;
            }

            if(gameData.stageNum == gameData.tireNum)
            {
                maxToque += 50;
            }
            else
            {
                maxToque -= 50;
            }
        }

    }

    private void FixedUpdate()
    {
        PlayToque();

        //AnimWheels();

    }

    public void AddToque(float add)
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            Debug.Log(wheels[i].motorTorque);
            wheels[i].motorTorque += add * Input.GetAxis("Vertical");
        }
    }

    void PlayToque()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                if (wheels[i].motorTorque < maxToque)
                {
                    wheels[i].motorTorque = maxToque * Input.GetAxis("Vertical");
                }
            }
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            for (int i = 0; i < wheels.Length - 2; i++)
            {
                wheels[i].steerAngle = Input.GetAxis("Horizontal") * steeringMax  + 0;

            }
        }
        else
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].steerAngle = 0;

            }
        }
        kmh = Mathf.Round(this.GetComponent<Rigidbody>().velocity.magnitude * 3.6f);
    }

    void AnimWheels()
    {
        //Vector3 wheelPos = Vector3.zero;
        //Quaternion wheelRot = Quaternion.identity;

        //for(int i = 0; i < 4; i++)
        //{
        //    wheelMesh[i].transform.Rotate(wheels[i].rpm / 60 * 360 * Time.fixedDeltaTime, 0, 0);
        //}
        for (int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheels[i].GetWorldPose(out pos, out quat);
            wheelMesh[i].transform.position = pos;
            quat.z += angle;
            wheelMesh[i].transform.rotation = quat;
        }
    }
    public void BtnClear()
    {
        Debug.Log("test");
        gameData.stageNum++;
        if(gameData.stageNum > gameData.maxStageNum)
        {
            gameData.SetRank(gameData.gold.ToString());
            gameData.InitData();
            gameData.SetStop(1);
            gameData.ChangeScene(0);
        }
        else
        {
            switch (gameData.stageNum)
            {
                case 2:
                    {
                        gameData.gold += 100000;
                    }
                    break;
                case 3:
                    {
                        gameData.gold += 1000000;
                    }
                    break;
            }
            gameData.SetStop(1);
            gameData.ChangeScene(1);
        }
    }

}
