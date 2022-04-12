using UnityEngine;

public struct Triangles
{
    public int A;
    public int B;
    public int C;

    public Vector3 APoint;
    public Vector3 BPoint;
    public Vector3 CPoint;


    public Triangles(int aIndex, int bIndex, int cIndex, Mesh mesh)
    {
        A = aIndex;
        B = bIndex;
        C = cIndex;

        APoint = mesh.vertices[A];
        BPoint = mesh.vertices[B];
        CPoint = mesh.vertices[C];
    }
}

public struct Edge
{
    public int A;
    public int B;

    public Vector3 APoint;
    public Vector3 BPoint;

    public Edge(int a, int b, Vector3 aPoint, Vector3 bPoint)
    {
        A = a;
        B = b;

        APoint = aPoint;
        BPoint = bPoint;
    }
}