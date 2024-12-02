using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Toybox;
using Toybox.rendermodels;
using Toybox.scenes;
using Toybox.utils.input;
using Toybox.utils.text;

namespace JingleJam2024 {
	public class Main:Core {

// Hello this is my test comment, I hope you like it! :)

		public Main() {
			Program.Input.Add(GameControl.Left, new VirtualKey(Keys.A, Keys.Left, Keys.T));
			Program.Input.Add(GameControl.Right, new VirtualKey(Keys.D, Keys.Right));
			Program.Input.Add(GameControl.Up, new VirtualKey(Keys.W, Keys.Up));
			Program.Input.Add(GameControl.Down, new VirtualKey(Keys.S, Keys.Down));
		}

		public override Scene GetActiveScene() {
			return Program.Scene;
		}

		protected override void LoadContent() {
			base.LoadContent();

			Camera = new Camera(GraphicsDevice, new Fit(630, 340), 3) { ClearColor = new Color(32, 18, 8) };

			Program.Font = new Font(Content.Load<Texture2D>("rainyhearts"), Font.FontStandard, '?', new Rectangle(0, 3, 1, 1));
		}

		protected override void Init() {
			Program.Scene = new GameScene();

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