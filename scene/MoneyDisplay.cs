﻿using Microsoft.Xna.Framework;
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
			TextRenderer.Content = $"Remaining Funds: ${(int)Program.State.Money}";
			TextRenderer.Position = new Point((c.Width / 2) - (TextRenderer.GetSize().X / 2), 30);
			TextRenderer.Draw(r.Batch, Color.Black);
			TextRenderer.Position -= new Point(1, 1);
			TextRenderer.Draw(r.Batch, Color.White);
		}

	}
}
