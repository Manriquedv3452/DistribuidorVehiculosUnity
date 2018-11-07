using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botonSiguiente : MonoBehaviour {

    public string action;
    public string type;
    private GameObject camera;
    private CameraController cameraCode;
    private AudioSource audioButton;

    // Use this for initialization
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        cameraCode = camera.GetComponent<CameraController>();
        audioButton = GetComponent<AudioSource>();
        audioButton.Stop();
    }

    // Update is called once per frame
    void Update () {
		
	}

    // When is pressed.
    void OnMouseDown()
    {
        audioButton.Play();
        if (action == "BACK")
        {
            cameraCode.changeFirstScreen();
        }

        cameraCode.camaraMuevete(action, false);
        if (type == "FIRSTLINES")
        {
            createLines();
        }
        else if (type == "GENERATOR")
        {
            generateLines();
        }
    }

    private void createLines()
    {
        GameObject generatorLines = GameObject.Find("lineGenerator");
        generatorLines.GetComponent<lineGenerator>().initialize();
    }

    private void generateLines()
    {
        GameObject generatorLines = GameObject.Find("lineGenerator");
        generatorLines.GetComponent<lineGenerator>().selectElements();
    }
}
