using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox.maps.tiles;
using Toybox;
using static Toybox.maps.tiles.TilemapCollisions;
using Microsoft.Xna.Framework;

namespace JingleJam2024.entity.player {
	public class PlayerCollider {

		private const int HitboxSize = 6;

		private List<TileData> NearCollisions = new();
		private Rectangle VisualHitbox;

		public void Move(Player p, Vector2 speed) {
			if (speed == Vector2.Zero) return;

			var center = new Vector2(p.TrueX, p.TrueY);
			var diameter = HitboxSize + Math.Abs((int)speed.Length()) + 6;
			var hitbox = new Rectangle((int)center.X - diameter / 2, (int)center.Y - diameter / 2, diameter, diameter);
			NearCollisions.Clear();

			foreach (var c in Program.Scene.MechMap.GetCollisionsSubpixel(hitbox, Resources.Camera)) {
				if (c.Tile.Id != Constants.SolidTile && c.Tile.Id != Constants.TargetTile) continue;
				c.Bounds = Resources.Camera.Project(Camera.Space.Scaled, Camera.Space.Pixel, c.Bounds);
				NearCollisions.Add(c);
			}

			while (Math.Abs(speed.X) > 0) {
				float step = Math.Sign(speed.X);
				if (Math.Abs(speed.X) < 1) step = speed.X;
				float check = center.X + step;
				if (step < 0) check = center.X - 2;

				if (CheckCollision(new Vector2(check, center.Y))) {
					p.TrueX = (int)p.TrueX;
					speed.X = 0;
					break;
				}
				speed.X -= step;
				center.X += step;
				p.TrueX += step;
			}

			while (Math.Abs(speed.Y) > 0) {
				float step = Math.Sign(speed.Y);
				if (Math.Abs(speed.Y) < 1) step = speed.Y;
				float check = center.Y + step;
				if (step < 0) check = center.Y - 2;

				if (CheckCollision(new Vector2(center.X, check))) {
					p.TrueY = (int)p.TrueY;
					break;
				}
				speed.Y -= step;
				center.Y += step;
				p.TrueY += step;
			}
			p.X = (int)p.TrueX;
			p.Y = (int)p.TrueY;

			VisualHitbox = new Rectangle((int)(center.X - HitboxSize / 2), (int)(center.Y - HitboxSize / 2), HitboxSize, HitboxSize);
		}

		private bool CheckCollision(Vector2 center) {
			foreach (var c in NearCollisions) {
				if (CircleRectangleCollision(center, HitboxSize / 2, c.Bounds)) {
					return true;
				}
			}
			return false;
		}



		private bool CircleRectangleCollision(Vector2 circleCenter, float radius, Rectangle rectangle) {
			float testX = circleCenter.X;
			float testY = circleCenter.Y;

			// which edge is closest?
			if (circleCenter.X < rectangle.X) testX = rectangle.X;
			else if (circleCenter.X > rectangle.Right) testX = rectangle.Right;
			if (circleCenter.Y < rectangle.Y) testY = rectangle.Y;
			else if (circleCenter.Y > rectangle.Bottom) testY = rectangle.Bottom;

			// get distance from closest edges
			float distX = circleCenter.X - testX;
			float distY = circleCenter.Y - testY;
			float distance = (float)Math.Sqrt((distX * distX) + (distY * distY));

			// if the distance is less than the radius, collision!
			if (distance <= radius) {
				return true;
			}
			return false;
		}

		public void DrawHitbox(Renderer r, Camera c) {
			r.DrawRect(VisualHitbox, Color.Red, c, Camera.Space.Pixel);
		}

		public Rectangle Bounds {
			get {
				return VisualHitbox;
			}
		}

	}
}
