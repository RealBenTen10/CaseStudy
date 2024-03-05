using Microsoft.MixedReality.Toolkit.UI;
using System;
//using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using UnityEngine.UIElements;
//using UnityEditor.PackageManager;

public class LineRendererPath : MonoBehaviour
{
    // Change group_name in "Algorithm.cs" to change group
    private static String group_name;
    private static String round_type = "Kalibrierung";
    // Anpassen der Werte über das gameObject! Wenn hier angepasst wird, muss das Script neu hinzugefügt werden!
    // Button for each round - wird noch durch Text ersetzt (eventuell)
    public GameObject Button;
    // Platzhalter für Text am Ende - wird noch ergänzt
    public String Text_at_end = "Danke für die Teilnahme";
    public GameObject Textfield_at_end;
    public GameObject Textfield_between;
    // Array der Reihenfolge der Runden - im GameObject anpassen!! Path_Line 
    // Anpassen für verschiedene Reihenfolgen!! Im GameObjekt Path_Line und im GameObjekt SetCubeParentsActive
    public static int[] round_array = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    // counter der runden-nummer. greift auf round_array zu
    public static int round_number = 0;
    // zweiter counter, für wschleifen
    private int counter = 0;
    // Array aller Spheren der aktuellen Runde
    public static GameObject[] spheres_with_same_tag;
    // wichtig für Klasse - nicht entfernen
    public LineRenderer lineRenderer;
    // Start is called before the first frame update
    
    public void render_path()
    {        
        spheres_with_same_tag = GameObject.FindGameObjectsWithTag("spheres_round"+round_array[(round_number-1)].ToString());
        
        counter = 0;
        lineRenderer.positionCount = spheres_with_same_tag.Length;
        //Debug.Log("Group;" + group_number + ";Round;" + round_number + ";Sphere;" + spheres_with_same_tag[0].name + ";Time;" + Time.time);
        for (int i = 0; i < spheres_with_same_tag.Length;i++)
        {
            lineRenderer.SetPosition(counter, spheres_with_same_tag[i].transform.position);
            counter++;        
        }
        if (spheres_with_same_tag.Length == 1)
        {
            if (round_number < 10)
            {
                Button.SetActive(true);
                ButtonConfigHelper buttonConfigHelper = Button.GetComponent<ButtonConfigHelper>();
                buttonConfigHelper.MainLabelText = "Runde " + round_number;
                //GameObject.Find("PressableButtonHoloLens2").GetComponentInChildren<Text>().text = "Round " + round_number;
            }
            else
            {
                Textfield_at_end.GetComponent<TextfieldScript>().End();
                Textfield_at_end.SetActive(true);
                TextMesh setTextOfTextfield = Textfield_at_end.GetComponent<TextMesh>();
                setTextOfTextfield.text = "Vielen Dank für die Teilnahme!";
            }
            
        }
    }
    public void render_path_only_color()
    {        
        spheres_with_same_tag = GameObject.FindGameObjectsWithTag("spheres_round"+round_array[(round_number-1)].ToString());
        var sphereRenderer = spheres_with_same_tag[0].GetComponent<Renderer>();
        sphereRenderer.material.SetColor("_Color", Color.green);
     }

    public void activate_line_renderer()
    {
        // Gruppe auswählen und in SetCubeParentsActive Reihenfolge anpassen!
        //Group 1
        if (group_name == "1") { round_array = new int[10] { 1, 2, 6, 9, 5, 3, 8, 7, 10, 4 }; }
        //Group 2 - Group 6
        if (group_name == "2") { round_array = new int[10] { 1, 4, 8, 3, 5, 9, 7, 2, 6, 10 }; }
        //Group 3
        if (group_name == "3") { round_array = new int[10] { 1, 9, 2, 5, 7, 8, 3, 10, 4, 6 }; }
        //Group 4
        if (group_name == "4") { round_array = new int[10] { 1, 7, 2, 6, 8, 4, 10, 3, 5, 9 }; }
        //Group 5
        if (group_name == "5") { round_array = new int[10] { 1, 6, 8, 7, 2, 4, 9, 5, 3, 10 }; }
        //Group 6 - Group 2
        if (group_name == "6") { round_array = new int[10] { 1, 4, 8, 3, 5, 9, 7, 2, 6, 10 }; }
        gameObject.SetActive(true);
        lineRenderer = GetComponent<LineRenderer>();
        round_number++;
        Debug.Log("Round: "+round_array[(round_number - 1)]);
        if (round_number < 11)
        {
            render_path_only_color();
            render_path();
        }
        
    }

    public void deactivate_object()
    {
        
        spheres_with_same_tag = GameObject.FindGameObjectsWithTag("spheres_round" + round_array[(round_number - 1)].ToString());

        String filePath = Path.Combine(Application.persistentDataPath, "Logfiles_ben.txt");
        if (spheres_with_same_tag[0] != gameObject)
        {
            var sphereRenderer = spheres_with_same_tag[0].GetComponent<Renderer>();
            sphereRenderer.material.SetColor("_Color", Color.green);
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine("Group;" + group_name + ";Round;" + round_array[round_number-1] + ";Type;"+ round_type + ";Sphere;" + gameObject.name + ";Time;" + Time.time + ";Order;Wrong;");
            }
        }
        else
        {
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine("Group;" + group_name + ";Round;" + round_array[round_number-1] + ";Type;"+ round_type + ";Sphere;" + gameObject.name + ";Time;" + Time.time + ";Order;Right;");
            } 
            Debug.Log("Group;" + group_name + ";Round;" + round_array[round_number-1] + ";Type;" + round_type + ";Sphere;" + gameObject.name + ";Time;" + Time.time + ";Order;Right;");
        }
        gameObject.SetActive(false);                              
    }

    public void deactivate_button()
    {
        // Gruppe auswählen und in SetCubeParentsActive Reihenfolge anpassen!
        //Group 1
        if (group_name == "1") { round_array = new int[10] { 1, 2, 6, 9, 5, 3, 8, 7, 10, 4 }; }
        //Group 2 - Group 6
        if (group_name == "2") { round_array = new int[10] { 1, 4, 8, 3, 5, 9, 7, 2, 6, 10 }; }
        //Group 3
        if (group_name == "3") { round_array = new int[10] { 1, 9, 2, 5, 7, 8, 3, 10, 4, 6 }; }
        //Group 4
        if (group_name == "4") { round_array = new int[10] { 1, 7, 2, 6, 8, 4, 10, 3, 5, 9 }; }
        //Group 5
        if (group_name == "5") { round_array = new int[10] { 1, 6, 8, 7, 2, 4, 9, 5, 3, 10 }; }
        //Group 6 - Group 2
        if (group_name == "6") { round_array = new int[10] { 1, 4, 8, 3, 5, 9, 7, 2, 6, 10 }; }
        gameObject.SetActive(false);
        String filePath = Path.Combine(Application.persistentDataPath, "Logfiles_ben.txt");        
        Textfield_at_end.GetComponent<TextfieldScript>().Set_time();
        Textfield_at_end.SetActive(true);
        TextMesh setTextOfTextfield = Textfield_at_end.GetComponent<TextMesh>();

        // Anpassen für andere Zuordnung
        if (group_name == "1" || group_name == "2" || group_name == "3")
        {
            if (round_array[(round_number)] < 8)
            {
                if (round_array[(round_number)] < 5)
                {
                    setTextOfTextfield.text = "KI unterstützte Runde"; round_type = "KI";
                }
                else
                {
                    //setTextOfTextfield.text = "Normale Runde"; round_type = "Normal";
                    setTextOfTextfield.text = "KI unterstützte Runde"; round_type = "Placebo";
                }

            }
            else
            {
                //setTextOfTextfield.text = "KI unterstützte Runde"; round_type = "Placebo";
                setTextOfTextfield.text = "Normale Runde"; round_type = "Normal";
            }
        }
        else
        {
            if (round_array[(round_number)] < 8) 
            { 
                if (round_array[(round_number)] < 5)
                {
                    setTextOfTextfield.text = "KI unterstützte Runde"; round_type = "KI";
                }
                else
                {
                    setTextOfTextfield.text = "Normale Runde"; round_type = "Normal";
                    //setTextOfTextfield.text = "KI unterstützte Runde"; round_type = "Placebo";
                }
                
            }
            else 
            { 
                setTextOfTextfield.text = "KI unterstützte Runde"; round_type = "Placebo";
                //setTextOfTextfield.text = "Normale Runde"; round_type = "Normal";
            }
        }
        
        if ((round_number) == 0) { setTextOfTextfield.text = "Kalibrierungsrunde"; round_type = "Kalibrierung"; }
        
        using (StreamWriter writer = File.AppendText(filePath))
        {
            writer.WriteLine("Group;" + group_name + ";Round;" + round_array[round_number] + ";Type;"+ round_type + ";Sphere;" + gameObject.name + ";Time;" + Time.time + ";Order;Right;");
        }

        Debug.Log("Group;" + group_name + ";Round;" + round_array[round_number] + ";Type;" +round_type+ ";Sphere;" + gameObject.name + ";Time;" + Time.time + ";Order;Right;");
    }

    public void activate_text_between_button_and_spheres()
    {
        Textfield_at_end.SetActive(true);
        TextMesh setTextOfTextfield = Textfield_at_end.GetComponent<TextMesh>();
        if (round_array[(round_number - 1)] < 8) { setTextOfTextfield.text = "KI unterstützte Runde"; }
        else { setTextOfTextfield.text = "normale Runde"; }
        
    }

    public void button_text_calibration()
    {
        
        Textfield_between.SetActive(true);
        Textfield_between.GetComponent<TextfieldScript>().Set_time();
        TextMesh setTextOfTextfield = Textfield_between.GetComponent<TextMesh>();
        setTextOfTextfield.text = "Die Pfade werden berechnet. Bitte warten";
        gameObject.SetActive(false);
    }

    public void Set_group_name(String group_namestring)
    {
        group_name = group_namestring;
    }
}
