using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.graphic;
using Toybox.maps.tiles;
using Toybox.utils;
using static Toybox.maps.tiles.TilemapCollisions;

namespace JingleJam2024.entity {
	public class Car {

		public Direction Direction = Direction.Neutral;
		public Point Position;
		public Point Size = new Point(5, 3);
		public float Angle = 0;
		public Point Speed;
		private Point HitboxSize = new Point(2, 2);
		public static SpriteMap Graphic;

		private Point LastTileCollision;
		private int TileCollisionShrink = 7;

		public Car(Point pos) {
			Position = pos;
			if (Graphic == null) {
				Graphic = Program.Sprites["car"];
			}
		}

		public void Update() {
			Position += Speed;
			//Angle = (float)Math.Atan2(Speed.Y, Speed.X);

			var tile = GetTileCollision();
			CollideTile(tile);
			CollidePlayer();
		}

		public void Init() {
			var grid = Program.Scene.MechMap.TileWidth * Resources.Camera.PixelScale;
			int x = (int)Math.Floor((float)Position.X / grid) * grid + (grid / 2);
			int y = (int)Math.Floor((float)Position.Y / grid) * grid + (grid / 2);
			Position = new Point(x, y);
		}

		public void Draw(Renderer r, Camera c) {
			Graphic.GetDrawRects(0, Position.X, Position.Y, out var source, out var dest);
			dest = new Rectangle(dest.X, dest.Y, dest.Width * c.PixelScale, dest.Height * c.PixelScale);
			var origin = new Vector2(source.Width / 2, source.Height / 2);
			dest = c.Project(Camera.Space.Pixel, Camera.Space.Render, dest);
			r.Batch.Draw(Graphic.Graphic, dest, source, Color.White, Angle, origin, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
		}

		public Rectangle Bounds {
			get { return new Rectangle(Position, HitboxSize); }
		}

		private TileData GetTileCollision() {
			TileData collision = null;
			var bounds = Bounds;
			foreach (var t in Program.Scene.MechMap.GetCollisionsSubpixel(bounds, Resources.Camera)) {
				t.Bounds.Inflate(-TileCollisionShrink, -TileCollisionShrink);
				t.Bounds = Resources.Camera.Project(Camera.Space.Scaled, Camera.Space.Pixel, t.Bounds);
				if (!t.Bounds.Intersects(bounds)) continue;

				if (t.Tile.Id == Constants.UpLeftTile || t.Tile.Id == Constants.UpRightTile || t.Tile.Id == Constants.DownLeftTile || t.Tile.Id == Constants.DownRightTile) {
					collision = t;
					continue;
				}
				if (collision == null) {
					collision = t;
				}
			}
			return collision;
		}

		private void CollideTile(TileData t) {
			if (t == null) return;
			if (LastTileCollision == t.Tile.Id) return;
			LastTileCollision = t.Tile.Id;

			if (t.Tile.Id == Constants.LeftTile) {
				GoLeft();
				return;
			} else if (t.Tile.Id == Constants.RightTile) {
				GoRight();
				return;
			} else if (t.Tile.Id == Constants.UpTile) {
				GoUp();
				return;
			} else if (t.Tile.Id == Constants.DownTile) {
				GoDown();
				return;
			}

			if (Resources.Random.Next(2) == 0) {
				//Turn
				if (t.Tile.Id == Constants.UpLeftTile) {
					if (Direction == Direction.Up) GoLeft();
					else GoUp();
				} else if (t.Tile.Id == Constants.UpRightTile) {
					if (Direction == Direction.Up) GoRight();
					else GoUp();
				} else if (t.Tile.Id == Constants.DownLeftTile) {
					if (Direction == Direction.Down) GoLeft();
					else GoDown();
				} else if (t.Tile.Id == Constants.DownRightTile) {
					if (Direction == Direction.Down) GoRight();
					else GoDown();
				}
			} else {
				//Go Straight
				if (t.Tile.Id == Constants.UpLeftTile) {
					if (Direction == Direction.Up) GoUp();
					else GoLeft();
				} else if (t.Tile.Id == Constants.UpRightTile) {
					if (Direction == Direction.Up) GoUp();
					else GoRight();
				} else if (t.Tile.Id == Constants.DownLeftTile) {
					if (Direction == Direction.Down) GoDown();
					else GoLeft();
				} else if (t.Tile.Id == Constants.DownRightTile) {
					if (Direction == Direction.Down) GoDown();
					else GoRight();
				}
			}
		}

		private void GoUp() {
			Speed = new Point(0, -Constants.CarSpeed);
			Angle = (float)(Math.PI + Math.PI / 2);
			if (Direction != Direction.Up && Direction != Direction.Down) {
				int gridsize = Program.Scene.MechMap.TileWidth * Resources.Camera.PixelScale;
				Position.X = (int)(Math.Floor((float)Position.X / gridsize) * gridsize) + (gridsize / 2);
			}
			Direction = Direction.Up;
		}

		private void GoDown() {
			Speed = new Point(0, Constants.CarSpeed);
			Angle = (float)(Math.PI / 2);
			if (Direction != Direction.Up && Direction != Direction.Down) {
				int gridsize = Program.Scene.MechMap.TileWidth * Resources.Camera.PixelScale;
				Position.X = (int)(Math.Floor((float)Position.X / gridsize) * gridsize) + (gridsize / 2);
			}
			Direction = Direction.Down;
		}

		private void GoLeft() {
			Speed = new Point(-Constants.CarSpeed, 0);
			Angle = (float)(Math.PI);
			if (Direction != Direction.Left && Direction != Direction.Right) {
				int gridsize = Program.Scene.MechMap.TileWidth * Resources.Camera.PixelScale;
				Position.Y = (int)(Math.Floor((float)Position.Y / gridsize) * gridsize) + (gridsize / 2);
			}
			Direction = Direction.Left;
		}

		private void GoRight() {
			Speed = new Point(Constants.CarSpeed, 0);
			Angle = 0;
			if (Direction != Direction.Left && Direction != Direction.Right) {
				int gridsize = Program.Scene.MechMap.TileWidth * Resources.Camera.PixelScale;
				Position.Y = (int)(Math.Floor((float)Position.Y / gridsize) * gridsize) + (gridsize / 2);
			}
			Direction = Direction.Right;
		}

		public void DrawHitbox(Renderer r, Camera c) {
			r.DrawRect(Bounds, Color.DarkRed, c, Camera.Space.Pixel);
			r.DrawRect(BumpHitbox, Color.Red * 0.5f, c, Camera.Space.Pixel);
		}

		public void CollidePlayer() {
			var bounds = BumpHitbox;
			if (bounds.Intersects(Program.Scene.Player.BumpHitbox)) {
				Program.Scene.Player.Bumper.Bump(Speed.ToVector2(), Program.Scene.Player);
			}
		}

		public Rectangle BumpHitbox {
			get {
				return new Rectangle(Position.X - Constants.CarBumpHitbox / 2, Position.Y - Constants.CarBumpHitbox / 2, Constants.CarBumpHitbox, Constants.CarBumpHitbox);
			}
		}

	}
}
