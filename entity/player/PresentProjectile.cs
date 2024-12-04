using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.maps.tiles;

namespace JingleJam2024.entity.player {
	public class PresentProjectile {

		public Vector2 Pos;
		public Point Size = new Point(4, 4);
		public Vector2 Speed;
		public bool DestroyMe = false;

		public PresentProjectile(Point start, Point target, float speed) {
			Pos = new Vector2(start.X, start.Y);
			Speed = (target - start).ToVector2();
			Speed.Normalize();
			Speed *= speed;
		}

		public void Draw(Renderer r, Camera c) {
			r.DrawRect(new Rectangle(Pos.ToPoint(), Size), Color.White, c, Camera.Space.Pixel);
		}

		public void Update() {
			Pos += Speed;
			foreach (var c in Program.Scene.GraphicMap.GetCollisionsSubpixel(Bounds, Resources.Camera)) {
				c.Bounds = Resources.Camera.Project(Camera.Space.Scaled, Camera.Space.Pixel, c.Bounds);
				if (c.Bounds.Intersects(Bounds)) {
					DestroyMe = true;
				}
			}
		}

		public Rectangle Bounds {
			get {
				return new Rectangle(Pos.ToPoint(), Size);
			}
		}

	}
}
