using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarAI : MonoBehaviour
{

    public WaypointNode waypointNode;
    public List<Transform> nodes = new List<Transform>();
    public Transform currentWaypoint;
    public int currentNum = 0;

    [Range(0, 10)]
    public float sterrForce;
    public float minDistance = 5.0f;

    public float vertical = 3.0f;
    public float horizontal;

    public WheelCollider[] wheels = new WheelCollider[4];
    public GameObject[] wheelMesh = new GameObject[4];
    // 엔진이 내는 순간적인 힘, 단위 kgmㆍm => 1m의 줄에 중량 1kg의 물체를 달아 회전시키는 힘
    public float maxToque = 30;
    public float steeringMax = 4;
    public float angle = 0;
    public bool isStop = false;
    GameData gameData;
    public GameObject GameFail;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i< waypointNode.transform.childCount;i++)
        {
            nodes.Add(waypointNode.transform.GetChild(i));
        }
        currentNum = 0;
        this.GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -1, 0);
        gameData = GameObject.FindGameObjectWithTag("Data").GetComponent<GameData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (!isStop)
        {
            AIDrive();
            PlayToque();
        }
    }

    void PlayToque()
    {
        if (vertical != 0)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = maxToque * vertical;
            }
        }
        if (horizontal != 0)
        {
            for (int i = 0; i < wheels.Length - 2; i++)
            {
                wheels[i].steerAngle = horizontal * steeringMax + 0;

            }
        }
        else
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].steerAngle = 0;

            }
        }
    }

    private void AIDrive()
    {
        AISteer();
        CalMinDistanceWaypoint();
    }

    private void AISteer()
    {
        Vector3 relative = transform.InverseTransformPoint(nodes[currentNum].position);
        
        relative /= relative.magnitude;
        

        horizontal = (relative.x / relative.magnitude) * sterrForce;
        
    }

    private void CalMinDistanceWaypoint()
    {
        if(Vector2.Distance(new Vector2(this.transform.position.x, this.transform.position.z), new Vector2(nodes[currentNum].position.x, nodes[currentNum].position.z)) < minDistance)
        {
            currentNum++;
        }
        if (!isStop)
        {
            if(nodes.Count <= currentNum)
            {
                isStop = true;
                GameFail.SetActive(true);
                gameData.SetStop(0);
                
            }
        }
    }

    public void BtnFail()
    {
        //gameData.InitData();
        //gameData.SetStop(1);
        gameData.SetStop(1);
        gameData.ChangeScene(gameData.sceneNum);
        
    }
}
