using Godot;
using System;

namespace Remorphous;

public partial class Player : Node2D
{

    [Signal]
    public delegate void HitEventHandler();
    
    [Export]
    public int Speed = 250; // pixels/sec

    private Vector2 _screenSize; 
    private Vector2 _velocity = Vector2.Zero;
    private float _angularSpeed = Mathf.Pi;

    public void Start(Vector2 position)
    {
        Position = position;
        Show();
        GetNode<Area2D>("PlayerBody").GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _screenSize = GetViewportRect().Size;
        //Hide();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double timeBetweenFrames)
    { 
        FollowMouse(timeBetweenFrames);
    }

    // Emit hit signal visible by Main
    private void EmitHit()
    {
        EmitSignal(SignalName.Hit);
    }
    
    private void FollowMouse(double timeBetweenFrames)
    {
        _velocity = Vector2.Zero;
        var mousePosition = GetGlobalMousePosition();
        var normalizedDirection = Vector2.Zero;
        var direction = Vector2.Zero;
        direction = (mousePosition - Position);
        normalizedDirection = direction.Normalized();
        // Make player follow mouse
        if ( direction.Length() > 30 ) // TODO: bad magic number
        {
            Rotation = mousePosition.AngleTo(normalizedDirection) + (float)(Math.PI / 2);
            _velocity = normalizedDirection * Speed;
            Position += _velocity * (float)timeBetweenFrames;
        }
        
        // Set animation based on velocity
        var playerBody = GetNode<Area2D>("PlayerBody");
        var animatedSprite2D = playerBody.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        if (_velocity.Length() > 0)
        {
            animatedSprite2D.Play();
        }
        else
        {
            animatedSprite2D.Stop();
        }

        // Make sure the player doesn't leave the screen
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, _screenSize.X),
            y: Mathf.Clamp(Position.Y, 0, _screenSize.Y)
        );
    }

}
