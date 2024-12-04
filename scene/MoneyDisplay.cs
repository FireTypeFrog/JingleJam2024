using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.utils.text;

namespace JingleJam2024.scene {
	public class MoneyDisplay {

		public Text TextRenderer;

		public MoneyDisplay(Font f) {
			TextRenderer = new Text(f);
		}

		public void Draw(Renderer r, Camera c) {
			TextRenderer.Draw(r.Batch, Color.Red, new Point(10, 10), $"${(int)Program.State.Money}");
		}

	}
}
