using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changePositionOfCube()
    {
        Transform cubeTransform = transform;
        cubeTransform.position = new Vector3(0f,2f,0f);
    }
}
