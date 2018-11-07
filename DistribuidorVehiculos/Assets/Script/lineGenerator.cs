using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineGenerator : MonoBehaviour {

    public int timeToEdit;
    private string[] names;
    private int[] times;
    private bool[,] vehicles;
    private int position;
    public GameObject lineName;
    public GameObject vehiclesFinalText;
    public GameObject timeText;
    public GameObject vehiclesText;

    // Use this for initialization
    void Start() {
        names = null;
        times = null;
        vehicles = null;
        position = 0;
    }

    // Initialize the list of vehicles with boolean elements.
    private void initializeBoolList()
    {
        for (int i = 0; i < names.Length; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                vehicles[i, j] = false;
            }
        }
    }

    private void initializeLinesName()
    {
        for (int i = 0; i < names.Length; i++)
        {
            names[i] = "Línea " + (i + 1).ToString();
        }
    }

    private void initializeLinesTime()
    {
        for (int i = 0; i < times.Length; i++)
        {
            times[i] = 80;
        }
    }

    private void newSelection()
    {
        lineName.GetComponent<TextMesh>().text = names[position];
        timeText.GetComponent<TextMesh>().text = times[position] + "s";
        changeVehicles();
    }

    // Initialize all the data.
    public void initialize()
    {
        position = 0;
        GameObject listNumbers = GameObject.Find("numeros");
        GameObject chosen = listNumbers.GetComponent<numbersController>().getChosen();
        int value = chosen.GetComponent<numbers>().getValue();
        names = new string[value];
        times = new int[value];
        vehicles = new bool[value, 7];
        initializeLinesName();
        initializeLinesTime();
        initializeBoolList();
        newSelection();
    }

    public void nextLine()
    {
        position++;
        if (position >= names.Length)
        {
            position = 0;
        }
        newSelection();
    }

    public void lastLine()
    {
        position--;
        if (position < 0)
        {
            position = names.Length - 1;
        }
        newSelection();
    }

    private void changeVehicles()
    {
        bool statePrevious = false;
        string text = "";
        for (int i = 0; i < 7; i++)
        {
            if (vehicles[position, i] == true)
            {
                if (statePrevious)
                {
                    text += " - ";
                }
                text += getNameVehicle(i);
                statePrevious = true;
            }
        }

        if (!statePrevious)
        {
            text = "Ninguno";
        }

        vehiclesText.GetComponent<TextMesh>().text = text;
        vehiclesFinalText.GetComponent<TextMesh>().text = text;
    }

    private string getNameVehicle(int numberCar)
    {
        switch (numberCar)
        {
            case 0:
                return "Moto Vieja";
            case 1:
                return "Moto Nueva";
            case 2:
                return "Carro Viejo";
            case 3:
                return "Carro Nuevo";
            case 4:
                return "Bus";
            case 5:
                return "Camión Dos Ejes";
            case 6:
                return "Camión Cinco Ejes";
            default:
                return "Ninguno";
        }
    }

    public void changeDataFinalScreen(string name)
    {
        for(int i = 0; i < names.Length; i++)
        {
            if (name == names[i])
            {
                position = i;
                changeVehicles();
                return;
            }
        }
    }

    public void increaseTime()
    {
        if (times[position] < 150000)
        {
            times[position] += timeToEdit;
        }
        newSelection();
    }

    public void decreaseTime()
    {
        if (times[position] > 50)
        {
            times[position] -= timeToEdit;
        }
        newSelection();
    }

    public bool addVehicle(int carPosition)
    {
        bool stateOperation = false;

        if (vehicles[position, carPosition - 1] == false)
        {
            stateOperation = true;
        }

        vehicles[position, carPosition - 1] = true;
        changeVehicles();

        return stateOperation;
    }

    public bool deleteVehicle(int carPosition)
    {
        bool stateOperation = false;

        if (vehicles[position, carPosition - 1] == true)
        {
            stateOperation = true;
        }

        vehicles[position, carPosition - 1] = false;
        changeVehicles();

        return stateOperation;
    }

    public void selectElements()
    {
        GameObject results = GameObject.Find("Resultados");
        Transform[] children = results.GetComponentsInChildren<Transform>(true);
        foreach (Transform item in children)
        {
            if (validName(item.name))
            {
                Transform[] grandChildren = item.GetComponentsInChildren<Transform>(true);
                
                int positionChild = positionElement(item.name);

                foreach (Transform babyChildren in grandChildren)
                {
                    
                    if (babyChildren.name == "tiempoText")
                    {
                        babyChildren.GetComponent<TextMesh>().text = findTimesResults(positionChild);
                    }

                    if (babyChildren.name == "motosTexto")
                    {
                        babyChildren.GetComponent<TextMesh>().text = findMotosResults(positionChild);
                    }

                    if (babyChildren.name == "carrosTexto")
                    {
                        babyChildren.GetComponent<TextMesh>().text = findCarsResults(positionChild);
                    }

                    if (babyChildren.name == "busTexto")
                    {
                        babyChildren.GetComponent<TextMesh>().text = findBusResults(positionChild);
                    }

                    if (babyChildren.name == "camionesTexto")
                    {
                        babyChildren.GetComponent<TextMesh>().text = findTruckResults(positionChild);
                    }

                    if (babyChildren.name == "botonMantenimiento" && positionChild == -1)
                    {
                        Destroy(babyChildren.gameObject);
                    }

                    if (babyChildren.name == "botonPausar" && positionChild == -1)
                    {
                        Destroy(babyChildren.gameObject);
                    }

                    if (babyChildren.name == "flechas" && positionChild == -1)
                    {
                        Destroy(babyChildren.gameObject);
                    }
                }
            }
        }
    }

    public bool validName(string possibleName)
    {
        switch (possibleName)
        {
            case "ResultadoLineaUno":
                return true;
            case "ResultadoLineaDos":
                return true;
            case "ResultadoLineaTres":
                return true;
            case "ResultadoLineaCuatro":
                return true;
            case "ResultadoLineaCinco":
                return true;
            case "ResultadoLineaSeis":
                return true;
            case "ResultadoLineaSiete":
                return true;
            case "ResultadoLineaOcho":
                return true;
            case "ResultadoLineaNueve":
                return true;
            case "ResultadoLineaDiez":
                return true;
            default:
                return false;
        }
    }

    public int positionElement(string possibleElement)
    {
        string name;
        switch (possibleElement)
        {
            case "ResultadoLineaUno":
                name = "Línea 1";
                break;
            case "ResultadoLineaDos":
                name = "Línea 2";
                break;
            case "ResultadoLineaTres":
                name = "Línea 3";
                break;
            case "ResultadoLineaCuatro":
                name = "Línea 4";
                break;
            case "ResultadoLineaCinco":
                name = "Línea 5";
                break;
            case "ResultadoLineaSeis":
                name = "Línea 6";
                break;
            case "ResultadoLineaSiete":
                name = "Línea 7";
                break;
            case "ResultadoLineaOcho":
                name = "Línea 8";
                break;
            case "ResultadoLineaNueve":
                name = "Línea 9";
                break;
            case "ResultadoLineaDiez":
                name = "Línea 10";
                break;
            default:
                name = "";
                break;
        }

        for (int i = 0; i < names.Length; i++)
        {
            if (name == names[i])
            {
                return i;
            }
        }

        return -1;
    }

    public string findTimesResults(int positionChild)
    {
        string text;
        if (positionChild > -1)
        {
            text = times[positionChild].ToString() + "s";
        }
        else
        {
            text = "0s";
        }
        return text;
    }

    public string findMotosResults(int position)
    {
        string text;
        if (position > -1)
        {
            if (vehicles[position, 0] && vehicles[position, 1])
            {
                text = "Motos: Vieja y Nueva";
            }
            else if (vehicles[position, 0] && !vehicles[position, 1])
            {
                text = "Motos: Vieja";
            }
            else if (!vehicles[position, 0] && vehicles[position, 1])
            {
                text = "Motos: Nueva";
            }else
            {
                text = "Motos: Ninguna";
            }
        }else
        {
            text = "Ninguna";
        }
        return text;
    }

    public string findCarsResults(int position)
    {
        string text;
        if (position > -1)
        {
            if (vehicles[position, 2] && vehicles[position, 3])
            {
                text = "Carros: Viejo y Nuevo";
            }
            else if (vehicles[position, 2] && !vehicles[position, 3])
            {
                text = "Carros: Viejo";
            }
            else if (!vehicles[position, 2] && vehicles[position, 3])
            {
                text = "Carros: Nuevo";
            }
            else
            {
                text = "Motos: Ninguna";
            }
        }
        else
        {
            text = "Ninguna";
        }
        return text;
    }

    public string findBusResults(int position)
    {
        string text;
        if (position > -1)
        {
            if (vehicles[position, 4])
            {
                text = "Bus: Claro";
            }
            else
            {
                text = "Bus: Ninguno";
            }
        }
        else
        {
            text = "Ninguna";
        }
        return text;
    }

    public string findTruckResults(int position)
    {
        string text;
        if (position > -1)
        {
            if (vehicles[position, 5] && vehicles[position, 6])
            {
                text = "Camiones: Todos";
            }
            else if (vehicles[position, 5] && !vehicles[position, 6])
            {
                text = "Camiones: Dos Ejes";
            }
            else if (!vehicles[position, 5] && vehicles[position, 6])
            {
                text = "Camiones: Cinco Ejes";
            }
            else
            {
                text = "Motos: Ninguna";
            }
        }
        else
        {
            text = "Ninguna";
        }
        return text;
    }

    public int getNumberLines()
    {
        return names.Length;
    }

    public bool[,] getVehicles()
    {
        return this.vehicles;
    }

    public int[] getTimes()
    {
        return this.times;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
