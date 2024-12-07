using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace JingleJam2024.entity.player {
	public class PlayerController {

		private const float TurnSpeed = 0.05f;
		private const float MaxSpeed = 3;
		private const float Accel = 0.1f;
		private const float Decel = 0.05f;

		public void Update(Player p) {
			if (p.Bumper.BumpTimer > 0) {
				return;
			}

			var speed = p.Speed.Length();

			var speedAngle = (float)Math.Atan2(p.Speed.Y, p.Speed.X);
			speedAngle = NormalizeAngle(speedAngle);
			p.Angle = NormalizeAngle(p.Angle);
			if (!(Math.Abs(speedAngle - p.Angle) < 0.01f)) {
				speed = -speed;
			}

			if (Program.Input[GameControl.Left].Down && speed != 0) {
				p.Angle -= TurnSpeed;
			}
			if (Program.Input[GameControl.Right].Down && speed != 0) {
				p.Angle += TurnSpeed;
			}
			if (Program.Input[GameControl.Up].Down) {
				speed += Accel;
				if (speed > MaxSpeed) speed = MaxSpeed;
			} else if (Program.Input[GameControl.Down].Down) {
				speed -= Accel;
				if (speed < -MaxSpeed) speed = -MaxSpeed;
			} else {
				if (speed > 0) {
					speed -= Decel;
					if (speed < 0) speed = 0;
				} else if (speed < 0) {
					speed += Decel;
					if (speed > 0) speed = 0;
				}
			}

			var xspeed = speed * (float)Math.Cos(p.Angle);
			var yspeed = speed * (float)Math.Sin(p.Angle);
			p.Speed = new Vector2(xspeed, yspeed);
		}

		public float NormalizeAngle(float r) {
			var twopi = Math.PI + Math.PI;
			var result = r % twopi;
			if (result > 0) return (float)result;
			return (float)(result + twopi);
		}

	}
}
