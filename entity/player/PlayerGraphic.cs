using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.graphic;

namespace JingleJam2024.entity.player {
	public class PlayerGraphic {

		public static SpriteMap Sprite;
		private Player Player;

		public PlayerGraphic(Player p) {
			Player = p;
		}

		public void Init() {
			Sprite = Program.Sprites["player"];
		}

		public void Draw(Renderer r, Camera c) {
			Sprite.GetDrawRects(0, Player.X, Player.Y, out var source, out var dest);
			dest = new Rectangle(dest.X, dest.Y, dest.Width * c.PixelScale, dest.Height * c.PixelScale);
			r.Draw(Sprite.Graphic, dest, source, Color.White, c, Camera.Space.Pixel, Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
		}

	}
}
