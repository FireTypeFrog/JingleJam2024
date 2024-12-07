using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.graphic;

namespace JingleJam2024.entity {
	public class PresentGraphic {

		public int Frame;
		public static SpriteMap Sprite;

		public PresentGraphic() {
			if (Sprite == null) {
				Sprite = Program.Sprites["presents"];
			}
			Frame = Resources.Random.Next(Sprite.Frames.Count);
		}

		public void Draw(Renderer r, Camera c, int x, int y) {
			Sprite.GetDrawRects(Frame, x, y, out var source, out var dest);
			dest = new Rectangle(dest.X, dest.Y, dest.Width * c.PixelScale, dest.Height * c.PixelScale);
			r.Draw(Sprite.Graphic, dest, source, Color.White, c, Camera.Space.Pixel, Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
		}


	}
}
