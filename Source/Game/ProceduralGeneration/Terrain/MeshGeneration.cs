using System.Collections.Generic;
using FlaxEngine;

namespace Game.Game.ProceduralGeneration.Terrain;

public class MeshGeneration
{
    public static CubeMesh GenerateCube(Int3 position, float size)
    {
        List<Float3> vertices = new  List<Float3>(8);
        vertices.Add(new Float3(0, 0, 0)*size);
        vertices.Add(new Float3(0, 0, 1)*size);
        vertices.Add(new Float3(0, 1, 0)*size);
        vertices.Add(new Float3(0, 1, 1)*size);
        vertices.Add(new Float3(1, 0, 0)*size);
        vertices.Add(new Float3(1, 0, 1)*size);
        vertices.Add(new Float3(1, 1, 0)*size);
        vertices.Add(new Float3(1, 1, 1)*size);
        List<int> triangles = new List<int>(0);
        //FRONT FACE
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(2);
        triangles.Add(1);
        triangles.Add(3);
        triangles.Add(2);
        //RIGHT FACE
        triangles.Add(1);
        triangles.Add(5);
        triangles.Add(3);
        triangles.Add(5);
        triangles.Add(7);
        triangles.Add(3);
        //LEFT FACE
        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(4);
        triangles.Add(4);
        triangles.Add(2);
        triangles.Add(6);
        //BACK FACE
        triangles.Add(5);
        triangles.Add(4);
        triangles.Add(6);
        triangles.Add(7);
        triangles.Add(5);
        triangles.Add(6);
        //BOTTOM FACE
        triangles.Add(0);
        triangles.Add(4);
        triangles.Add(1);
        triangles.Add(1);
        triangles.Add(4);
        triangles.Add(5);
        
        //TOP FACE
        triangles.Add(2);
        triangles.Add(3);
        triangles.Add(6);
        triangles.Add(3);
        triangles.Add(7);
        triangles.Add(6);
        return new CubeMesh(vertices.ToArray(), triangles.ToArray());
    }
}

public struct CubeMesh(Float3[] vertices, int[] triangles)
{
    public Float3[] vertices { get; set; } = vertices;
    public int[] triangles { get; set; } = triangles;
}