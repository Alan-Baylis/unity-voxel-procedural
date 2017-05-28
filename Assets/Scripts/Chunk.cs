using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Chunk : MonoBehaviour {
    public static int ChunkSize = 16;

    public bool update = true;

    MeshFilter filter;

    MeshCollider col;

    public World world;

    public WorldPos pos;

    private Block[,,] blocks = new Block[ChunkSize, ChunkSize, ChunkSize];

    // Use this for initialization
    void Start() {
        filter = gameObject.GetComponent<MeshFilter>();
        col = gameObject.GetComponent<MeshCollider>();
    }

    //Update is called once per frame
    void Update() {
        if (update) {
            update = false;
            UpdateChunk();
        }
    }

    public Block GetBlock(int x, int y, int z) {
        if (InRange(x) && InRange(y) && InRange(z))
            return blocks[x, y, z];
        return world.GetBlock(pos.x + x, pos.y + y, pos.z + z);
    }

    public void SetBlock(int x, int y, int z, Block block) {
        if (InRange(x) && InRange(y) && InRange(z)) {
            blocks[x, y, z] = block;
        } else {
            world.SetBlock(pos.x + x, pos.y + y, pos.z + z, block);
        }
    }

    //new function
    public static bool InRange(int index) {
        return index >= 0 && index < ChunkSize;
    }

    // Updates the chunk based on its contents
    void UpdateChunk() {
        MeshData meshData = new MeshData();
        for (int x = 0; x < ChunkSize; x++) {
            for (int y = 0; y < ChunkSize; y++) {
                for (int z = 0; z < ChunkSize; z++) {
                    meshData = blocks[x, y, z].Blockdata(this, x, y, z, meshData);
                }
            }
        }
        RenderMesh(meshData);
    }

    // Sends the calculated mesh information
    // to the mesh and collision components
    void RenderMesh(MeshData meshData) {
        filter.mesh.Clear();
        filter.mesh.vertices = meshData.vertices.ToArray();
        filter.mesh.triangles = meshData.triangles.ToArray();
        filter.mesh.uv = meshData.uv.ToArray();
        filter.mesh.RecalculateNormals();

        col.sharedMesh = null;
        Mesh mesh = new Mesh();
        mesh.vertices = meshData.colVertices.ToArray();
        mesh.triangles = meshData.colTriangles.ToArray();
        mesh.RecalculateNormals();

        col.sharedMesh = mesh;
    }
}