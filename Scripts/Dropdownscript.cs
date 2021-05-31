using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dropdownscript : MonoBehaviour
{
    public RSSI rssi;
    public Dropdown dropdown;
    public Text SelectedItem;
    private string SSID;


    public void dropdownSelect(int indexy)
    {
        SelectedItem.text = rssi.listName[indexy];
        if (indexy == 0)
        {
            SelectedItem.color = Color.red;
        }
        else
        {
            SelectedItem.color = Color.white;
        }

    }
    /* public List<string> listN = new List<string>() {"ONE","TWO" };
     void populateList()
     {

         dropdown.options.Clear();
         dropdown.AddOptions(listN);

     }
    */
    void Start()
    {
        //populateList();
        
        dropdown.options.Clear();
        dropdown.AddOptions(rssi.listName);
     }
    


    void Update()
    {
        foreach (var x in rssi.listName)
        {
            SSID = x.ToString();
          
            Debug.Log(SSID);
        }
        dropdown.RefreshShownValue();

    }
}
