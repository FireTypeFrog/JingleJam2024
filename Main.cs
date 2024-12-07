using JingleJam2024.entity.player;
using JingleJam2024.scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Toybox;
using Toybox.rendermodels;
using Toybox.scenes;
using Toybox.tiled;
using Toybox.utils.input;
using Toybox.utils.text;

namespace JingleJam2024 {
	public class Main:Core {

		public Main() {
			Program.Input.Add(GameControl.Left, new VirtualKey(Keys.A, Keys.Left));
			Program.Input.Add(GameControl.Right, new VirtualKey(Keys.D, Keys.Right));
			Program.Input.Add(GameControl.Up, new VirtualKey(Keys.W, Keys.Up));
			Program.Input.Add(GameControl.Down, new VirtualKey(Keys.S, Keys.Down));
			Program.Input.Add(GameControl.SwapGraphic, new VirtualKey(Keys.F5));
			IsFixedTimeStep = true;
        }

		public override Scene GetActiveScene() {
			return Program.Scene;
		}

		protected override void LoadContent() {
			base.LoadContent();

			Camera = new Camera(GraphicsDevice, new Fit(630, 340), 3) { ClearColor = new Color(0, 0, 0) };

			Program.Font = new Font(Content.Load<Texture2D>("rainyhearts"), Font.FontStandard, '?', new Rectangle(0, 3, 1, 1));
			Program.Sprites.LoadDirectory(Path.Join(Content.RootDirectory, "sprites"));
			Program.PlayerSprite = Content.Load<Texture2D>("sprites/sleigh");
			FloatingText.TextRenderer = new Text(Program.Font);

			Program.Levels.Add(LoadLevel("maps/level0.tmx"));
			Program.Levels.Add(LoadLevel("maps/level1.tmx"));
			Program.Scene = Program.Levels[0];
		}

		private GameScene LoadLevel(string path) {
			var scene = new GameScene();
			var tiled = new TiledFile(Content.RootDirectory, path);
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
			return scene;
		}

		protected override void Init() {
			Program.Scene.Init();
		}

		protected override void UpdateInputManager(MouseState m, KeyboardState k) {
			Program.Input.UpdateControlStates(k, m);
		}

		protected override Font GetDefaultFont() {
			return Program.Font;
		}
	}
}