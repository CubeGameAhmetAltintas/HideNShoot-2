using UnityEngine;

[System.Serializable]
public struct TransformData
{
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;

    public TransformData(Vector3 pos, Vector3 eulerAngles, Vector3 scale)
    {
        Position = pos;
        Rotation = eulerAngles;
        Scale = scale;
    }

    public TransformData(Transform transform)
    {
        Position = transform.position;
        Rotation = transform.localEulerAngles;
        Scale = transform.localScale;
    }
}
