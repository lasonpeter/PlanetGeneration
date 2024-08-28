using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// Planet Script.
/// </summary>
public class Planet : Script
{
    [Serialize][ShowInEditor]private int x = 0;
    public Planet(){}
    public Vector3 Position { get; set; }
    //public Vector3 NewPosition { get; set; }

    private Vector3 pos;
    public Vector3 Velocity { get; set; }
    public ulong Mass { get; set; }

    
    //TODO: BOUND THE CALCULATIONS FOR MAX v=C 
    public void AcceleratedPositionChange(float force, Vector3 normalizedDir,float deltaTime)
    {
        //Debug.Log(force);
        var velocityChange = ((force / Mass) * deltaTime);
        Velocity = Velocity+ normalizedDir * velocityChange;
    }

    public override void OnAwake()
    {
        Position = Actor.Position;
    }

    public void UpdatePosition(float deltaTime)
    {
        x++;
        pos = Position;
        Position = Position+Velocity*deltaTime;
        Actor.Position = Position;
        //Debug.Log("Updated");
    }
    /// <inheritdoc/>
    public override void OnStart()
    {
        
        // Here you can add code that needs to be called when script is created, just before the first game update
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

    public override void OnDebugDraw()
    {
        if (x > 1000)
        {
            DebugDraw.DrawBox(new BoundingBox(Position, Position+1),Color.Blue,10,true);
            DebugDraw.DrawLine(Position, pos, Color.Green, Color.Green, 10f, true);
            x = 0;
        }
        base.OnDebugDraw();
    }
}
