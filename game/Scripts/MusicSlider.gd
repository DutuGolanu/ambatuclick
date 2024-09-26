extends HSlider

var music_bus = AudioServer.get_bus_index("Music")

func _ready() -> void:
	value = Audio.music_value

func _on_value_changed(value: float) -> void:
	Audio.music_value = value
	AudioServer.set_bus_volume_db(music_bus,Audio.music_value)
	if value == -30:
		AudioServer.set_bus_mute(music_bus,true)
	else:
		AudioServer.set_bus_mute(music_bus,false)
