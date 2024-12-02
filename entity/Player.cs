using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.components.colliders;
using Toybox.utils.math;

namespace JingleJam2024.entity {
	public class Player:Entity {

		private const float TurnSpeed = 0.05f;
		private const float MaxSpeed = 3;
		private const float Accel = 0.1f;
		private const float Decel = 0.05f;

		public Point Size = new Point(15, 10);
		public float Angle = 0;
		public float MoveSpeed = 0;
		public float TrueX;
		public float TrueY;

		public Player() {
			Collider = new NoCollider(this);
			Hitbox = new Hitbox(this);
		}

		protected override void DoDraw(Renderer r, Camera c) {
			var dest = new Rectangle(X, Y, Size.X, Size.Y);
			var origin = new Vector2((float)r.Blank.Width / 2, (float)r.Blank.Height / 2);
			dest = c.Project(Camera.Space.Pixel, Camera.Space.Render, dest);
			r.Batch.Draw(r.Blank, dest, null, Color.White, Angle, origin, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
		}

		public override void Update() {
			if (Program.Input[GameControl.Left].Down && MoveSpeed != 0) {
				Angle -= TurnSpeed;
			}
			if (Program.Input[GameControl.Right].Down && MoveSpeed != 0) {
				Angle += TurnSpeed;
			}
			if (Program.Input[GameControl.Up].Down) {
				MoveSpeed += Accel;
				if (MoveSpeed > MaxSpeed) MoveSpeed = MaxSpeed;
			} else if (Program.Input[GameControl.Down].Down) {
				MoveSpeed -= Accel;
				if (MoveSpeed < -MaxSpeed) MoveSpeed = -MaxSpeed;
			} else {
				if (MoveSpeed > 0) {
					MoveSpeed -= Decel;
					if (MoveSpeed < 0) MoveSpeed = 0;
				} else if (MoveSpeed < 0) {
					MoveSpeed += Decel;
					if (MoveSpeed > 0) MoveSpeed = 0;
				}
			}

			var xspeed = MoveSpeed * (float)Math.Cos(Angle);
			var yspeed = MoveSpeed * (float)Math.Sin(Angle);
			TrueX += xspeed;
			TrueY += yspeed;
			X = (int)TrueX;
			Y = (int)TrueY;
		}

	}
}
