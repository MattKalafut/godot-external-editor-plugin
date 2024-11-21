@tool
extends EditorPlugin

var checkbox = CheckBox.new()
var settings = get_editor_interface().get_editor_settings()

func _enter_tree():
	# Initial setup for the checkbox
	checkbox.text = ".Net: Use Rider?"
	
	# Log the current setting for dotnet/editor/external_editor
	var current_editor_setting = settings.get("dotnet/editor/external_editor")
	print("Current External Editor setting (integer):", current_editor_setting)
	
	# Set checkbox based on the current integer setting (5 for Rider, 0 for Disabled)
	checkbox.set_pressed(current_editor_setting == 5)
	checkbox.connect("toggled", Callable(self, "_on_CheckBox_toggled"))
	
	# Add checkbox to the toolbar
	add_control_to_container(EditorPlugin.CONTAINER_TOOLBAR, checkbox)

func _exit_tree():
	# Clean up when the plugin is disabled
	remove_control_from_container(EditorPlugin.CONTAINER_TOOLBAR, checkbox)
	checkbox.free()

func _on_CheckBox_toggled(pressed):
	# Log when the checkbox is toggled and the new state
	print("Checkbox toggled. New state:", pressed)
	
	# Log the current setting before changing it
	print("Current setting before toggle (integer):", settings.get("dotnet/editor/external_editor"))
	
	# Update the external editor setting based on the checkbox state
	settings.set("dotnet/editor/external_editor", 5 if pressed else 0)
	
	# Log the setting after change
	print("Updated External Editor setting (integer):", settings.get("dotnet/editor/external_editor"))
