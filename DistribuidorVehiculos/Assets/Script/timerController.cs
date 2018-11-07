using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerController : MonoBehaviour {

    public string type;
    public GameObject lineObject;
    private lines line;

	// Use this for initialization
	void Start () {
        line = lineObject.GetComponent<lines>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        Debug.Log("Detecto el click");
        if (type == "INCREASE")
        {
            line.increase();
        }else
        {
            line.decrease();
        }
    }
}
