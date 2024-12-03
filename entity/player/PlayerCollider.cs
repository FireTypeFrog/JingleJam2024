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

		public void Move(Player p, Vector2 speed) {
			if (speed == Vector2.Zero) return;

			var center = new Vector2(p.TrueX + p.Size.X / 2, p.TrueY + p.Size.Y / 2);
			var diameter = HitboxSize + Math.Abs((int)speed.Length()) + 4;
			var hitbox = new Rectangle((int)center.X - diameter / 2, (int)center.Y - diameter / 2, diameter, diameter);
			NearCollisions.Clear();

			foreach (var c in Program.Scene.GraphicMap.GetCollisionsSubpixel(hitbox, Resources.Camera)) {
				c.Bounds = Resources.Camera.Project(Camera.Space.Scaled, Camera.Space.Pixel, c.Bounds);
				NearCollisions.Add(c);
			}

			while (Math.Abs(speed.X) >= 1) {
				var step = Math.Sign(speed.X);
				if (CheckCollision(new Vector2(center.X + step, center.Y))) {
					p.TrueX = (int)p.TrueX;
					speed.X = 0;
					break;
				}
				speed.X -= step;
				center.X += step;
				p.TrueX += step;
			}
			if (speed.X != 0) {
				if ((int)(p.TrueX + speed.X) != (int)p.TrueX) {
					var step = Math.Sign(speed.X);
					if (CheckCollision(new Vector2(center.X + step, center.Y))) {
						p.TrueX = (int)p.TrueX;
					} else {
						p.TrueX += speed.X;
						center.X += step;
					}
				} else {
					p.TrueX += speed.X;
				}
			}

			while (Math.Abs(speed.Y) >= 1) {
				var step = Math.Sign(speed.Y);
				if (CheckCollision(new Vector2(center.X, center.Y + step))) {
					p.TrueY = (int)p.TrueY;
					break;
				}
				speed.Y -= step;
				center.Y += step;
				p.TrueY += step;
			}
			p.X = (int)p.TrueX;
			p.Y = (int)p.TrueY;
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

	}
}
