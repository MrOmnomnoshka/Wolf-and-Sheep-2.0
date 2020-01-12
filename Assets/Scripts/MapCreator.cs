using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCreator : MonoBehaviour
{
    public int pointsAmount = 50;
    public GameObject pointPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
        CheckConnections();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void GenerateMap()
    {
        for (int i = 0; i < pointsAmount; i++)
        {
            var pointObj = Instantiate(pointPrefab);
            pointObj.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.0f, 4.0f), 0);
        }
    }

    void CheckConnections()
    {
        var lines = GameObject.FindGameObjectsWithTag("LineTag");

        foreach (GameObject line in lines)
        {
            if (line.GetComponent<LineRenderer>().positionCount == 0)  // if all have pairs
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
