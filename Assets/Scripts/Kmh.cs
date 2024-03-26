using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kmh : MonoBehaviour
{
    public CarController carController;
    // Start is called before the first frame update
    void Start()
    {
        carController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(carController != null)
        {
            this.GetComponent<Text>().text = carController.kmh + "km/h";
        }
    }
}
