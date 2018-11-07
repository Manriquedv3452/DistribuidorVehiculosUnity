using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLeft : MonoBehaviour {

    public string type;
    private GameObject lineControllerGenerator;
    private lineGenerator generator;
    private AudioSource[] arrowSound;

	// Use this for initialization
	void Start () {
        lineControllerGenerator = GameObject.Find("lineGenerator");
        generator = lineControllerGenerator.GetComponent<lineGenerator>();
        arrowSound = GetComponents<AudioSource>();

        // Stop the start sound.
        foreach(AudioSource sound in arrowSound)
        {
            sound.Stop();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        switch (type)
        {
            case "RIGHT":
                arrowSound[0].Play(); // Play the sound.
                generator.nextLine();
                break;
            case "LEFT":
                arrowSound[0].Play(); // Play the sound.
                generator.lastLine();
                break;
            case "UP":
                arrowSound[1].Play();
                generator.increaseTime();
                break;
            case "DOWN":
                arrowSound[1].Play();
                generator.decreaseTime();
                break;
            default:
                return;
        }
    }
}
