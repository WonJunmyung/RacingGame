using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform Player;
    public float distance = 10.0f;
    public float height = 5.0f;
    public float smoothRotate = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    void Follow()
    {
        float yAngle = Mathf.LerpAngle(this.transform.eulerAngles.y, Player.eulerAngles.y, smoothRotate * Time.deltaTime);
        Quaternion rot = Quaternion.Euler(0, yAngle, 0);

        this.transform.position = Player.position + Vector3.up * height - rot * Vector3.forward * distance;
        this.transform.LookAt(Player);
    }
}
