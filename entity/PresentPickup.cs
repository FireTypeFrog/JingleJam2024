using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;

namespace JingleJam2024.entity {

	public class PresentPickup {

		public const int Size = 4;

		public int X, Y;

		public void Draw(Renderer r, Camera c) {
			r.DrawRect(new Rectangle(X, Y, Size, Size), Color.White, c, Camera.Space.Pixel);
		}

	}
}
