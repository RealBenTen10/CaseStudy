using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathColorAndLine : MonoBehaviour
{
    public int round_number = 1;
    //private int counter = 0;
    //private bool first_click;
    //private static int spheres_per_Scene = 10;
    //private int counter_spheres_left = spheres_per_Scene;
    //public Transform[] allSpheres = new Transform[spheres_per_Scene];
    public GameObject[] spheres_with_same_tag;
    public LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        render_path_with_color();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void render_path_with_color()
    {
        
        spheres_with_same_tag = GameObject.FindGameObjectsWithTag("spheres_round"+round_number.ToString());
        if (spheres_with_same_tag.Length >= 1)
        {
            var sphereRenderer = spheres_with_same_tag[1].GetComponent<Renderer>();
            sphereRenderer.material.SetColor("_Color", Color.green);
        }
        else
        {
            
        }
    }
}
