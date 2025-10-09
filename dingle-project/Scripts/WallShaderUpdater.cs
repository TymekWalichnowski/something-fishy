using Godot;
using System;
using System.Diagnostics;

public partial class WallShaderUpdater : Node3D
{
    [Export] public Player player;
    [Export] public RayCast3D rayToPlayer;
    [Export] public ShaderMaterial SharedWallMaterial;
    private bool playerSeen;

    // Handle wall hit
    MeshInstance3D hitObj;
    MeshInstance3D prevHitObj;

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
            hitObj = HasLineOfSight(out playerSeen);
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

            // Handle which is the newest hit
            // turn off previous object
            if (prevHitObj != null && prevHitObj != hitObj)
            {
                prevHitObj.SetInstanceShaderParameter("hole_visable", false);
            }

            if (hitObj != null)
            {
                hitObj.SetInstanceShaderParameter("hole_visable", true);
                prevHitObj = hitObj;
            }
            else if (prevHitObj != null)
            {
                prevHitObj.SetInstanceShaderParameter("hole_visable", false);
                prevHitObj = null;
            }

            SharedWallMaterial.SetShaderParameter("hole_size", holeSize);
        }
    }

    public MeshInstance3D HasLineOfSight(out bool t_playerSeen)
    {
        rayToPlayer.TargetPosition = rayToPlayer.ToLocal(player.GlobalPosition);
        rayToPlayer.ForceRaycastUpdate();

        if (rayToPlayer.GetCollider() == player)
        {
            t_playerSeen = true;
            return null;
        }
        else
        {
            t_playerSeen = false;
            return (rayToPlayer.GetCollider() as Node).GetParent() as MeshInstance3D;
        }   
            
    }
}
