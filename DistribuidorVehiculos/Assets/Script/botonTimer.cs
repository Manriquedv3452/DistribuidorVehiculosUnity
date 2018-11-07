using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botonTimer : MonoBehaviour {

    private lines line;
    public GameObject lineObject;


    // Use this for initialization
    void Start () {
        line = lineObject.GetComponent<lines>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    // Press click on the button.
    void OnMouseDown()
    {
        // If the line is not paused.
        if (line.isAvailable())
        {
            line.waitTime(); // Paused the line.
        }else
        {
            line.stopRunThread(); // Stop the thread.
        }
    }
}
