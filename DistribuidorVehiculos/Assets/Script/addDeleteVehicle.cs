using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addDeleteVehicle : MonoBehaviour {

    public string type;
    public int car;
    private GameObject lineControllerGenerator;
    private lineGenerator generator;
    private AudioSource actionSound;

    // Use this for initialization
    void Start () {
        lineControllerGenerator = GameObject.Find("lineGenerator");
        generator = lineControllerGenerator.GetComponent<lineGenerator>();
        actionSound = GetComponent<AudioSource>();
        actionSound.Stop();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        bool state = false;
        switch (type)
        {
            case "ADD":
                state = generator.addVehicle(car);
                break;
            case "DELETE":
                state = generator.deleteVehicle(car);
                break;
            default:
                break;
        }

        if (state)
        {
            actionSound.Play();
        }
    }
}

