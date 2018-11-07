using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointStart : MonoBehaviour {

    public float timeSpeed;

    private GameObject salaEspera;
    private waitRoom salaObject;
    private GameObject lineGeneratorObject;
    private lineGenerator line;
    private float waitTime;
    private float timeExpected;
    private bool call;
    private List<DistribucionVehiculosGeneticos.Vehiculo> vehiculosTemporales;

	// Use this for initialization
	void Start () {
        salaEspera = GameObject.Find("SalaEspera");
        salaObject = salaEspera.GetComponent<waitRoom>();
        lineGeneratorObject = GameObject.Find("lineGenerator");
        line = lineGeneratorObject.GetComponent<lineGenerator>();
        waitTime = 0;
        timeExpected = 200;
        call = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (call)
        { 
            if (waitTime < timeExpected)
            {
                waitTime += timeSpeed;
            }else
            {
                callNewCar();
                call = false;
                waitTime = 0;
            }
        }
	}

    // make for first time.
    public void firstCall()
    {
        call = true;
    }

    // Set the list of possible cars.
    public void setVehicles(List<DistribucionVehiculosGeneticos.Vehiculo> lista)
    {
        this.vehiculosTemporales = lista;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "vehicle")
        {
            call = true; // If a car goes out ... call other.
        }
    }

 
    /* Call a new car.. */
    private void callNewCar()
    {
        // If there are not cars availables.
        if (vehiculosTemporales.Count <= 0)
        {
            return;
        }

        // Get the first car.
        DistribucionVehiculosGeneticos.Vehiculo tempVehiculo = vehiculosTemporales[0];

        // If the car is not going to be assigned.
        if (vehiculosTemporales[0].GetLineaAsignada() != null)
        {
            GameObject newCar = salaObject.getCarById(tempVehiculo.GetId()); // Get the respective car of the waitRoom.
            if (newCar)
            {
                newCar.transform.position = new Vector3(transform.position.x, transform.position.y, newCar.transform.position.z);
                GameObject lineObject = getLine(tempVehiculo.GetLineaAsignada().GetId()); // Get the line of the car.
                lines line = lineObject.GetComponent<lines>(); // Call the script of the line.
                newCar.GetComponent<vehicle>().setActualLine(line); // Add to the car, the state of the actual line.
                line.addElement(newCar); // Add the new car to the line.
                vehiculosTemporales.Remove(tempVehiculo);
            }
        }
        else
        {
            vehiculosTemporales.Remove(tempVehiculo); // Remove the car from the possibles ... and call other.
            callNewCar();
        }
    }

    private GameObject getLine(int idLine)
    {
        string lineName = "linea" + idLine.ToString();
        return GameObject.Find(lineName);
    }
}
