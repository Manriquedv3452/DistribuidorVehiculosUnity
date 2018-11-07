using DistribucionVehiculosGeneticos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class geneticController : MonoBehaviour {

    public GameObject lineGeneratorObject;
    private lineGenerator lineController;
    public GameObject salaEspera;
    private waitRoom salaEsperaController;

	// Use this for initialization
	void Start () {
        lineController = lineGeneratorObject.GetComponent<lineGenerator>();
        salaEsperaController = salaEspera.GetComponent<waitRoom>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private List<Linea> obtenerListaLineas()
    {
        int numberLines = lineController.getNumberLines();
        bool[,] vehicles = lineController.getVehicles();
        int[] times = lineController.getTimes();
        List<Linea> lineas = new List<Linea>();
        GameObject line;

        for (int i = 0; i < numberLines; i++)
        {
            line = GameObject.Find("linea" + (i + 1).ToString());
            lines tempLine = line.GetComponent<lines>();

            int maxTime = times[i]; // Tiempo máximo disponible.
            float timeReach = tempLine.getTime(); // Tiempo que llevan los carros atendidos.

            int restantTime = (int)System.Math.Round(maxTime - timeReach); // Tiempo restante.

            List<char> tiposVehiculos = getListType(vehicles, i);
            Linea linea = new Linea(tempLine.getLine(), maxTime, tiposVehiculos, true, restantTime); // Generamos la línea.
            lineas.Add(linea);
        }

        return lineas;
    }

    private List<Vehiculo> obtenerListaVehiculos()
    {

        List<Vehiculo> vehiculos = new List<Vehiculo>();
        List<GameObject> listaCarros = salaEsperaController.getListCars();
        foreach(GameObject car in listaCarros)
        {
            vehicle classCar = car.GetComponent<vehicle>();
            int id = classCar.getID();
            int time = (int) System.Math.Round(classCar.getTime());
            char type = classCar.getType();
            Vehiculo vehiculo = new Vehiculo(id, type, time);
            vehiculos.Add(vehiculo);
        }

        return vehiculos;
    }

    public List<Vehiculo> getSolution()
    {
        List<Linea> lineas = obtenerListaLineas();
        List<Vehiculo> vehiculos = obtenerListaVehiculos();

        AlgoritmoGenetico AG = new AlgoritmoGenetico(vehiculos, lineas);
        Individuo solucion = AG.IniciarGenetico();
        Debug.Log("GENERACION #" + AG.getGeneracionActual() + "\n");
        Debug.Log(solucion.PrintStringIndividuo());
        // Vehiculos a la linea a la que pertenecen.
        List<Vehiculo> lista = solucion.GetVehiculos();
        return lista;
    }

    private List<char> getListType(bool[,] vehicles, int pos)
    {
        List<char> lista = new List<char>();

        for(int i = 0; i < 7; i++)
        {
            if (vehicles[pos, i])
            {
                char letter = ' ';
                switch (i)
                {
                    case 0:
                        letter = 'a';
                        break;
                    case 1:
                        letter = 'b';
                        break;
                    case 2:
                        letter = 'c';
                        break;
                    case 3:
                        letter = 'd';
                        break;
                    case 4:
                        letter = 'e';
                        break;
                    case 5:
                        letter = 'f';
                        break;
                    case 6:
                        letter = 'g';
                        break;
                    default:
                        letter = ' ';
                        break;
                }
                lista.Add(letter);
            }
        }

        return lista;
    }
}
