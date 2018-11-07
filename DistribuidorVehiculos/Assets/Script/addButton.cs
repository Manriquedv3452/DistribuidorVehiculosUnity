using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addButton : MonoBehaviour {

    public GameObject objectCreate;
    private waitRoom salaEspera;

	// Use this for initialization
	void Start () {
        GameObject sala = GameObject.Find("SalaEspera");
        salaEspera = sala.GetComponent<waitRoom>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    // When is pressed.
    void OnMouseDown()
    {
        if (salaEspera.thereIsAvailable())
        {
            GameObject nuevo = Instantiate(objectCreate, transform.position, transform.rotation) as GameObject;
            salaEspera.addCar(nuevo);
        }else
        {
            Debug.Log("Esta lleno");
        }
        
    }
}
