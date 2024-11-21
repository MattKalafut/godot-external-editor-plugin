#if TOOLS
using Godot;

[Tool]
public class ExternalEditorTogglePlugin : EditorPlugin
{
	private CheckBox _checkbox;
	private EditorSettings _settings;

	public override void _EnterTree()
	{
		// Initialize settings and checkbox
		_settings = GetEditorInterface().GetEditorSettings();
		_checkbox = new CheckBox
		{
			Text = ".Net: Use Rider?"
		};
		
		// Get the current setting and log it
		int currentEditorSetting = _settings.Get("dotnet/editor/external_editor").As<int>();
		GD.Print("Current External Editor setting (integer): ", currentEditorSetting);
		
		// Set checkbox based on current setting (5 for Rider, 0 for Disabled)
		_checkbox.Pressed = currentEditorSetting == 5;
		_checkbox.Toggled += OnCheckBoxToggled;

		// Add checkbox to the toolbar
		AddControlToContainer(EditorPlugin.ContainerType.Toolbar, _checkbox);
	}

	public override void _ExitTree()
	{
		// Remove checkbox and free it when the plugin is disabled
		RemoveControlFromContainer(EditorPlugin.ContainerType.Toolbar, _checkbox);
		_checkbox.QueueFree();
	}

	private void OnCheckBoxToggled(bool pressed)
	{
		// Log checkbox toggled state and current setting before the change
		GD.Print("Checkbox toggled. New state: ", pressed);
		GD.Print("Current setting before toggle (integer): ", _settings.Get("dotnet/editor/external_editor"));
		
		// Set the external editor setting based on checkbox state
		_settings.Set("dotnet/editor/external_editor", pressed ? 5 : 0);
		
		// Log updated setting
		GD.Print("Updated External Editor setting (integer): ", _settings.Get("dotnet/editor/external_editor"));
	}
}
#endif
