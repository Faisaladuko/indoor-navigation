using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSG.Android.Wifi;


public class RSSI : MonoBehaviour
{
    public List<string> listName = new List<string>() { };
    public string Names;
    private void Start()
    {
        // Start the scan
        StartCoroutine(PrintWifiNetworks());
    }

    private IEnumerator PrintWifiNetworks()
    {
        // Check if wifi is enabled
        if (AndroidWifiManager.IsWifiEnabled() == false)
        {
            // If not, enable it
            AndroidWifiManager.SetWifiEnabled(true);
            // Give the device time to enable wifi
            yield return new WaitForSeconds(1);
        }

        // Initiate a scan (not always needed)
        AndroidWifiManager.StartScan();

        // Wait for the scan to complete
        yield return new WaitForSeconds(1);

        // Get the list of scan results
        var results = AndroidWifiManager.GetScanResults();
        foreach (AndroidWifiScanResults result in results)
        {
            
            Names = string.Format("SSID: {0} Signal: {1}dBm Security Type: {2}", result.SSID, result.level, result.securityType);
            listName.Add(Names);
             //Debug.LogFormat(listName);
        }
        
    }

    
}
