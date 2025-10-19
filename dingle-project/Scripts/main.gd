extends Node3D


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var csharp_dialogue_interactions = $DialogueInteractions
	csharp_dialogue_interactions.connect("WalkTo", Callable(self, "_on_walk_to"))

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func _on_walk_to(target_position: Vector3):
	print("Got target position")
	print("Player walking to: ", target_position)
	$Player.WalkTo(target_position) 
