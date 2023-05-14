using Godot;
using System;

public partial class Player : Area2D
{
    [Signal]
    public delegate void HitEventHandler();
    
    [Export]
    public int Speed = 400; // pixels/sec

    private Vector2 _screenSize; 
    private Vector2 _velocity = Vector2.Zero;
    private float _angularSpeed = Mathf.Pi;

    public void Start(Vector2 position)
    {
        Position = position;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
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
        RespondToMovementKeys(timeBetweenFrames);
    }

    private void RespondToMovementKeys(double timeBetweenFrames)
    {
        var direction = 0;
        _velocity = Vector2.Zero;

        if (Input.IsActionPressed("move_left"))
        {
            direction = -1;
        }
        else if (Input.IsActionPressed("move_right"))
        {
            direction = 1;
        }

        Rotation += _angularSpeed * direction * (float)timeBetweenFrames;

        if (Input.IsActionPressed("move_up"))
        {
            _velocity = Vector2.Up.Rotated(Rotation) * Speed;
        }
        
        /* TODO: Backwards movement?
        else if (Input.IsActionPressed("move_down"))
        {
            _velocity = Vector2.Down.Rotated(Rotation) * Speed;
        }
        */

        Position += _velocity * (float)timeBetweenFrames;

        var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

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
    
    private void OnBodyEntered(PhysicsBody2D body)
    {
        Hide(); // Player disappears after being hit.
        EmitSignal(SignalName.Hit);

        // Must be deferred as we can't change physics properties on a physics callback.
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    }
    
    
}
