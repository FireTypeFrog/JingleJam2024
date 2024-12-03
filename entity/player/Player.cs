using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.components;
using Toybox.components.colliders;
using Toybox.maps.tiles;
using Toybox.utils.math;
using static Toybox.maps.tiles.TilemapCollisions;

namespace JingleJam2024.entity.player {
	public class Player {

		public int X, Y;
		public float TrueX, TrueY;
		public Point Size = new Point(15, 10);
		public Vector2 Speed;
		public float Angle = 0;

		public PlayerController Controller = new();
		public PlayerCollider Collider = new();

		public Player() {
		}

		public void Draw(Renderer r, Camera c) {
			var dest = new Rectangle(X, Y, Size.X, Size.Y);
			var origin = new Vector2((float)r.Blank.Width / 2, (float)r.Blank.Height / 2);
			dest = c.Project(Camera.Space.Pixel, Camera.Space.Render, dest);
			r.Batch.Draw(r.Blank, dest, null, Color.White, Angle, origin, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
		}

		public void Update() {
			Controller.Update(this);
			Collider.Move(this, Speed);
		}

	}
}
