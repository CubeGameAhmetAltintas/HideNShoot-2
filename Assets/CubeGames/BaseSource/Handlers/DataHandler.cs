using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataHandler : HandlerBaseModel
{
    public SettingDataModel Setting;
    public PlayerDataModel Player;

    public override void Initialize()
    {
        base.Initialize();

        Setting = new SettingDataModel().Load();
        Player = new PlayerDataModel().Load();
    }

    public static void ClearAllData()
    {
        string[] files = System.IO.Directory.GetFiles(Application.persistentDataPath, "*.dat");
        for (int i = 0; i < files.Length; i++)
        {
            System.IO.File.Delete(files[i]);
        }

        PlayerPrefs.DeleteAll();
    }

}