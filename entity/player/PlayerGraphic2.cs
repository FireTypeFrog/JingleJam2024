using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.graphic;

namespace JingleJam2024.entity.player {
	public class PlayerGraphic2 {

		public static SpriteMap Sprite;
		private Player Player;

		public PlayerGraphic2(Player p) {
			Player = p;
		}

		public void Init() {
			Sprite = Program.Sprites["player"];
		}

		public void Draw(Renderer r, Camera c) {
			var frameAngle = (Math.PI * 2) / 16;
			var frame = (Player.Angle) / frameAngle;
			frame = Math.Round(frame);
			frame = frame % 16;
			while (frame < 0) frame += 16;

			Sprite.GetDrawRects((int)frame, Player.X, Player.Y, out var source, out var dest);
			dest = new Rectangle(dest.X, dest.Y, dest.Width * c.PixelScale, dest.Height * c.PixelScale);
			dest.X -= dest.Width / 2;
			dest.Y -= dest.Height / 2;
			r.Draw(Sprite.Graphic, dest, source, Color.White, c, Camera.Space.Pixel, Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
		}

	}
}
