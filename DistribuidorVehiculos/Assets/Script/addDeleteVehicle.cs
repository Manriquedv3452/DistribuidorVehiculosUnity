using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addDeleteVehicle : MonoBehaviour {

    public string type;
    public int car;
    private GameObject lineControllerGenerator;
    private lineGenerator generator;

    // Use this for initialization
    void Start () {
        lineControllerGenerator = GameObject.Find("lineGenerator");
        generator = lineControllerGenerator.GetComponent<lineGenerator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        switch (type)
        {
            case "ADD":
                generator.addVehicle(car);
                break;
            case "DELETE":
                generator.deleteVehicle(car);
                break;
            default:
                return;
        }
    }
}

