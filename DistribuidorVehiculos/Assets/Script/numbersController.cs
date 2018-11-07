using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numbersController : MonoBehaviour {

    private GameObject[] numbers;
    private bool[] stateNumbers;
    private GameObject chosen;

	// Use this for initialization
	void Start () {
        int i = 0;
        Transform[] children = gameObject.GetComponentsInChildren<Transform>(true);
        numbers = new GameObject[children.Length - 1];
        stateNumbers = new bool[numbers.Length];

        foreach (Transform item in children)
        {
            if (item.name != "numeros")
            {
                numbers[i] = item.gameObject;
                stateNumbers[i] = false;
                i++;
            }
        }
        chosen = numbers[4];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Process all the numbers in the game.
    public bool solicitateActive(GameObject requestObject)
    {
        for(int i = 0; i < numbers.Length; i++)
        {
            GameObject actual = numbers[i];

            if (actual.name == requestObject.name)
            {
                for(int k = 0; k < stateNumbers.Length; k++)
                {
                    bool possible = stateNumbers[k];
                    if (possible == true)
                    {
                        numbers[k].GetComponent<numbers>().goBack(); // Devuelva el objeto.
                        stateNumbers[k] = false;
                    }
                }
                stateNumbers[i] = true;
                chosen = numbers[i]; // The number chosen to select.
                Debug.Log("El elegido es: " + chosen.name);
                return true;
            }
        }
        return false;
    }

    public GameObject getChosen()
    {
        return chosen;
    }
}
