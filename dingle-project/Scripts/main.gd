extends Node3D

@onready var pause_menu = $CanvasLayer/PauseMenu
var paused = false

func _ready() -> void:
	# Wait for C# autoloads to be ready
	await get_tree().process_frame
	
	# Connect to Dialogue Manager events (from your autoload)
	if Engine.has_singleton("DialogueInteractions"):
		DialogueInteractions.WalkTo.connect(_on_walk_to)


func _process(delta: float) -> void:
	if Input.is_action_just_pressed("Pause"):
		pause_menu_toggle()


func _on_walk_to(target_position: Vector3) -> void:
	print("Got target position")
	print("Player walking to: ", target_position)
	$Player.WalkTo(target_position)


func pause_menu_toggle() -> void:
	if paused:
		pause_menu.hide()
		get_tree().paused = false
	else:
		pause_menu.show()
		get_tree().paused = true
	paused = !paused
