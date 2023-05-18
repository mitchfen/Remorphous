using Godot;

namespace Remorphous;

public partial class PlayerBody : Area2D
{
    [Signal]
    public delegate void HitEventHandler();
    
    private void OnBodyEntered(PhysicsBody2D body)
    {
        Hide();
        GetParent().GetNode<Area2D>("Sword").Hide();

        EmitSignal(SignalName.Hit);

        // Must be deferred as we can't change physics properties on a physics callback.
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
        GetParent().GetNode<Area2D>("Sword").GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    }
}
