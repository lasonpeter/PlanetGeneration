using System;
using FlaxEngine;
using Object = FlaxEngine.Object;

namespace Game;

/// <summary>
/// CameraMovement Script.
/// </summary>
public class CameraMovement : Script
{
    [Serialize] public Vector3 _velocity = new();
    [Serialize] public float mouseSensitivity = 0.1f;
    [Serialize] public float mouseX = 1f;
    [Serialize] public float mouseY =1f;
    [Serialize] public float Speed =10f;
    /// <inheritdoc/>
    public override void OnStart()
    {
        Screen.CursorLock = CursorLockMode.Locked;
        Screen.CursorVisible = false;
        // Here you can add code that needs to be called when script is created, just before the first game update
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        //mouseX= Mathf.Clamp(mouseX, 0, 90);
        //mouseY= Mathf.Clamp(mouseY, 0, 180);
        //Debug.Log(mouseX);
        var w= Actor.LocalEulerAngles + new Vector3(mouseY,mouseX,0);
        w.X = Mathf.Clamp(w.X, -90, 90);
        //w.X = Mathf.Clamp(w.X, 0, 90);
        Actor.LocalEulerAngles = w;
        // Here you can add code that needs to be called every frame
    }

    public override void OnFixedUpdate()
    {
        Vector3 normalizedSpeedChange = Vector3.Zero;
        if (Input.GetAction("MoveForward"))
        {
            normalizedSpeedChange += Actor.Transform.Forward;
        }
        if (Input.GetAction("MoveBackwards"))
        {
            normalizedSpeedChange += Actor.Transform.Backward;
        }
        if (Input.GetAction("MoveRight"))
        {
            normalizedSpeedChange += Actor.Transform.Right;
        }
        if (Input.GetAction("MoveLeft"))
        {
            normalizedSpeedChange += Actor.Transform.Left;
        }
        //Vector3.Forward is in global space
        // Use Actor.Forward
        // 
        normalizedSpeedChange *= Speed;
        Actor.LocalPosition = (Actor.LocalPosition + normalizedSpeedChange);
        // Actor.Position=Actor.LocalTransform.LocalToWorldVector(Actor.LocalPosition + normalizedSpeedChange);

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
}
