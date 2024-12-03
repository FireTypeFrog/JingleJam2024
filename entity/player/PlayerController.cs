using Microsoft.Xna.Framework;
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
			var speed = p.Speed.Length();

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

	}
}
