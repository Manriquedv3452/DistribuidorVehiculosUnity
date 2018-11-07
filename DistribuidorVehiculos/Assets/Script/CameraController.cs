using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private bool lastScreen;
    public float speed;
    private float limitDown;
    private float limitLeft;
    private float limitRight;
    private float limitUp;

    private int firstScreenDistance;
    private int finalScreenDistance;

	// Use this for initialization
	void Start () {
        lastScreen = false;
        firstScreenDistance = 57;
        finalScreenDistance = 75;
	}
	
	// Update is called once per frame
	void Update () {
		if (lastScreen)
        {
            moveCamera();
        }
	}

    private void moveCamera()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > limitLeft)
            {
                transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            }else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < limitRight)
            {
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.position.y > limitDown)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.position.y < limitUp)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        }
    }

    public void changeLastScreen()
    {
        expandableScreen(); // Let to the user administrate the camera.
        limitDown = transform.position.y;
        limitUp = transform.position.y + 30;
        limitLeft = transform.position.x;
        limitRight = transform.position.x + 82;
    }

    public void expandableScreen()
    {
        lastScreen = true;
    }

    public void changeFirstScreen()
    {
        lastScreen = false;
    }

    public void camaraMuevete(string Movement, bool finalScreen)
    {
        float posX = transform.position.x;
        float posY = transform.position.y;

        if (Movement == "NEXT")
        {
            if (finalScreen)
            {
                posX += finalScreenDistance;
            }
            else
            {
                posX += firstScreenDistance;
            }
        }

        if (Movement == "BACK")
        {
            if (finalScreen)
            {
                posX -= finalScreenDistance;
            }
            else
            {
                posX -= firstScreenDistance;
            }
        }

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
