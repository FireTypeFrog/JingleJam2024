using JingleJam2024.entity.player;
using JingleJam2024.scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
			Program.Input.Add(GameControl.Enter, new VirtualKey(Keys.Enter));
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
			CheerMeter.TextRenderer = new Text(Program.Font);
			Player.Shadow = Content.Load<Texture2D>("sprites/shadow");
			Program.CompleteMessage = new GameCompleteMessage(Program.Font);
			GameScene.GameOverMessage = new GameOver(Program.Font);
			TutorialText.Graphic = Content.Load<Texture2D>("sprites/arrow");
			TutorialText.TextRenderer = new Text(Program.Font);
			TitleScreen.TextRenderer = new Text(Program.Font);

			Program.Levels.Add("maps/level0.tmx");
			Program.Levels.Add("maps/level1.tmx");
			Program.Levels.Add("maps/level2.tmx");
			Program.Levels.Add("maps/level3.tmx");
			Program.LoadStage(Program.Levels[0]);

			SoundPlayer.Music = Content.Load<Song>("music/ChristmasTime");
			SoundPlayer.Bump = Content.Load<SoundEffect>("music/bump");
			SoundPlayer.Gameover = Content.Load<SoundEffect>("music/gameover");
			SoundPlayer.Pickup = Content.Load<SoundEffect>("music/pickup");
			SoundPlayer.Spin = Content.Load<SoundEffect>("music/spin");
			SoundPlayer.Target = Content.Load<SoundEffect>("music/target");
			SoundPlayer.Win = Content.Load<SoundEffect>("music/win");
		}

		protected override void Init() {
			Program.Scene.Init();
			SoundPlayer.Init();
		}

		protected override void UpdateInputManager(MouseState m, KeyboardState k) {
			Program.Input.UpdateControlStates(k, m);
		}

		protected override Font GetDefaultFont() {
			return Program.Font;
		}
	}
}