using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.graphic;

namespace JingleJam2024.entity.player {
	public class PlayerGraphic1 {

		public static Texture2D Sprite;
		private Player Player;

		public PlayerGraphic1(Player p) {
			Player = p;
		}

		public void Init() {
			Sprite = Program.PlayerSprite;
		}

		public void Draw(Renderer r, Camera c) {

			var source = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
			var dest = new Rectangle(Player.X, Player.Y, source.Width * c.PixelScale, source.Height * c.PixelScale);
			var origin = new Vector2((float)source.Width / 2, (float)source.Height / 2);
			dest = c.Project(Camera.Space.Pixel, Camera.Space.Render, dest);
			r.Batch.Draw(Sprite, dest, null, Color.White, Player.Angle, origin, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
		}

	}
}
