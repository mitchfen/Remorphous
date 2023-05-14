using Godot;

public partial class Gloople : RigidBody2D
{
    public override void _Ready()
    {
        var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite2D.Play();
    }
    
    private void OnVisibleOnScreenNotifier2DScreenExited()
    {
        // Delete when exiting screen
        //QueueFree();
        LinearVelocity = LinearVelocity.Rotated(Mathf.DegToRad(45));
    }
}
