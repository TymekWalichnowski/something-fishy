using Godot;
using System;
using System.Diagnostics;

public partial class WallShaderUpdater : Node3D
{
    [Export] public Player player;
    [Export] public RayCast3D rayToPlayer;
    [Export] public ShaderMaterial SharedWallMaterial;
    private bool playerSeen;

    // Hole parameters
    private bool holeOn;
    private float holeSize = 1f;
    [Export] private float maxHoleSize = 0.97f;
    [Export] private float minHoleSize = 0.985f;

    public override void _Ready()
    {
        SharedWallMaterial.SetShaderParameter("alignment_max", maxHoleSize);
    }


    public override void _Process(double delta)
    {
        if (player != null && SharedWallMaterial != null)
        {
            playerSeen = HasLineOfSight();
            SharedWallMaterial.SetShaderParameter("player_position", player.GlobalPosition);

            if (playerSeen)
            {
                if (holeSize <= minHoleSize)
                {
                    holeSize += 0.01f;
                }
                else
                {
                    holeOn = false;
                }
            }
            else if (holeSize > maxHoleSize)
            {
                holeOn = true;
                holeSize -= 0.001f;
            }


            SharedWallMaterial.SetShaderParameter("hole_visable", holeOn);
            SharedWallMaterial.SetShaderParameter("hole_size", holeSize);
        }
    }

    public bool HasLineOfSight()
    {
        rayToPlayer.TargetPosition = rayToPlayer.ToLocal(player.GlobalPosition);
        rayToPlayer.ForceRaycastUpdate();

        if (rayToPlayer.GetCollider() == player)
        {
            return true;
        }

        return false;
    }
}
