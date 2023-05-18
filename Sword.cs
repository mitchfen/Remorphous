using Godot;
using static Remorphous.HelperFunctions;

namespace Remorphous;

public partial class Sword : Area2D
{
    private void OnBodyEntered(CanvasItem body)
    {
        Print("SWORD HIT");
        body.Hide();
        body.QueueFree();
    }


}
