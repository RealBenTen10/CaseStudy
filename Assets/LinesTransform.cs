using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesTransform : MonoBehaviour
{

    public Transform startCube;
    public Transform endCube;
    public LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startCube.position);
        lineRenderer.SetPosition(1, endCube.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
