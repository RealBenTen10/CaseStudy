using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] spheres_with_same_tag;
    public int round_number = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeColorOfObject()
    {
        spheres_with_same_tag = GameObject.FindGameObjectsWithTag("spheres_round"+round_number.ToString());
        var sphereRenderer = spheres_with_same_tag[0].GetComponent<Renderer>();
        sphereRenderer.material.SetColor("_Color", Color.green);
    }
}
