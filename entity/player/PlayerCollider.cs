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
			var center = new Vector2(p.TrueX + p.Size.X / 2, p.TrueY + p.Size.Y / 2);
			var diameter = HitboxSize + Math.Abs((int)speed.Length()) + 4;
			var hitbox = new Rectangle((int)center.X - diameter / 2, (int)center.Y - diameter / 2, diameter, diameter);
			NearCollisions.Clear();

			foreach (var c in Program.Scene.GraphicMap.GetCollisionsSubpixel(hitbox, Resources.Camera)) {
				c.Bounds = Resources.Camera.Project(Camera.Space.Scaled, Camera.Space.Pixel, c.Bounds);
				NearCollisions.Add(c);
			}

			while (speed.X >= 1) {
				if (CheckCollision(new Vector2(center.X + 1, center.Y))) {
					p.TrueX = (int)p.TrueX;
					break;
				}
				speed.X--;
				center.X++;
				p.X++;
			}
			while (speed.X <= -1) {
				if (CheckCollision(new Vector2(center.X - 1, center.Y))) {
					p.TrueX = (int)p.TrueX;
					break;
				}
				speed.X++;
				center.X--;
				p.X--;
			}
			while (speed.Y >= 1) {
				if (CheckCollision(new Vector2(center.X, center.Y + 1))) {
					p.TrueY = (int)p.TrueY;
					break;
				}
				speed.Y--;
				center.Y++;
				p.Y++;
			}
			while (speed.Y <= -1) {
				if (CheckCollision(new Vector2(center.X, center.Y - 1))) {
					p.TrueY = (int)p.TrueY;
					break;
				}
				speed.Y++;
				center.Y--;
				p.Y--;
			}
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
			// temporary variables to set edges for testing
			float testX = circleCenter.X;
			float testY = circleCenter.Y;

			// which edge is closest?
			if (circleCenter.X < rectangle.X) testX = rectangle.X;      // test left edge
			else if (circleCenter.X > rectangle.Right) testX = rectangle.Right;   // right edge
			if (circleCenter.Y < rectangle.Y) testY = rectangle.Y;      // top edge
			else if (circleCenter.Y > rectangle.Bottom) testY = rectangle.Bottom;   // bottom edge

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
