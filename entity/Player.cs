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

		private const float TurnSpeed = 0.1f;
		private const float MaxSpeed = 2;

		public Point Size = new Point(10, 15);
		public float Angle = 0;

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
			if (Program.Input[GameControl.Left].Down) {
				Angle -= TurnSpeed;
			}
			if (Program.Input[GameControl.Right].Down) {
				Angle += TurnSpeed;
			}

			

			base.Update();
		}

	}
}
