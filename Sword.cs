using Godot;
using static Remorphous.HelperFunctions;

namespace Remorphous;

public partial class Sword : Area2D
{
    private void OnBodyEntered(CanvasItem body)
    {
        body.Hide();
        body.QueueFree();
    }
}
