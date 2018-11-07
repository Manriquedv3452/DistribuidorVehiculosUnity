using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distribuirButton : MonoBehaviour {

    private GameObject camera;
    private CameraController cameraCode;
    public GameObject salaEspera;
    private waitRoom waitRoomObject;
    public GameObject genetic;
    private geneticController geneticClass;
    
    // Use this for initialization
	void Start () {
        camera = GameObject.Find("Main Camera");
        waitRoomObject = salaEspera.GetComponent<waitRoom>();
        cameraCode = camera.GetComponent<CameraController>();
        geneticClass = genetic.GetComponent<geneticController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // When is pressed.
    void OnMouseDown()
    {
        if (!waitRoomObject.emptyList())
        {
            cameraCode.camaraMuevete("NEXT", false);
            cameraCode.changeLastScreen();
            // Empezar el algoritmo de ordenamiento ... realizarlo.

            List<DistribucionVehiculosGeneticos.Vehiculo> listaVehiculos = geneticClass.getSolution();

            GameObject point = GameObject.Find("start");
            point.GetComponent<pointStart>().setVehicles(listaVehiculos);
            point.GetComponent<pointStart>().firstCall();
        }
        else
        {
            Debug.Log("No hay vehículos asignados a la lista de espera. ");
        }
    }
}
