extends AudioStreamPlayer

@onready var hover = load("res://Assets/Audio/ui_hover.ogg")
@onready var select = load("res://Assets/Audio/ui_select.ogg")

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


func _on_responses_menu_response_selected(response: Variant) -> void:
	stream = select
	play()


func _on_responses_menu_response_hovered() -> void:
	stream = hover
	play()
