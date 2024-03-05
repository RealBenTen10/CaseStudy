using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TextfieldScript : MonoBehaviour
{
    private float temp_time;
    public GameObject Parent_to_set_active;
    public GameObject path_line;

    // Start is called before the first frame update
    void Start()
    {
        
        temp_time = Time.time + 3f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Time.time > temp_time) 
        { 
            gameObject.SetActive(false); 
            Parent_to_set_active.GetComponent<SetCubeParentsActiveForButton>().Set_parent_active(); 
            path_line.GetComponent<LineRendererPath>().activate_line_renderer(); 
        }
    }

    public void Set_time()
    {
        temp_time = Time.time+3f;
    }

    public void End()
    {
        temp_time = Time.time + 100f;
    }
}
