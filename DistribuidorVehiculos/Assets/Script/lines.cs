using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lines : MonoBehaviour {

    public int linea; // ID
    public GameObject textTime; // Represent of the text time total.
    public GameObject textTimePaused; // Represent of the time that is paused in the thread.
    public float timeClock; // Heurística.
    public int timeToEdit; // Heurística.

    private point[] puntos; // Points of the respective path to be attended.
    private List<GameObject> cars; // Cars added to the line.
    private bool stateLine; // State of the line. IF there are a thread activated.
    private float timer; // Timer of the possible line thread.
    private float wait; // Count...

    // Use this for initialization
    void Start () {
        cars = new List<GameObject>();
        getAllPoints();
        stateLine = true;
        wait = 0;
        timer = 20; // Por defecto.
	}

    /* Get the respective GameObject of the line. */
    private void getAllPoints()
    {
        string name = "linea" + linea.ToString(); // String of the name of the line.
        GameObject possiblePoints = GameObject.Find(name); // Find the respective gameObject.

        // Get all the children that belongs to this respective gameobject.
        Transform[] children = possiblePoints.GetComponentsInChildren<Transform>(true); 
        puntos = new point[children.Length - 1];
        int i = 0;

        // Add the gameObject to the respective position of the points.
        foreach (Transform item in children)
        {
            if (item.name != name)
            {
                puntos[i] = new point(item.gameObject);
                i++;
            }
        }
    }

    /* Add a new car... */
    public void addElement(GameObject element)
    {
        cars.Add(element);
        element.GetComponent<vehicle>().setObjective(puntos[0].getReferencePoint(), 0); // Get the first point.
    }

    /* Delete the car if was attended. */
    public void deleteElement(GameObject element)
    {
        cars.Remove(element);
        Destroy(element); // Destroy the gameObject.
        setPoint(puntos.Length - 1, null, false); // The point is available to other cars...
    }

    /* Config the point. Position of the point, cars that is going to be located in the point,
     *  and the respective state. (Empty, Occupied) */
    public void setPoint(int position, GameObject content, bool state)
    {
        puntos[position].setContent(content);
        puntos[position].setState(state);
    }

    /* Get the next State of the point... if the next point is free, you can go. */
    public void getStateNext(GameObject content, int position)
    {
        if (position == puntos.Length - 1)
        {
            content.GetComponent<vehicle>().setTime(); // Counter time of the car.
        }else
        {
            int next = position + 1; // Get the next car.

            // If the point is free. Move...
            if (puntos[next].getState() == false)
            {
                setPoint(position, null, false); // The actual point is free.
                setPoint(next, content, true); // The next point is occupied by me.
                content.GetComponent<vehicle>().setObjective(puntos[next].getReferencePoint(), next); // Move the car.
            }
        }
    }

    /* Return the id of the line. */
    public int getLine()
    {
        return linea;
    }

    /* Return the time of the cars that are in the line. */
    public float getTime()
    {
        float count = 0;
        foreach (GameObject element in cars)
        {
            count += element.GetComponent<vehicle>().getTime();
        }
        return count;
    }

    // Update is called once per frame
    void Update () {
        float time = getTime();
        textTime.GetComponent<TextMesh>().text = "Total: " + time + "s";
        textTimePaused.GetComponent<TextMesh>().text = timer.ToString() + "s";

        // If the line is desactivated.
        if (!stateLine)
        {
            // Start the thread...
            if (timer > 0)
            {
                timer -= timeClock;
            }
            else
            {
                stateLine = true;
                timer = wait;
            }
        }
	}

    /* Return if the line is available. */
    public bool isAvailable()
    {
        return this.stateLine;
    }

    /* Start the waiting thread in the line. */
    public void waitTime()
    {
        this.stateLine = false; // Desactivate the line.
        wait = timer;
    }

    /* Stop running the thread. */
    public void stopRunThread()
    {
        this.stateLine = true;
    }

    /* Increase the time to the possible thread. */
    public void increase()
    {
        if (timer < 1000)
        {
            timer += timeToEdit;
        }
    }

    /* Decrease the time to the possible thread. */
    public void decrease()
    {
        if (timer > 40)
        {
            timer -= timeToEdit;
        }
    }
}
