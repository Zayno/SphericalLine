//Author Wiaam Suleiman
//this script creates a line mesh around a sphere of radius r
//https://github.com/Zayno/SphericalLine

using UnityEngine;

public class SphericalLine : MonoBehaviour
{
    public float LineWidth = 1;
    public float radius = 5;

    [Range(0, 360)]
    public int Faces = 360;//1 face maps to 1/360 of a circle 
    Vector3[] MyVertices;
    int[] MyTriangles;

    public Mesh MyMesh;

    public void Reset()
    {
        if (MyVertices != null && MyVertices.Length > 0)
        {
            System.Array.Clear(MyVertices, 0, MyVertices.Length);
            MyVertices = null;
        }

        if (MyTriangles != null && MyTriangles.Length > 0)
        {
            System.Array.Clear(MyTriangles, 0, MyTriangles.Length);
            MyTriangles = null;
        }
        if (MyMesh != null)
        {
            MyMesh.Clear();
            MyMesh = null;
        }

    }

    void BuildMesh()
    {
        Reset();
        if (Faces == 0) return;

        MyMesh = new Mesh();
        int NumOfVertivies = 2 + Faces * 2;

        MyVertices = new Vector3[NumOfVertivies];

        float Angle = 0;
        for (int i = 0; i < MyVertices.Length; i += 2)
        {
            MyVertices[i] =     new Vector3(-(LineWidth / 2.0f), radius * Mathf.Cos(Angle * Mathf.Deg2Rad), radius * Mathf.Sin(Angle * Mathf.Deg2Rad) );
            MyVertices[i + 1] = new Vector3((LineWidth / 2.0f),  radius * Mathf.Cos(Angle * Mathf.Deg2Rad), radius * Mathf.Sin(Angle * Mathf.Deg2Rad) );
            Angle++;
        }
        MyMesh.vertices = MyVertices;

        MyTriangles = new int[Faces * 6];

        int looper1 = 0;
        int looper2 = 2;
        int Looper3 = 1;
        for (int Tris = 0; Tris < MyTriangles.Length; Tris += 6)
        {
            MyTriangles[Tris] = looper1;
            looper1++;
            MyTriangles[Tris + 1] = looper2;
            MyTriangles[Tris + 2] = Looper3;
            Looper3 += 2;

            MyTriangles[Tris + 3] = looper1;
            looper1++;
            MyTriangles[Tris + 4] = looper2;
            MyTriangles[Tris + 5] = Looper3;

            looper2 += 2;

        }

        MyMesh.triangles = MyTriangles;

        MyMesh.RecalculateNormals();

        if (GetComponent<MeshFilter>() != null)
        {
            GetComponent<MeshFilter>().mesh = MyMesh;
        }
        else
        {
            Debug.LogError("Add a mesh filter component to this game object");
        }


    }

    // Use this for initialization
    void Start()
    {
        BuildMesh();
    }

    void OnValidate()
    {
        BuildMesh();
    }
}
