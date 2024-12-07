using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;

namespace JingleJam2024.entity.player {
	public class PresentShooter {

		public List<PresentProjectile> Content = new();
		public float ProjectileSpeed = 6;

		public PresentShooter() {
		}

		public void Update(Player p) {
			if (Resources.MouseInput.LeftPress) {
				var mousepos = Resources.Camera.Project(Camera.Space.Screen, Camera.Space.Pixel, Resources.MouseInput.Position);
				ShootPresent(p, mousepos);
			}

			for (int i = 0; i < Content.Count; i++) {
				Content[i].Update();
				if (Content[i].DestroyMe) {
					Content.RemoveAt(i);
					i--;
				}
			}
		}

		public void Draw(Renderer r, Camera c) {
			foreach (var p in Content) {
				p.Draw(r, c);
			}
		}

		public void ShootPresent(Player p, Point pos) {
			if (p.Trail.Content.Count == 0) return;

			p.Trail.Content[0].DestroyMe = true;
			var present = new PresentProjectile(new Point(p.X, p.Y), pos, ProjectileSpeed, p.Trail.Content[0].Graphic);
			Content.Add(present);
		}

	}
}
