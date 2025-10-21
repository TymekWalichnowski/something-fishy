using Godot;

public partial class BillboardSprite : Node3D
{
    private AnimatedSprite3D _anim;

    public override void _Ready()
    {
        _anim = GetNode<AnimatedSprite3D>("AnimatedSprite3D");
    }

    public override void _Process(double delta)
    {
        var camera = GetViewport().GetCamera3D();
        if (camera == null)
            return;

        // Make the sprite face the camera
        _anim.LookAt(camera.GlobalTransform.Origin, Vector3.Up, true);

        // Quads in Godot face -Z, so rotate 180 degrees to face the camera
        _anim.RotateY(Mathf.Pi);
    }
}
