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
    private AudioSource[] sounds;
    
    // Use this for initialization
	void Start () {
        camera = GameObject.Find("Main Camera");
        sounds = GetComponents<AudioSource>();
        waitRoomObject = salaEspera.GetComponent<waitRoom>();
        cameraCode = camera.GetComponent<CameraController>();
        geneticClass = genetic.GetComponent<geneticController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Review if there is a valid solution.
    private bool revisarSolucion(List<DistribucionVehiculosGeneticos.Vehiculo> listaVehiculo)
    {
        foreach (DistribucionVehiculosGeneticos.Vehiculo veh in listaVehiculo)
        {
            if (veh.GetLineaAsignada() != null)
            {
                return true;
            }
        }
        return false;
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

            if (revisarSolucion(listaVehiculos))
            {
                sounds[0].Play(); // Sonido... se asignaron los carros.
            }
            else
            {
                sounds[1].Play(); // Sonido... no se asignaron los carros.
            }
        }
        else
        {
            sounds[1].Play(); // Sonido .. no se asigno ningun vehiculo.
        }
    }
}
