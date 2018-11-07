using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botonMantenimiento : MonoBehaviour {

    public int linea;
    public string type;
    private GameObject text;
    private GameObject camera;
    private CameraController cameraCode;
    private GameObject lineGeneratorObject;

    // Use this for initialization
    void Start () {
        camera = GameObject.Find("Main Camera");
        cameraCode = camera.GetComponent<CameraController>();
        text = GameObject.Find("lineFinalScreen");
        lineGeneratorObject = GameObject.Find("lineGenerator");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        if (type == "NEXT")
        {
            string nameLine = "Línea " + linea;
            text.GetComponent<TextMesh>().text = nameLine;
            lineGeneratorObject.GetComponent<lineGenerator>().changeDataFinalScreen(nameLine);
        }
        else if (type == "BACK")
        {
            generateLines();
            cameraCode.expandableScreen();
        }
        cameraCode.camaraMuevete(type, true);
    }

    private void generateLines()
    {
        GameObject generatorLines = GameObject.Find("lineGenerator");
        generatorLines.GetComponent<lineGenerator>().selectElements();
    }
}
