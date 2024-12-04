using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;

namespace JingleJam2024.entity.player {
	public class PlayerBumper {


		public int BumpTimer;

		public void Bump(Vector2 speed, Player p) {
			if (BumpTimer > 0) {
				Program.State.Money -= Constants.CarBumpMoneyLoss;
			}
			BumpTimer = Constants.CarBumpTime;

			var speedAngle = (float)Math.Atan2(speed.Y, speed.X);
			speedAngle += (float)(Resources.Random.NextDouble() * Math.PI) - (float)(Math.PI / 2);
			var xspeed = (float)Math.Cos(speedAngle);
			var yspeed = (float)Math.Sin(speedAngle);
			p.Speed = new Vector2(xspeed, yspeed);
		}

		public void Update(Player p) {
			if (BumpTimer <= 0) return;
			BumpTimer--;
			p.Angle += Constants.CarBumpSpin;
		}

	}
}
