using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public long lastUpdated;

    //the values defined here will be the default values
    public SettingsData()
    {

    }

    //you can incorporate TMPro elements by refering them to here and it will update in the SaveSlot.cs class
}
