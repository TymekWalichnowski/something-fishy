using Godot;
using System;

[GlobalClass] // Makes it appear in the "New Resource" menu
public partial class InventoryItem : Resource
{
    [Export]
    public string Name { get; set; } = "";

    [Export]
    public Texture2D Texture { get; set; }
}