using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.utils.text;

namespace JingleJam2024.scene {
	public class GameCompleteMessage {

		public Text TextRenderer;

		private float Text1 = 0;
		private float Text2 = 0;
		private float Text3 = 0;
		private int Timer = 0;
		private float FadeRate = 0.05f;

		public GameCompleteMessage(Font f) {
			TextRenderer = new Text(f);
		}

		public void Draw(Renderer r, Camera c) {
			TextRenderer.Content = $"Congratulations!";
			TextRenderer.Position = new Point(c.Width / 3, 50);
			TextRenderer.Draw(r.Batch, Color.Black * Text1);
			TextRenderer.Position -= new Point(1, 1);
			TextRenderer.Draw(r.Batch, Color.White * Text1);

			TextRenderer.Content = $"You saved your budget!";
			TextRenderer.Position = new Point(c.Width / 3, 80);
			TextRenderer.Draw(r.Batch, Color.Black * Text2);
			TextRenderer.Position -= new Point(1, 1);
			TextRenderer.Draw(r.Batch, Color.White * Text2);

			TextRenderer.Content = $"...and also Christmas.";
			TextRenderer.Position = new Point(c.Width / 3, 100);
			TextRenderer.Draw(r.Batch, Color.Black * Text3);
			TextRenderer.Position -= new Point(1, 1);
			TextRenderer.Draw(r.Batch, Color.White * Text3);
		}

		public void Update() {
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
			}
		}

	}
}
