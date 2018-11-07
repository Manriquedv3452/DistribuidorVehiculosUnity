using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numbers : MonoBehaviour {

    public float speed;
    public int value;
    private bool actived;
    private int distanceToWalk;
    private bool straight;
    private float finalPosition;
    private float startPosition;

	// Use this for initialization
	void Start () {
        straight = true;
        actived = false;
        distanceToWalk = 2;
        finalPosition = transform.position.y + distanceToWalk;
        startPosition = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (actived)
        {
            move();
        }
	}


    private void move()
    {
        // Move straight to up.
        if (straight)
        {
            float actualPosition = transform.position.y;
            if (actualPosition < finalPosition)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
            }
            else
            {
                actived = false;
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        }
        // Move back to come back to the start place.
        else 
        {
            float actualPosition = transform.position.y;
            if (actualPosition > startPosition)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
            }
            else
            {
                actived = false;
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                straight = true;
            }
        }
    }

    // When your press the mouse in the object.
    void OnMouseDown()
    {
        // Review if the controller is ok.
        GameObject numbersController = GameObject.Find("numeros");
        numbersController controller = numbersController.GetComponent<numbersController>();

        if (controller.solicitateActive(gameObject))
        {
            actived = true;
        }
    }

    public void goBack()
    {
        straight = false;
        actived = true;
    }

    public int getValue()
    {
        return value;
    }

}
