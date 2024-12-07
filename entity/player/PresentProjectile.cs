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
		public PresentGraphic Graphic;

		public PresentProjectile(Point start, Point target, float speed, PresentGraphic graphic) {
			Pos = new Vector2(start.X, start.Y);
			Speed = (target - start).ToVector2();
			Speed.Normalize();
			Speed *= speed;
			Graphic = graphic;
		}

		public void Draw(Renderer r, Camera c) {
			Graphic.Draw(r, c, (int)Pos.X, (int)Pos.Y);
		}

		public void Update() {
			Pos += Speed;
			foreach (var c in Program.Scene.MechMap.GetCollisionsSubpixel(Bounds, Resources.Camera)) {
				if (Resources.Camera.Project(Camera.Space.Scaled, Camera.Space.Pixel, c.Bounds).Intersects(Bounds)) {
					if (c.Tile.Id == Constants.SolidTile) {
						DestroyMe = true;
					}
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
			var tile = Program.Scene.DoorMap.Get(pos);
			Program.Scene.DoorMap.Set(pos.X, pos.Y, new Tilemap.Tile(new Point(tile.Value.Id.X + 2, tile.Value.Id.Y), Microsoft.Xna.Framework.Graphics.SpriteEffects.None));
			Program.State.Money += Constants.DoorMoney;
			Program.State.DoorsClosed++;

			var textpos = Resources.Camera.Project(Camera.Space.Scaled, Camera.Space.Pixel, pos);
			textpos.X += (Program.Scene.MechMap.TileWidth * Resources.Camera.PixelScale) / 3;
			Program.Scene.FloatingText.Add(new scene.FloatingText(textpos.X, textpos.Y, "+" + Constants.DoorMoney.ToString(), Color.LightGreen));

			if (Program.State.DoorsClosed >= Program.State.AllDoors) {
				Program.Scene.StageComplete = true;
			}
		}

	}
}
