using Godot;
using static Godot.GD;

namespace Remorphous;

public partial class Sword : Area2D
{
    private void OnBodyEntered(PhysicsBody2D body)
    {
        print("SWORD HIT");
    }

    private static void print(string msg)
    {
        Print(msg);
    }
}
