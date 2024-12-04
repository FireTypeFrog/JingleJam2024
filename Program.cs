
using JingleJam2024;
using Toybox.utils.input;
using Toybox.utils.text;

public static class Program {

	public static Font Font;

	public static GameScene Scene;
	public static GameInputManager<GameControl> Input = new GameInputManager<GameControl>();
	public static GameState State = new GameState();

	private static void Main(string[] args) {
		using var game = new JingleJam2024.Main();
		game.Run();
	}
}