using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitRoom : MonoBehaviour {

    private List<Transform> points;
    private Dictionary<string, bool> dictionaryPosition;
    private Dictionary<string, GameObject> cars;

	// Use this for initialization
	void Start () {
        points = new List<Transform>();
        dictionaryPosition = new Dictionary<string, bool>();
        cars = new Dictionary<string, GameObject>(); // Dictionary the respective cars.
        GameObject gameObjectPoints = GameObject.Find("points");
        Transform[] children = gameObjectPoints.GetComponentsInChildren<Transform>(true);
        foreach(Transform item in children)
        {
            if (item.name != "points")
            {
                points.Add(item); // Save the gameObject.
                dictionaryPosition.Add(item.name, false); // The space is free to save a new value.
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /* Add a new car and position in the respective position in the dictionary. */
    public void addCar(GameObject car)
    {
        foreach(Transform item in points)
        {
            var element = dictionaryPosition[item.name];
            if (!element)
            {
                car.transform.position = item.position;
                dictionaryPosition[item.name] = true;
                cars[item.name] = car;
                return;
            }
        }

        // Show message that there is not available to save more.
    }

    public GameObject getCar()
    {
        foreach(string name in dictionaryPosition.Keys)
        {
            if (dictionaryPosition[name])
            {
                dictionaryPosition[name] = false;
                Debug.Log("NameL: " + name);
                GameObject value = cars[name];
                cars.Remove(name);
                return value;
            }
        }

        return null;
    }

    public List<GameObject> getListCars()
    {
        List<GameObject> lista = new List<GameObject>();

        foreach(string name in cars.Keys)
        {
            if (cars[name])
            {
                lista.Add(cars[name]);   
            }
        }
        return lista;
    }

    public GameObject getCarById(int id)
    {
        foreach(string name in cars.Keys)
        {
            if (cars[name])
            {
                int tempId = cars[name].GetComponent<vehicle>().getID();

                if (tempId == id)
                {
                    dictionaryPosition[name] = false;
                    GameObject value = cars[name];
                    cars.Remove(name);
                    return value;
                }
            }
        }
        return null;
    }

    public bool thereIsAvailable()
    {
        if (emptyList())
        {
            return true;
        }

        foreach(bool state in dictionaryPosition.Values)
        {
            if (state == false)
            {
                return true;
            }
        }
        return false; // There isn't enough space.
    }

    /* Look if the list of values is empty. */
    public bool emptyList()
    {
        foreach(bool state in dictionaryPosition.Values)
        {
            if (state == true)
            {
                return false;
            }
        }
        return true;
    }
}
