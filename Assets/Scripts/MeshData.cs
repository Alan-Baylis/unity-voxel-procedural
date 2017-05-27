using System;
using System.Collections.Generic;
using UnityEngine;

public class MeshData {
    public List<Vector3> vertices = new List<Vector3>();
    public List<int> triangles = new List<int>();
    public List<Vector2> uv = new List<Vector2>();
    public List<Vector3> colVertices = new List<Vector3>();
    public List<int> colTriangles = new List<int>();

    public bool colUseRenderData;

    public MeshData() {
    }

    public void AddQuadTriangles() {
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 1);
        if (colUseRenderData) {
            colTriangles.Add(colVertices.Count - 4);
            colTriangles.Add(colVertices.Count - 3);
            colTriangles.Add(colVertices.Count - 2);
            colTriangles.Add(colVertices.Count - 4);
            colTriangles.Add(colVertices.Count - 2);
            colTriangles.Add(colVertices.Count - 1);
        }
    }

    public void AddTriangle(int tri) {
        triangles.Add(tri);
        if (colUseRenderData) {
            colTriangles.Add(tri - (vertices.Count - colVertices.Count));
        }
    }

    public void AddVertex(Vector3 vertex) {
        vertices.Add(vertex);
        if (colUseRenderData) {
            colVertices.Add(vertex);
        }
    }
}