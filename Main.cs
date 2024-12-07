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

			var tiled = new TiledFile(Content.RootDirectory, "maps/map.tmx");
			Program.Scene = new GameScene();
			tiled.TryGetTilemap("graphic", out var tilemap);
			Program.Scene.GraphicMap = tilemap;
			tiled.TryGetObjects("spawns", out var spawns);
			Program.Scene.Spawns = spawns;
			tiled.TryGetTilemap("mech", out var mech);
			Program.Scene.MechMap = mech;
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