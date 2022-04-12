using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using Layout = UnityEditor.EditorGUILayout;

public class DatahandlerWindow : EditorWindow
{
    DataHandler dataHandler;

    [MenuItem("Editors/Datahandler")]
    static void Init()
    {
        DatahandlerWindow window = GetWindow<DatahandlerWindow>();
        window.Show();
    }

    private void OnGUI()
    {
        Layout.LabelField("Datahandler");

        string[] files = System.IO.Directory.GetFiles(Application.persistentDataPath, "*.dat");

        EditorGUI.BeginDisabledGroup(files.Length == 0);
        if (GUILayout.Button("Delete Save Files"))
        {
            for (int i = 0; i < files.Length; i++)
            {
                System.IO.File.Delete(files[i]);
            }

            PlayerPrefs.DeleteAll();
        }
        EditorGUI.EndDisabledGroup();

        if (dataHandler != null)
        {
            //TODO FILL EDITOR WINDOWS

        }
        else
        {
            dataHandler = GameObject.FindObjectOfType<DataHandler>();
        }
    }

}
#endif
