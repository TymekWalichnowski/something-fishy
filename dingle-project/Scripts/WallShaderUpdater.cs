using Godot;
using System;
using System.Diagnostics;

public partial class WallShaderUpdater : Node
{
	[Export] public Node3D Player;
    [Export] public ShaderMaterial SharedWallMaterial;

    public override void _Process(double delta)
    {
		if (Player != null && SharedWallMaterial != null)
		{
			SharedWallMaterial.SetShaderParameter("player_position", Player.GlobalPosition);
        }
    }
}
