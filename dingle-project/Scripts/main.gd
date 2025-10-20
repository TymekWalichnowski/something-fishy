extends Node3D

@onready var pause_menu = $CanvasLayer/PauseMenu
var paused = false

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	# Wait for the C# autoload to be ready
	await get_tree().process_frame
	DialogueInteractions.WalkTo.connect(_on_walk_to)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if Input.is_action_just_pressed("Pause"):
		pauseMenu()

func _on_walk_to(target_position: Vector3):
	print("Got target position")
	print("Player walking to: ", target_position)
	$Player.WalkTo(target_position) 

func pauseMenu():
	if paused:
		#Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
		pause_menu.hide()
		get_tree().paused = false
		#$Player.ToggleMove(true)
	else:
		#Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
		pause_menu.show()
		get_tree().paused = true
		#$Player.ToggleMove(false)
		
	paused = !paused
