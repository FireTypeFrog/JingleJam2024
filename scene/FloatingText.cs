using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.utils.text;
using Toybox.utils.tween;

namespace JingleJam2024.scene {
	public class FloatingText {

		public int X, Y;
		public string Text;
		public Color Color;

		public static Text TextRenderer;

		public static StaticTween Tween = new StaticTween(EasingFunctions.OutSine, 40);
		private int StartingY;
		private int Timer = 0;
		private int Time = 60;
		private const int TravelDist = 40;

		public bool DestroyMe = false;

		public FloatingText(int x, int y, string text, Color c) {
			X = x;
			Y = y;
			Text = text;
			Color = c;
			StartingY = y;
		}

		public void Update() {
			Timer++;
			if (Timer > Time) {
				DestroyMe = true;
			}

			Y = StartingY - (int)(Tween.Get(Timer) * TravelDist);
		}

		public void Draw(Renderer r, Camera c) {
			var pos = new Point(X - c.X, Y - c.Y);
			TextRenderer.Draw(r.Batch, Color.Black, pos, Text);
			pos -= new Point(1, 1);
			TextRenderer.Draw(r.Batch, Color, pos, Text);
		}

	}
}
