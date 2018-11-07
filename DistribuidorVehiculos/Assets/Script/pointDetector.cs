using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointDetector : MonoBehaviour {

    public int position;
    public GameObject line;
    private lines codeLine;

	// Use this for initialization
	void Start () {
        codeLine = line.GetComponent<lines>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        Debug.Log("Si detecta la entrada de un objeto");
        if(collision.gameObject.tag == "vehicle")
        {
            Debug.Log("Entro el vehiculo");
            codeLine.setPoint(position, collision.gameObject, true);
        }
        */
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        /*
        if (collision.gameObject.tag == "vehicle")
        {
            Debug.Log("Salió el vehículo");
            codeLine.setPoint(position, collision.gameObject, false);
        }
        */
    }
}
