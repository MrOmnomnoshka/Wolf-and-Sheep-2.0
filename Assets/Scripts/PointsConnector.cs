using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsConnector : MonoBehaviour
{
    public GameObject lineRendererObj;

    private LineRenderer lineRenderer;
    private GameObject[] linesObjects;
    public GameObject[] connectedPoints;
    private int[] connectedIndexes; 

    private int maxDistance = 3;
    //private int minDistance = 1;

    // Start is called before the first frame update
    void Start()
    {
        var pointsAmount = GameObject.FindGameObjectsWithTag("PointTag").Length;
        linesObjects = new GameObject[pointsAmount];
        for (int i = 0; i < pointsAmount; i++)
        {
            var tempObject = Instantiate(lineRendererObj);
            linesObjects[i] = tempObject;
        }
    }


    void Update()
    {
        var points = GameObject.FindGameObjectsWithTag("PointTag");

        connectedIndexes = new int[points.Length];  // array for neighborhoods
        int indexesCounter = 0;

        for (int i = 0; i < points.Length; i++)
        {
            indexesCounter = 0;
            float distance;  // distance to the other point

            lineRenderer = linesObjects[i].GetComponent<LineRenderer>();
            lineRenderer.positionCount = 0;
            for (int j = 0; j < points.Length; j++)
            {
                if (i != j)
                {
                    distance = Vector2.Distance(points[i].transform.position, points[j].transform.position);
                    if (distance < maxDistance)
                    {
                        // attach to other point
                        lineRenderer.positionCount++;
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1, points[j].transform.position);
                        connectedIndexes[indexesCounter] = j;
                        indexesCounter++;

                        // and go back to self
                        lineRenderer.positionCount++;
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1, points[i].transform.position);
                    }
                }
            }
            
            PointParms pp = points[i].GetComponent<PointParms>();
            
            pp.connectedPoints = new GameObject[indexesCounter];
            for (int k = 0; k < indexesCounter; k++)
            {
                pp.connectedPoints[k] = points[connectedIndexes[k]]; // TODO: Fix it
            }
        }
    }
}
