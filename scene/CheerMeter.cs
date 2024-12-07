using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.utils.text;

namespace JingleJam2024.scene {
	public class CheerMeter {

		public Point Size = new Point(100, 10);
		public const int CheerCost = 30;
		public static Text TextRenderer;

		public int MoneySpent = 0;

		public void Draw(Renderer r, Camera c) {
			var dest = new Rectangle((c.Width / 2) - (Size.X / 2), 100, Size.X, Size.Y);
			r.DrawRect(dest, Color.White, c, Camera.Space.Render);
			dest.Location -= new Point(1, 1);
			dest.Width = (int)((float)MoneySpent / CheerCost * Size.X);
			r.DrawRect(dest, Color.Red, c, Camera.Space.Render);

			TextRenderer.Content = $"Christmas Cheer: {Program.State.CheerLevel}";
			TextRenderer.Position = new Point(c.Width / 2, 80);
			TextRenderer.Position.X -= TextRenderer.GetSize().X / 2;
			TextRenderer.Draw(r.Batch, Color.Black);
			TextRenderer.Position -= new Point(1, 1);
			TextRenderer.Draw(r.Batch, Color.White);
		}

		public void Update() {
			if (Program.State.Money <= 0) {

				return;
			}

			Program.State.Money--;
			MoneySpent++;
			if (MoneySpent >= CheerCost) {
				Program.State.CheerLevel++;
				MoneySpent = 0;
			}
		}

	}
}
