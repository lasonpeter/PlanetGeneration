using System;
using System.Collections.Generic;
using System.Timers;
using FlaxEngine;

namespace Game;

/// <summary>
/// SimulationManager Script.
/// </summary>
public class SimulationManager : Script
{
    public SimulationManager(){}
    private Timer simulationTimer;
    private List<Planet> _planets = new List<Planet>();
    public int deltaTime = 1; //In seconds
    public float step = 1f; //in miliseconds, amount of deltatime simulations per second, max = 1000
    /// <inheritdoc/>
    public override void OnStart()
    {
        var actors = Level.FindActors(Tags.Get("planet"), true);
        foreach (var actor in actors)
        {
            _planets.Add(actor.GetScript<Planet>());
        }
        // Here you can add code that needs to be called when script is created, just before the first game update
        simulationTimer = new Timer((int)(1000/step));
        simulationTimer.Elapsed += (sender, args) => { Simulate(); };
        simulationTimer.Start();
    }
    
    const double GravitationalConstant = 6.67430e-11;
    private void Simulate()
    {
        /*Debug.Log("SIMULATE");
        Debug.Log("Count:"+ _planets.Count);*/
        for (int i = 0; i < _planets.Count; i++)
        {
            for (int j = 0; j < _planets.Count; j++)
            {
                if (i != j)
                {
                    var force =  GravitationalConstant * (_planets[i].Mass * _planets[j].Mass) /
                                Vector3.Distance(_planets[i].Position, _planets[j].Position);
                    Vector3 direction = _planets[j].Position - _planets[i].Position; //FROM I TO J
                    Vector3 normDirVector= Vector3.Normalize(direction);
                    /*Debug.Log("MASS"+_planets[i].Mass);
                    Debug.Log( "DISTANCE:"+Vector3.Distance(_planets[i].CurrentPosition, _planets[j].CurrentPosition));
                    Debug.Log("FORCE"+force);*/
                    //F = m*a
                    //a = F/m
                    //s = a*t^2/2
                    _planets[i].AcceleratedPositionChange((float)force,normDirVector,deltaTime);
                    //_planets[i].AcceleratedPositionChange(force,normDirVector,deltaTime);

                }
            }
        }

        foreach (var planet in _planets)
        {
            planet.UpdatePosition(deltaTime);
        }
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

    public override void OnFixedUpdate()
    {
        if (simulationTimer.Interval != (int)(1000 / step))
        {
            simulationTimer.Interval = (int)(1000 / step);
        }
    }

    public override void OnDestroy()
    {
        simulationTimer.Stop();
        simulationTimer.Dispose();
        base.OnDestroy();
    }
}
