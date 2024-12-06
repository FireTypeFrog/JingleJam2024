﻿
using JingleJam2024;
using JingleJam2024.scene;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Toybox.graphic;
using Toybox.load;
using Toybox.utils.input;
using Toybox.utils.text;

public static class Program {

	public static Font Font;

	public static GameScene Scene;
	public static GameInputManager<GameControl> Input = new GameInputManager<GameControl>();
	public static GameState State = new GameState();
	public static AssetManager<SpriteMap> Sprites = new AssetManager<SpriteMap>(new AsepriteLoader());
	public static Texture2D PlayerSprite;
	public static List<GameScene> Levels = new();
	public static GameCompleteMessage CompleteMessage;

	private static void Main(string[] args) {
		using var game = new JingleJam2024.Main();
		game.Run();
	}


	public static void NextStage() {
		State.Money = GameState.StartingMoney;
		State.StageNum++;
		if (State.StageNum < Levels.Count) {
			Scene = Levels[State.StageNum];
			Scene.Init();
		} else {
			Program.State.GameComplete = true;
		}
	}
}