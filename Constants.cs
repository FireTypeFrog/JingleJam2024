using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Toybox.components;

namespace JingleJam2024 {
	public enum GameControl {
		Left, Right, Up, Down
	}

	public enum CollisionType {
		Clear, Solid
	}

	public static class Constants {
		private static Dictionary<Point, CollisionType> TileCollisionTypes = new Dictionary<Point, CollisionType>() {
			{ new Point(0, 0), CollisionType.Solid }
		};

		public static CollisionType TileToCollisionType(Point p) {
			if (TileCollisionTypes.ContainsKey(p)) return TileCollisionTypes[p];
			return CollisionType.Clear;
		}

		public static bool IsSolid(Collision c, Point reference) {
			if (c.Type == (int)CollisionType.Solid) return true;
			return false;
		}
	}

}
