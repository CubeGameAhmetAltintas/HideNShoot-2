using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VectorInput : MonoBehaviour
{
    [SerializeField] InputField xInp, yInp, zInp;

    public void SetValue(Vector3 value)
    {
        xInp.text = value.x.ToString();
        yInp.text = value.y.ToString();
        zInp.text = value.z.ToString();
    }

    public Vector3 GetValue()
    {
        return new Vector3(xInp.text.Length > 0 ? System.Convert.ToSingle(xInp.text) : 0, yInp.text.Length > 0 ? System.Convert.ToSingle(yInp.text) : 0, zInp.text.Length > 0 ? System.Convert.ToSingle(zInp.text) : 0);
    }

    public void SetValue(Vector2 value)
    {
        xInp.text = value.x.ToString();
        yInp.text = value.y.ToString();
    }

    public Vector2 GetValueV2()
    {
        return new Vector2(xInp.text.Length > 0 ? System.Convert.ToSingle(xInp.text) : 0, yInp.text.Length > 0 ? System.Convert.ToSingle(yInp.text) : 0);
    }

    public Vector2Int GetValueV2Int()
    {
        return new Vector2Int(xInp.text.Length > 0 ? System.Convert.ToInt32(xInp.text) : 0, yInp.text.Length > 0 ? System.Convert.ToInt32(yInp.text) : 0);
    }
}