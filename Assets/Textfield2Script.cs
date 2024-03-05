using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Textfield2Script : MonoBehaviour
{
    private float temp_time;
    public GameObject Button;
    public GameObject Sphere;

    // Start is called before the first frame update
    void Start()
    {

        temp_time = Time.time + 3f;


    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > (temp_time - 1.5f))
        {
            TextMesh setTextOfTextfield = gameObject.GetComponent<TextMesh>();
            setTextOfTextfield.text = "Die Pfade werden gesetzt...";
        }

        if (Time.time > temp_time)
        {
            gameObject.SetActive(false);
            Button.SetActive(true);
            ButtonConfigHelper buttonConfigHelper = Button.GetComponent<ButtonConfigHelper>();
            buttonConfigHelper.MainLabelText = "Runde 1";
        }
    }

    public void Setactive()
    {
        if (GameObject.FindGameObjectsWithTag("spheres_round1").Length <= 1)
        {
            gameObject.SetActive(true);
        }
        
    }
}
