using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;

public class ObjectDisappear : MonoBehaviour
{
    private static int counter = 5;
    public GameObject Button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.SetActive(false);
    }
    public void test()
    {
        gameObject.SetActive(false);
        counter--;
        if (counter == 0)
        {
            Button.SetActive(true);
        }
    }
}
