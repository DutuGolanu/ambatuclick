extends Button

@onready var texture = $HBoxContainer/MarginContainer/TextureRect


func _on_mouse_entered() -> void:
	texture.self_modulate.a = 0.5


func _on_mouse_exited() -> void:
	texture.self_modulate.a = 1

func _process(_delta: float) -> void:
	if disabled:
		texture.self_modulate.a = 0.3
