using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadScreen : MonoBehaviour
{
    TextMesh setTextOfTextfield;
    // Start is called before the first frame update
    void Start()
    {
        setTextOfTextfield = gameObject.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        setTextOfTextfield.text = "Die Gewichte werden berechnet...";
    }
}
