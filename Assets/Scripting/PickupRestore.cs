using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRestore : MonoBehaviour
{
    private GameObject[] 
    private Timer aTimer;
    private GameObject prefab;

    public void StartRestoreTimer(double restoreSeconds, GameObject obj)
    {
        prefab = obj;
        aTimer = new Timer(restoreSeconds * 1000);
        aTimer.Elapsed += RestorePickup;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
    }

    private void RestorePickup(System.Object source, ElapsedEventArgs e)
    {
        aTimer.Stop();
        aTimer.Dispose();
    }
}
