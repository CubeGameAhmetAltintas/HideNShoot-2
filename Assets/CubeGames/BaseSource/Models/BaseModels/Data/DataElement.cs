using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataElement
{
    public string Key;
    public string Value;

    public DataElement()
    {

    }

    public DataElement(string key, string value)
    {
        Key = key;
        Value = value;
    }
}

[System.Serializable]
public class DataListElement
{
    public string Key;
    public List<DataElement> Values;

    public DataListElement(string key)
    {
        Key = key;
        Values = new List<DataElement>();
    }

    public DataElement AddOrUpdateValue(string key, string value)
    {
        DataElement dataElement = Values.Find(x => x.Key == key);
        if (dataElement == null)
        {
            dataElement = new DataElement();
            dataElement.Key = key;
            Values.Add(dataElement);
        }

        dataElement.Value = value;

        return dataElement;
    }

    public string GetValue(string key)
    {
        DataElement data = Values.Find(x => x.Key == key);
        if (data != null)
        {
            return data.Value;
        }
        else
        {
            return "";
        }
    }
}
