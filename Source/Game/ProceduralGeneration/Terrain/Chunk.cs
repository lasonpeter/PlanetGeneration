using System;
using System.Collections.Generic;
using FlaxEngine;
using Game.Game.ProceduralGeneration.Terrain;

namespace Game;

/// <summary>
/// Chunk Script.
/// </summary>
public class Chunk : Script
{
    [Serialize] [ShowInEditor] private float size=1;
    private Model _model;
    public MaterialBase MaterialBase;
    public readonly Int2 Position;

    public Chunk()
    {
        
    }

    public override void OnStart()
    {
        // Create dynamic model with a single LOD with one mesh
        _model = Content.CreateVirtualAsset<Model>();
        _model.SetupLODs(new[] { 1 });
        UpdateMesh(_model.LODs[0].Meshes[0]);

        // Create or reuse child model
        var childModel = Actor.GetOrAddChild<StaticModel>();
        childModel.Model = _model;
        childModel.LocalScale = new Float3(1);
        childModel.SetMaterial(0, MaterialBase);
    }
    
    public override void OnDestroy()
    {
        FlaxEngine.Object.Destroy(ref _model);
    }
    
    public Chunk(Int2 position)
    {
        Position = position;
    }

    private void UpdateMesh(Mesh mesh)
    {
        /*const float X = 0.525731112119133606f;
        const float Z = 0.850650808352039932f;
        const float N = 0.0f;
        var vertices = new[]
        {
            new Float3(-X, N, Z),
            new Float3(X, N, Z),
            new Float3(-X, N, -Z),
            new Float3(X, N, -Z),
            new Float3(N, Z, X),
            new Float3(N, Z, -X),
            new Float3(N, -Z, X),
            new Float3(N, -Z, -X),
            new Float3(Z, X, N),
            new Float3(-Z, X, N),
            new Float3(Z, -X, N),
            new Float3(-Z, -X, N)
        };
        var triangles = new[]
        {
            1, 4, 0, 4, 9, 0, 4, 5, 9, 8, 5, 4,
            1, 8, 4, 1, 10, 8, 10, 3, 8, 8, 3, 5,
            3, 2, 5, 3, 7, 2, 3, 10, 7, 10, 6, 7,
            6, 11, 7, 6, 0, 11, 6, 1, 0, 10, 1, 6,
            11, 0, 9, 2, 11, 9, 5, 2, 9, 11, 2, 7
        };*/
        var meshData =MeshGeneration.GenerateCube(new Int3(0, 0, 0),size);
        mesh.UpdateMesh(meshData.vertices,meshData.triangles);
    }
    
    /// <inheritdoc/>
    public override void OnEnable()
    {
        // Here you can add code that needs to be called when script is enabled (eg. register for events)
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        // Here you can add code that needs to be called every frame
    }
}