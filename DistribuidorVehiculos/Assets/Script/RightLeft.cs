using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLeft : MonoBehaviour {

    public string type;
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
            case "RIGHT":
                generator.nextLine();
                break;
            case "LEFT":
                generator.lastLine();
                break;
            case "UP":
                generator.increaseTime();
                break;
            case "DOWN":
                generator.decreaseTime();
                break;
            default:
                return;
        }
    }
}
