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
			foreach (var c in Program.Scene.MechMap.GetCollisionsSubpixel(Bounds, Resources.Camera)) {
				if (Resources.Camera.Project(Camera.Space.Scaled, Camera.Space.Pixel, c.Bounds).Intersects(Bounds)) {
					DestroyMe = true;
					if (c.Tile.Id == Constants.TargetTile) {
						HitTarget(c.Bounds.Location);
					}
				}
			}
		}

		public Rectangle Bounds {
			get {
				return new Rectangle(Pos.ToPoint(), Size);
			}
		}

		private void HitTarget(Point pos) {
			Program.Scene.MechMap.Set(pos.X, pos.Y, new Tilemap.Tile(Constants.SolidTile, Microsoft.Xna.Framework.Graphics.SpriteEffects.None));
			Program.Scene.GraphicMap.Set(pos.X, pos.Y, new Tilemap.Tile(Constants.ClosedDoor, Microsoft.Xna.Framework.Graphics.SpriteEffects.None));
			Program.State.Money += Constants.DoorMoney;
		}

	}
}
