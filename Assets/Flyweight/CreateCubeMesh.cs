using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCubeMesh : MonoBehaviour
{
    [Header("Terrain Settings")]
    public int width = 10;
    public int depth = 10;
    public float noiseScale = 0.2f;
    public float heightMultiplier = 3f;
    
    [Header("Mesh Settings")]
    public Material cubeMaterial;
    public string cubeNamePrefix = "CreatedCube_";
    public string meshNamePrefix = "ScriptedMesh";

    private enum Cubeside { BOTTOM, TOP, LEFT, RIGHT, FRONT, BACK }

    private readonly Dictionary<Cubeside, CubeSideData> cubeSideConfig = new Dictionary<Cubeside, CubeSideData>
    {
        { Cubeside.BOTTOM, new CubeSideData(new[] { 0, 1, 2, 3 }, Vector3.down, new[] { 3, 1, 0, 3, 2, 1 }) },
        { Cubeside.TOP, new CubeSideData(new[] { 7, 6, 5, 4 }, Vector3.up, new[] { 3, 1, 0, 3, 2, 1 }) },
        { Cubeside.LEFT, new CubeSideData(new[] { 7, 4, 0, 3 }, Vector3.left, new[] { 3, 1, 0, 3, 2, 1 }) },
        { Cubeside.RIGHT, new CubeSideData(new[] { 5, 6, 2, 1 }, Vector3.right, new[] { 3, 1, 0, 3, 2, 1 }) },
        { Cubeside.FRONT, new CubeSideData(new[] { 4, 5, 1, 0 }, Vector3.forward, new[] { 3, 1, 0, 3, 2, 1 }) },
        { Cubeside.BACK, new CubeSideData(new[] { 6, 7, 3, 2 }, Vector3.back, new[] { 3, 1, 0, 3, 2, 1 }) }
    };

    private readonly Vector3[] cubeVertices = 
    {
        new Vector3(-0.5f, -0.5f,  0.5f), // p0
        new Vector3( 0.5f, -0.5f,  0.5f), // p1
        new Vector3( 0.5f, -0.5f, -0.5f), // p2
        new Vector3(-0.5f, -0.5f, -0.5f), // p3
        new Vector3(-0.5f,  0.5f,  0.5f), // p4
        new Vector3( 0.5f,  0.5f,  0.5f), // p5
        new Vector3( 0.5f,  0.5f, -0.5f), // p6
        new Vector3(-0.5f,  0.5f, -0.5f)  // p7
    };

    private readonly Vector2[] quadUVs = 
    {
        new Vector2(1f, 1f), // uv11
        new Vector2(0f, 1f), // uv01
        new Vector2(0f, 0f), // uv00
        new Vector2(1f, 0f)  // uv10
    };

    private const int VERTICES_PER_QUAD = 4;
    private const int TRIANGLES_PER_QUAD = 6;
    private const int SIDES_PER_CUBE = 6;

    void Start()
    {
        if (cubeMaterial == null)
            cubeMaterial = new Material(Shader.Find("Specular"));

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                float height = Mathf.PerlinNoise(x * noiseScale, z * noiseScale) * heightMultiplier;
                Vector3 position = new Vector3(x, height, z);
                int cubeIndex = x * depth + z;
                CreateCube(cubeIndex, position);
            }
        }
    }

    void CreateQuad(Cubeside side, GameObject parent)
    {
        Mesh mesh = new Mesh();
        mesh.name = meshNamePrefix + side.ToString();

        Vector3[] vertices = new Vector3[VERTICES_PER_QUAD];
        Vector3[] normals = new Vector3[VERTICES_PER_QUAD];
        Vector2[] uvs = new Vector2[VERTICES_PER_QUAD];

        CubeSideData sideData = cubeSideConfig[side];

        for (int i = 0; i < VERTICES_PER_QUAD; i++)
        {
            vertices[i] = cubeVertices[sideData.vertexIndices[i]];
            normals[i] = sideData.normal;
            uvs[i] = quadUVs[i];
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.triangles = sideData.triangles;
        mesh.RecalculateBounds();

        GameObject quad = new GameObject("Quad");
        quad.transform.parent = parent.transform;
        MeshFilter meshFilter = quad.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    void CreateCube(int number, Vector3 position)
    {
        GameObject cube = new GameObject();
        cube.AddComponent<MeshFilter>();
        cube.AddComponent<MeshRenderer>();

        foreach (Cubeside side in System.Enum.GetValues(typeof(Cubeside)))
        {
            CreateQuad(side, cube);
        }

        MeshFilter[] meshFilters = cube.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
        }

        cube.GetComponent<MeshFilter>().mesh = new Mesh();
        cube.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        cube.GetComponent<MeshFilter>().mesh.name = cubeNamePrefix + number;
        
        MeshRenderer renderer = cube.GetComponent<MeshRenderer>();
        renderer.material = cubeMaterial;
        
        cube.gameObject.SetActive(true);
        cube.transform.position = position;
    }

    private struct CubeSideData
    {
        public readonly int[] vertexIndices;
        public readonly Vector3 normal;
        public readonly int[] triangles;

        public CubeSideData(int[] vertexIndices, Vector3 normal, int[] triangles)
        {
            this.vertexIndices = vertexIndices;
            this.normal = normal;
            this.triangles = triangles;
        }
    }
}
