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

		private static StaticTween Tween = new StaticTween(EasingFunctions.InOutBounce, 30);

		public PresentTrailPresent(Point pos) {
			TweenStart = pos;
			X = pos.X;
			Y = pos.Y;
		}

		public void Draw(Renderer r, Camera c) {
			r.DrawRect(new Rectangle(X, Y, Size, Size), Color.White, c, Camera.Space.Pixel);
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
