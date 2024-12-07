using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Toybox.components;

namespace JingleJam2024 {
	public enum GameControl {
		Left, Right, Up, Down, SwapGraphic
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

		public static Point SolidTile = new Point(0, 0);
		public static Point TargetTile = new Point(1, 0);
		public static Point LeftTile = new Point(0, 1);
		public static Point DownTile = new Point(1, 1);
		public static Point UpLeftTile = new Point(2, 1);
		public static Point UpRightTile = new Point(3, 1);
		public static Point RightTile = new Point(0, 2);
		public static Point UpTile = new Point(1, 2);
		public static Point DownLeftTile = new Point(2, 2);
		public static Point DownRightTile = new Point(3, 2);

		public static Point OpenDoor = new Point(1, 0);
		public static Point ClosedDoor = new Point(0, 0);

		public const int DoorMoney = 10;
		public const float MoneyLossPerTick = 0.01f;
		public const int CarBumpMoneyLoss = 5;

		public const int CarSpeed = 2;
		public const int CarBumpTime = 50;
		public const float CarBumpSpin = 0.1f;
		public const int CarBumpHitbox = 10;

		public const int PlayerBumpHitbox = 10;
	}

}
