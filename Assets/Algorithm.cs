using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Text;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine.UI;


public class Algorithm : MonoBehaviour
{
    // change Group_Name here to change group
    private static String group_name = "1";
    private int group_number = 1;
    // static muss benutzt werden, damit die arrays/variablen übergreifend sind "global". Ansonsten pro Object eine Instanz
    private static float[] time = new float[15];
    private static float[] time_2 = new float[14];
    private static int counter = 0;
    private static float time_max = 0;
    // 
    public GameObject Button;
    public GameObject Textfield;
    public GameObject Path_Line;

    public GameObject[] spheres_for_weights = new GameObject[15];
    private static Vector3[] vectors_from_calibration = new Vector3[14];

    public GameObject[] spheres_for_path_1 =  new GameObject[15];
    private static Vector3[,] vectors_for_path_1 = new Vector3[15,15];
    private static float[,] cost_matrix_1 = new float[15, 15];

    public GameObject[] spheres_for_path_2 = new GameObject[15];
    private static Vector3[,] vectors_for_path_2 = new Vector3[15, 15];
    private static float[,] cost_matrix_2 = new float[15, 15];

    public GameObject[] spheres_for_path_3 = new GameObject[15];
    private static Vector3[,] vectors_for_path_3 = new Vector3[15, 15];
    private static float[,] cost_matrix_3 = new float[15, 15];

    // Funktion zum Deaktivieren der Kugeln und setzen der Zeit. Hier eventuell ebenfalls das Logging einbauen
    public void GetTime_SetInactive()
    {
        //spheres_for_weights[counter].SetActive(false);
        
        /* To-Do:
         * Reihenfolge soll eingehalten werden!
         * Falls falsche Kugel angetippt wird, 
         * 
         * 
         */
        if ((GameObject.FindGameObjectsWithTag("spheres_round1"))[0] == gameObject)
        {
            gameObject.SetActive(false);
            time[counter] = Time.time;
            counter++;
            String filePath = Path.Combine(Application.persistentDataPath, "Logfiles_ben.txt");
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine("Group;"+group_name+";Round;1;"+"Type;Kalibrierung"+";Sphere;" + gameObject.name + ";Time;" + Time.time + ";Order;Right;");
            }
            Debug.Log("Group;" + group_name + ";Round;1;" + "Type;Kalibrierung" + ";Sphere;" + gameObject.name + ";Time;" + Time.time + ";Order;Right;");
        }
        else
        {
            (GameObject.FindGameObjectsWithTag("spheres_round1"))[0].GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            String filePath = Path.Combine(Application.persistentDataPath, "Logfiles_ben.txt");
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine("Group;" + group_name + ";Round;1;" + "Type;Kalibrierung" + ";Sphere;" + gameObject.name + ";Time;" + Time.time + ";Order;Wrong;");
            }
        }
        if (GameObject.FindGameObjectsWithTag("spheres_round1").Length <= 0) 
        {
            Path_Line.SetActive(false);
            //Textfield.SetActive(true);
            SetWeights();
        }
    }

    public void SetWeights()
    {
        Button.GetComponent<LineRendererPath>().Set_group_name(group_name);
        Path_Line.GetComponent<LineRendererPath>().Set_group_name(group_name);
        GameObject.FindGameObjectWithTag("CubeParents").GetComponent<SetCubeParentsActiveForButton>().Set_round_order(group_name);
        for (int i = 0; i < time_2.Length; i++)
        {
            if (time_max < time_2[i]) { time_max = time_2[i]; }
        }
        TextMesh setTextOfTextfield = Textfield.GetComponent<TextMesh>();
        //if (GameObject.FindGameObjectsWithTag("spheres_round1").Length > 0) { return; }
        // Vektoren für kalibrierungsrunde berechnen und Zeit berechnen -> es entstehen zwei 14 x 1 Matrizen
        for (int i = 0;i < 14; i++)
        {
            vectors_from_calibration[i] = spheres_for_weights[i+1].transform.position - spheres_for_weights[i].transform.position;
            time_2[i] = time[i+1] - time[i];
        }
        // vektoren für KI Runde berechnen
        for (int i = 0; (i < 15); i++)
        {
            for (int j = 0; j < 15; j++)
            {
                vectors_for_path_1[i, j] = spheres_for_path_1[j].transform.position - spheres_for_path_1[i].transform.position;
            }
        }
        for (int i = 0; (i < 15); i++)
        {
            for (int j = 0; j < 15; j++)
            {
                vectors_for_path_2[i, j] = spheres_for_path_2[j].transform.position - spheres_for_path_2[i].transform.position;
            }
        }
        /* Print Length of vectors 
         Debug.Log("Length of Vectors Round 2:");
                for (int k = 0; k < 15; k++)
                {
                    Debug.Log(vectors_for_path_2[k, 0].sqrMagnitude + " - " + vectors_for_path_2[k, 1].sqrMagnitude + " - " + vectors_for_path_2[k, 2].sqrMagnitude + " - " + vectors_for_path_2[k, 3].sqrMagnitude + " - " + vectors_for_path_2[k, 4].sqrMagnitude + " - " + vectors_for_path_2[k, 5].sqrMagnitude);
                }
        */

        for (int i = 0; (i < 15); i++)
        {
            for (int j = 0; j < 15; j++)
            {
                vectors_for_path_3[i, j] = spheres_for_path_3[j].transform.position - spheres_for_path_3[i].transform.position;
            }
        }
        // Gewichtung berechnen - Variablen 
        float threshold_length = 0.07f;
        float threshold_angle = 17.0f;
        float edge_to_not_take = 0f;
        // 15 x 15 Matrix (Cost-Matrix)
        for (int i = 0; (i < 15); i++)
        {
            // doppelte For-Schleife
            for (int j = 0; j < 15; j++)
            {
                // Falls Kante zu sich selber, Kosten hoch setzen
                if (i == j)
                {
                    cost_matrix_1[i, j] = edge_to_not_take;
                    cost_matrix_2[i, j] = edge_to_not_take;
                    cost_matrix_3[i, j] = edge_to_not_take;
                }
                else
                {


                    // Länge des Vektors zwischen den zwei Knoten (da öfter benötigt, als Variable)
                    float length_temp_1 = vectors_for_path_1[i, j].sqrMagnitude;
                    float length_temp_2 = vectors_for_path_2[i, j].sqrMagnitude;
                    float length_temp_3 = vectors_for_path_3[i, j].sqrMagnitude;



                    // erste KI Gruppe
                    // falls Kante zu lang, nicht betrachten
                    if (length_temp_1 > threshold_length) { cost_matrix_1[i, j] = edge_to_not_take; }
                    else
                    {
                        // counter_temp wird benutzt, um am Ende den Mean zu berechnen. Falls mehr als ein Vektor die BEdingungen erfüllt (Winkel kleiner x Grad und Länge kleiner y)
                        float counter_temp = 0;
                        // Gewicht aufaddieren in weight_temp
                        float weight_temp = 0;
                        // 14 vektoren, da 15 punkte und ende != anfang
                        for (int k = 0; k < 14; k++)
                        {
                            // Winkel berechnen
                            float temp_angle = Vector3.Angle(vectors_for_path_1[i, j], vectors_from_calibration[k]);

                            if (temp_angle < threshold_angle) //threschold = 15f (angle between 0-180)
                            {
                                // Längenunterschied berechnen

                                float temp_length = (length_temp_1 / (vectors_from_calibration[k].sqrMagnitude));

                                // Metric überlegen!!
                                // Vorschlag_1: 1 + winkel durch 10 (winkel zwischen 0 und 20 Grad) mal längenunterschied * zeit der Kalibrierungsrunde
                                weight_temp += (1 + (temp_angle / 50)) * temp_length * time_2[k];
                                counter_temp++;
                            }
                        }
                        // Kosten des Pfades setzen
                        if (counter_temp > 0) { cost_matrix_1[i, j] = weight_temp / counter_temp; }
                        // falls kein passender Winkel gefunden wurde -> langsamste Bewegung
                        else { cost_matrix_1[i, j] = time_max * 1.5f; }
                    }

                    // zweite KI Gruppe
                    
                    if (length_temp_2 > threshold_length) { cost_matrix_2[i, j] = edge_to_not_take; }
                    else
                    {
                        
                        // counter_temp wird benutzt, um am Ende den Mean zu berechnen. Falls mehr als ein Vektor die BEdingungen erfüllt (Winkel kleiner x Grad und Länge kleiner y)
                        float counter_temp = 0;
                        // Gewicht aufaddieren in weight_temp
                        float weight_temp = 0;
                        // 14 vektoren, da 15 punkte und ende != anfang
                        for (int k = 0; k < 14; k++)
                        {
                            // Winkel berechnen
                            float temp_angle = Vector3.Angle(vectors_for_path_2[i, j], vectors_from_calibration[k]);
                            if (temp_angle < threshold_angle)
                            {
                                // Längenunterschied berechnen
                                float temp_length = (length_temp_2 / (vectors_from_calibration[k].sqrMagnitude));

                                // Metric überlegen!!
                                // Vorschlag_1: 1 + winkel durch 10 (winkel zwischen 0 und 20 Grad) mal längenunterschied * zeit der Kalibrierungsrunde
                                weight_temp += (1 + (temp_angle / 100)) * temp_length * time_2[k];
                                counter_temp++;
                            }
                        }
                        // Kosten des Pfades setzen
                        if (counter_temp > 0) { cost_matrix_2[i, j] = weight_temp / counter_temp; }
                        else { cost_matrix_3[i, j] = time_max * 1.5f; }
                    }

                    // dritte KI Gruppe
                    if (length_temp_3 > threshold_length) { cost_matrix_3[i, j] = edge_to_not_take; }
                    else
                    {
                        // counter_temp wird benutzt, um am Ende den Mean zu berechnen. Falls mehr als ein Vektor die BEdingungen erfüllt (Winkel kleiner x Grad und Länge kleiner y)
                        float counter_temp_3 = 0;
                        // Gewicht aufaddieren in weight_temp
                        float weight_temp_3 = 0;
                        // 14 vektoren, da 15 punkte und ende != anfang
                        for (int k = 0; k < 14; k++)
                        {
                            // Winkel berechnen
                            float temp_angle_3 = Vector3.Angle(vectors_for_path_3[i, j], vectors_from_calibration[k]);
                            if (temp_angle_3 < threshold_angle)
                            {
                                // Längenunterschied berechnen
                                float temp_length_3 = (length_temp_3 / (vectors_from_calibration[k].sqrMagnitude));

                                // Metric überlegen!!
                                // Vorschlag_1: 1 + winkel durch 10 (winkel zwischen 0 und 20 Grad) mal längenunterschied * zeit der Kalibrierungsrunde
                                weight_temp_3 += (1 + (temp_angle_3 / 100)) * temp_length_3 * time_2[k];
                                counter_temp_3++;
                            }
                        }
                        // Kosten des Pfades setzen
                        if (counter_temp_3 > 0) { cost_matrix_3[i, j] = weight_temp_3 / counter_temp_3; }
                        else { cost_matrix_3[i, j] = time_max * 1.5f; }

                    }
                }
            }
        }
                
        Debug.Log("Cost_Matrix_1:");
        for (int i = 0; i < 15; i++)
        {
            Debug.Log("| " + cost_matrix_1[i, 0] + " | " + cost_matrix_1[i, 1] + " | " + cost_matrix_1[i, 2] + " | " + cost_matrix_1[i, 3] + " |" + cost_matrix_1[i, 4] + " |" + cost_matrix_1[i, 5] + " |" + cost_matrix_1[i, 6] + " |" + cost_matrix_1[i, 7] + " |" + cost_matrix_1[i, 8] + " |" + cost_matrix_1[i, 9] + " |" + cost_matrix_1[i, 10] + " |" + cost_matrix_1[i, 11] + " |" + cost_matrix_1[i, 12] + " |" + cost_matrix_1[i, 13] + " |" + cost_matrix_1[i, 14] + " |");
        }
        Debug.Log("Cost_Matrix_2:");
        for (int i = 0; i < 15; i++)
        {
            Debug.Log("| " + cost_matrix_2[i, 0] + " | " + cost_matrix_2[i, 1] + " | " + cost_matrix_2[i, 2] + " | " + cost_matrix_2[i, 3] + " |" + cost_matrix_2[i, 4] + " |" + cost_matrix_2[i, 5] + " |" + cost_matrix_2[i, 6] + " |" + cost_matrix_2[i, 7] + " |" + cost_matrix_2[i, 8] + " |" + cost_matrix_2[i, 9] + " |" + cost_matrix_2[i, 10] + " |" + cost_matrix_2[i, 11] + " |" + cost_matrix_2[i, 12] + " |" + cost_matrix_2[i, 13] + " |" + cost_matrix_2[i, 14] + " |");
        }
        Debug.Log("Cost_Matrix_3:");
        for (int i = 0; i < 15; i++)
        {
            Debug.Log("| " + cost_matrix_3[i, 0] + " | " + cost_matrix_3[i, 1] + " | " + cost_matrix_3[i, 2] + " | " + cost_matrix_3[i, 3] + " |" + cost_matrix_3[i, 4] + " |" + cost_matrix_3[i, 5] + " |" + cost_matrix_3[i, 6] + " |" + cost_matrix_3[i, 7] + " |" + cost_matrix_3[i, 8] + " |" + cost_matrix_3[i, 9] + " |" + cost_matrix_3[i, 10] + " |" + cost_matrix_3[i, 11] + " |" + cost_matrix_3[i, 12] + " |" + cost_matrix_3[i, 13] + " |" + cost_matrix_3[i, 14] + " |");
        }
        /*
        float[,] cost_matrix_test = new float[,]
        {
            { -1f, -1f, -1f, -1f, 0.4f},
            { -1f, -1f, 0.1f, 0.3f, 0.4f},
            { -1f, 0.4f, -1f, 0.1f, 0.2f},
            { 0.2f, 0.1f, 0.4f, -1f, 0.1f},
            { -1f, 0.1f, 0.1f, 0.5f, -1f}
        };
        */

        // Set Graphs (AdjacencyMatrix) and compute best round (calibration round)
        Graph graph_1 = new Graph(cost_matrix_1);
        int[] bestPath_1 = graph_1.FindBestPath();
                        
        Graph graph_2 = new Graph(cost_matrix_2);
        int[] bestPath_2 = graph_2.FindBestPath();

        Graph graph_3 = new Graph(cost_matrix_3);
        int[] bestPath_3 = graph_3.FindBestPath();
        
       
        //int[] bestPath_1 = new int[15] { 1, 3, 2, 4, 5, 7, 6, 8, 9, 10, 12, 11, 13, 14, 15 };
        //int[] bestPath_2 = new int[15] { 1, 2, 4, 3, 5, 6, 8, 7, 10, 9, 11, 12, 13, 15, 14 };
        //int[] bestPath_3 = new int[15] { 2, 3, 1, 5, 4, 7, 6, 9, 8, 10, 12, 11, 15, 14, 13 };

        

        // Positionen der Kugeln speichern (in der Reihenfolge des neuen Pfades)
        
        Vector3[] save_positions_of_spheres_1 = new Vector3[15];
        for (int i = 0; i < 15; i++)
        {        
            save_positions_of_spheres_1[i] = spheres_for_path_1[(bestPath_1[i])].transform.position;
        }        
        Vector3[] save_positions_of_spheres_2 = new Vector3[15];
        for (int i = 0; i < 15; i++)
        {
            save_positions_of_spheres_2[i] = spheres_for_path_2[(bestPath_2[i])].transform.position;
        }
        Vector3[] save_positions_of_spheres_3 = new Vector3[15];
        for (int i = 0; i < 15; i++)
        {
            save_positions_of_spheres_3[i] = spheres_for_path_3[(bestPath_3[i])].transform.position;
        }
        
        // Positionen der Kugeln anpassen
        for (int i = 0; i < 15; i++)
        {
            spheres_for_path_1[i].transform.position = save_positions_of_spheres_1[i];
            spheres_for_path_2[i].transform.position = save_positions_of_spheres_2[i];
            spheres_for_path_3[i].transform.position = save_positions_of_spheres_3[i];
        }

        Debug.Log("Length of path 1: ");
        for (int i = 0; i < 14; i++)
        {
            float temp_length = (spheres_for_path_1[i+1].transform.position - spheres_for_path_1[i].transform.position).sqrMagnitude;
            Debug.Log(temp_length);
        }

        Debug.Log("Length of path 2: ");
        for (int i = 0; i < 14; i++)
        {
            float temp_length = (spheres_for_path_2[i + 1].transform.position - spheres_for_path_2[i].transform.position).sqrMagnitude;
            Debug.Log(temp_length);
        }

        Debug.Log("Length of path 3: ");
        for (int i = 0; i < 14; i++)
        {
            float temp_length = (spheres_for_path_3[i + 1].transform.position - spheres_for_path_3[i].transform.position).sqrMagnitude;
            Debug.Log(temp_length);
        }


        // Textfeld ausblenden und Button aktivieren
        /*
        Textfield.SetActive(false);
        Button.SetActive(true);
        ButtonConfigHelper buttonConfigHelper = Button.GetComponent<ButtonConfigHelper>();
        buttonConfigHelper.MainLabelText = "Runde 1";
        */
    }

    

}
