using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface SettingsDataPersistence
{
    void LoadData(SettingsData data);

    void SaveData(SettingsData data);
}

