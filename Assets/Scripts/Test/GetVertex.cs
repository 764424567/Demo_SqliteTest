using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVertex : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] vertices = mesh.vertices;

        foreach (Vector3 vertex in vertices)
        {
            //Debug.Log(vertex);
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(i, i, i);
        }
    }
}
