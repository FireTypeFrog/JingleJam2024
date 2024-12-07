using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.utils.text;

namespace JingleJam2024.scene {
	public class LevelDisplay {

		public Text TextRenderer;

		public LevelDisplay(Font f) {
			TextRenderer = new Text(f);
		}

		public void Draw(Renderer r, Camera c) {
			TextRenderer.Content = $"Stage: {Program.State.StageNum}";
			TextRenderer.Position = new Point(10, 10);
			TextRenderer.Draw(r.Batch, Color.Black);
			TextRenderer.Position -= new Point(1, 1);
			TextRenderer.Draw(r.Batch, Color.White);

			TextRenderer.Content = $"Christmas Cheer: {Program.State.CheerLevel}";
			TextRenderer.Position = new Point(10, 30);
			TextRenderer.Draw(r.Batch, Color.Black);
			TextRenderer.Position -= new Point(1, 1);
			TextRenderer.Draw(r.Batch, Color.White);
		}

	}
}
