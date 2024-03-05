using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCubeParentsActiveForButton : MonoBehaviour
{
    
    public int round_number = 0;
    private int[] round_array = new int[10] { 1,2,3,4,5,6,7,8,9,10 };
    public GameObject[] parent_to_set_active = new GameObject[10];
    public GameObject Textfield;
    public void Set_parent_active()
    {
        if (round_number < 10)
        {
            parent_to_set_active[round_array[round_number]-1].SetActive(true);
            round_number++;
        }
        else
        {
            Textfield.SetActive(true);
            TextMesh setTextOfTextfield = Textfield.GetComponent<TextMesh>();
            setTextOfTextfield.text = "Vielen Dank für die Teilnahme!";
        }
       
    }

    public void Set_round_order(string group_name) 
    {
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
    }
}
