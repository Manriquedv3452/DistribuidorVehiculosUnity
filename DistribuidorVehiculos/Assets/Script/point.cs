using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point{

    private GameObject referencePoint;
    private bool state;
    private GameObject content;

    public point(GameObject point)
    {
        referencePoint = point;
        state = false;
    }

    public void setReferencePoint(GameObject pReferencePoint)
    {
        this.referencePoint = pReferencePoint;
    }

    public void setState(bool pState)
    {
        this.state = pState;
    }

    public void setContent(GameObject pContent)
    {
        this.content = pContent;
    }

    public GameObject getReferencePoint()
    {
        return this.referencePoint;
    }

    public bool getState()
    {
        return this.state;
    }

    public GameObject getContent()
    {
        return this.content;
    }
}
