
using JingleJam2024;
using JingleJam2024.scene;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Toybox;
using Toybox.graphic;
using Toybox.load;
using Toybox.tiled;
using Toybox.utils.input;
using Toybox.utils.text;

public static class Program {

	public static Font Font;

	public static GameScene Scene;
	public static GameInputManager<GameControl> Input = new GameInputManager<GameControl>();
	public static GameState State = new GameState();
	public static AssetManager<SpriteMap> Sprites = new AssetManager<SpriteMap>(new AsepriteLoader());
	public static Texture2D PlayerSprite;
	public static List<string> Levels = new();
	public static GameCompleteMessage CompleteMessage;
	public static TutorialText Tutorial = new();

	private static void Main(string[] args) {
		using var game = new JingleJam2024.Main();
		game.Run();
	}


	public static void NextStage() {
		State.StageNum++;
		if (State.StageNum < Levels.Count) {
			LoadStage(Levels[State.StageNum]);
			Scene.Init();
		} else {
			State.GameComplete = true;
		}
	}

	public static void LoadStage(string path) {
		var scene = new GameScene();
		var tiled = new TiledFile(Resources.Content.RootDirectory, path);
		tiled.TryGetTilemap("graphic", out var tilemap);
		scene.GraphicMap = tilemap;
		tiled.TryGetObjects("spawns", out var spawns);
		scene.Spawns = spawns;
		tiled.TryGetTilemap("mech", out var mech);
		scene.MechMap = mech;
		tiled.TryGetTilemap("doors", out var doors);
		scene.DoorMap = doors;
		tiled.TryGetTilemap("floor", out var floor);
		scene.FloorMap = floor;
		Scene = scene;
		State.Money = GameState.StartingMoney;
	}
}