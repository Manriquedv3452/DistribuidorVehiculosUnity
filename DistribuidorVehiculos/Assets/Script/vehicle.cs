using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicle : MonoBehaviour {

    public float timeSpeed;
    public float timeClock;
    public float range;
    public GameObject textID;
    public GameObject textTime;
    public float speed;
    public float time;
    public char type;

    private static int cont;
    private float waitTime;
    private float timeExpected;
    private int id;
    private lines actualLine;
    private int actualPosition;
    private bool isMove;
    private bool advance;
    private Vector3 destination;
    private bool attend;

	// Use this for initialization
	void Start () {
        cont++;
        this.id = cont;
        textID.GetComponent<TextMesh>().text = "ID: " + this.id.ToString();
        textTime.GetComponent<TextMesh>().text = "Time: " + time.ToString();
        isMove = false;
        advance = false;
        attend = false;
        actualLine = null;
        actualPosition = -1;
        waitTime = 0;
        timeExpected = 150;
	}
	
	// Update is called once per frame
	void Update () {
		if (isMove)
        {
            move();
        }else
        {
            if (advance)
            {
                // solicite si el siguiente lugar está disponible.
                if (actualLine)
                {
                    if (waitTime < timeExpected)
                    {
                        waitTime += timeSpeed;
                    }
                    else
                    {
                        actualLine.getStateNext(gameObject, actualPosition);
                    }
                }
            }
        }

        startTime(); // Review if the time can be reduced.
	}

    private void startTime()
    {
        if (attend && actualLine != null)
        {
            if (actualLine.isAvailable())
            {
                if (time > 0)
                {
                    time -= timeClock;
                    textTime.GetComponent<TextMesh>().text = "Time: " + time.ToString();
                }
                else
                {
                    actualLine.deleteElement(gameObject);
                }
            }
        }
    }

    private void move()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;

        bool firstCondition = (destination.x + range) > transform.position.x && transform.position.x > (destination.x - range);
        bool secondCondition = (destination.y + range) > transform.position.y && (destination.y - range) < transform.position.y;

        if (firstCondition && secondCondition)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            isMove = false;
            advance = true;
            waitTime = 0;
        }else
        {
            if (firstCondition && !secondCondition)
            {
                if (destination.y - range > transform.position.y)
                {
                    posY += speed;
                }

                if (destination.y + range < transform.position.y)
                {
                    posY -= speed;
                }
            }

            if (!firstCondition && secondCondition)
            {
                if (destination.x - range > transform.position.x)
                {
                    posX += speed;
                }

                if (destination.x + range < transform.position.x)
                {
                    posX -= speed;
                }
            }

            if (!firstCondition && !secondCondition)
            {
                if (destination.x - range > transform.position.x)
                {
                    posX += speed;
                }

                if (destination.x + range < transform.position.x)
                {
                    posX -= speed;
                }

                if (destination.y - range > transform.position.y)
                {
                    posY += speed;
                }

                if (destination.y + range < transform.position.y)
                {
                    posY -= speed;
                }
            }

            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }

    public void setObjective(GameObject objective, int position)
    {
        destination = new Vector3(objective.transform.position.x, objective.transform.position.y, transform.position.z);
        isMove = true;
        actualPosition = position;
    }

    public int getPosition()
    {
        return this.actualPosition;
    }

    public float getTime()
    {
        return this.time;
    }

    public void setActualLine(lines line)
    {
        this.actualLine = line;
    }

    public void setTime()
    {
        this.attend = true;
    }

    public void activeTime()
    {
        this.attend = true;
    }

    public void desactivateTime()
    {
        this.attend = false;
    }

    public int getID()
    {
        return this.id;
    }

    public char getType()
    {
        return this.type;
    }
}
