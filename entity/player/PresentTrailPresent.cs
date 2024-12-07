using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.utils.tween;

namespace JingleJam2024.entity.player {
	public class PresentTrailPresent {

		public const int Size = 4;

		public int X, Y;
		private int TweenTimer = 0;
		private Point TweenStart;
		public bool DestroyMe = false;
		public PresentGraphic Graphic;

		private static StaticTween Tween = new StaticTween(EasingFunctions.InOutBounce, 30);

		public PresentTrailPresent(Point pos, PresentGraphic graphic) {
			TweenStart = pos;
			X = pos.X;
			Y = pos.Y;
			Graphic = graphic;
		}

		public void Draw(Renderer r, Camera c) {
			Graphic.Draw(r, c, X, Y);
		}

		public void Update(Point target) {
			if (TweenTimer < Tween.Frames) {
				TweenTimer++;
				var p = target - TweenStart;
				X = (int)(TweenStart.X + p.X * Tween.Get(TweenTimer));
				Y = (int)(TweenStart.Y + p.Y * Tween.Get(TweenTimer));
			} else {
				X = target.X;
				Y = target.Y;
			}
		}

	}
}
