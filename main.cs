using Godot;
using System;

public partial class Main : Node
{
    [Export] public PackedScene GloopleScene { get; set; }

    private int _score;

    public override void _Ready()
    {
        NewGame();
    }

    private void GameOver()
    {
        GetNode<Timer>("GloopleTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();
    }

    private void NewGame()
    {
        _score = 0;

        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Marker2D>("StartPosition");
        player.Start(startPosition.Position);

        GetNode<Timer>("StartTimer").Start();
    }

    private void OnScoreTimerTimeout()
    {
        _score++;
    }

    private void OnStartTimerTimeout()
    {
        GetNode<Timer>("GloopleTimer").Start();
        GetNode<Timer>("ScoreTimer").Start();
    }
    
    private void OnGloopleTimerTimeout()
    {
        // Create a new instance of the Mob scene.
        Gloople gloople = GloopleScene.Instantiate<Gloople>();

        // Choose a random location on Path2D.
        var mobSpawnLocation = GetNode<PathFollow2D>("Path2D/GloopleSpawnLocation");
        mobSpawnLocation.ProgressRatio = GD.Randf();
        // Set the mob's direction perpendicular to the path direction.
        var direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;
        
        gloople.Position = mobSpawnLocation.Position;

        // Add some randomness to the direction.
        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        gloople.Rotation = direction;

        // Choose the velocity.
        var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
        gloople.LinearVelocity = velocity.Rotated(direction);

        // Spawn the mob by adding it to the Main scene.
        GD.Print("Adding gloople");
        AddChild(gloople);
    }
}
