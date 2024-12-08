using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.utils.text;

namespace JingleJam2024.scene {
	public class GameOver {

		public Text TextRenderer;

		private float Text1 = 0;
		private float Text2 = 0;
		private float Text3 = 0;
		private float Text4 = 0;
		private int Timer = 0;
		private float FadeRate = 0.05f;

		public GameOver(Font f) {
			TextRenderer = new Text(f);
		}

		public void Draw(Renderer r, Camera c) {
			TextRenderer.Content = $"Game Over!";
			TextRenderer.Position = new Point(c.Width / 3, 100);
			TextRenderer.Draw(r.Batch, Color.Black * Text1);
			TextRenderer.Position -= new Point(1, 1);
			TextRenderer.Draw(r.Batch, Color.White * Text1);

			TextRenderer.Content = $"Santa is out of money.";
			TextRenderer.Position = new Point(c.Width / 3, 120);
			TextRenderer.Draw(r.Batch, Color.Black * Text2);
			TextRenderer.Position -= new Point(1, 1);
			TextRenderer.Draw(r.Batch, Color.White * Text2);

			TextRenderer.Content = $"You ruined Christmas.";
			TextRenderer.Position = new Point(c.Width / 3, 140);
			TextRenderer.Draw(r.Batch, Color.Black * Text3);
			TextRenderer.Position -= new Point(1, 1);
			TextRenderer.Draw(r.Batch, Color.White * Text3);

			TextRenderer.Content = $"Press Enter to try again...";
			TextRenderer.Position = new Point(c.Width / 3, 180);
			TextRenderer.Draw(r.Batch, Color.Black * Text4);
			TextRenderer.Position -= new Point(1, 1);
			TextRenderer.Draw(r.Batch, Color.White * Text4);
		}

		public void Update() {
			if (Program.Input[GameControl.Enter].Pressed) {
				Program.LoadStage(Program.Levels[Program.State.StageNum]);
				Program.Scene.Init();
				return;
			}

			if (Text1 < 1) {
				Text1 += FadeRate;
			} else if (Timer < 30) {
				Timer++;
			} else if (Text2 < 1) {
				Text2 += FadeRate;
			} else if (Timer < 60) {
				Timer++;
			} else if (Text3 < 1) {
				Text3 += FadeRate;
			} else if (Timer < 90) {
				Timer++;
			} else if (Text4 < 1) {
				Text4 += FadeRate;
			}
		}

	}
}
