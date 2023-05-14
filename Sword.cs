using Godot;
using static Godot.GD;

namespace Remorphous;

public partial class Sword : Area2D
{
    private void OnBodyEntered(CanvasItem body)
    {
        body.Hide();
        body.GetParent().QueueFree();
        // Must be deferred as we can't change physics properties on a physics callback.
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    }

}
