extends HSlider

var master_bus = AudioServer.get_bus_index("Main")

func _ready() -> void:
	value = Audio.audio_value

func _on_value_changed(value: float) -> void:
	Audio.audio_value = value
	AudioServer.set_bus_volume_db(master_bus,Audio.audio_value)
	if value == -30:
		AudioServer.set_bus_mute(master_bus,true)
	else:
		AudioServer.set_bus_mute(master_bus,false)
