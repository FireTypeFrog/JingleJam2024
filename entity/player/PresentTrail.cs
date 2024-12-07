using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;

namespace JingleJam2024.entity.player {
	public class PresentTrail {

		public List<PresentTrailPresent> Content = new();
		public int MaxLength = 3;
		public Point PickupSize = new Point(30, 30);
		private Point PickupZonePos;

		private List<Point> PlayerPositions = new();
		private const int FramesBetweenPositions = 10;

		public PresentTrail() {
		}

		public void Update(Player p) {
			PickupZonePos = new Point(p.X - PickupSize.X / 2, p.Y - PickupSize.Y / 2);
			var pickup = Program.Scene.Park.FindCollision(new Rectangle(PickupZonePos, PickupSize));
			if (pickup != null) {
				Pickup(pickup);
			}

			UpdatePlayerPositions(p);

			for (int i = 0; i < Content.Count; i++) {
				if (Content[i].DestroyMe) {
					Content.RemoveAt(i);
					i--;
					continue;
				}

				var pos = (i + 1) * FramesBetweenPositions;
				var target = PlayerPositions[0];
				if (pos < PlayerPositions.Count) {
					target = PlayerPositions[pos];
				}
				Content[i].Update(target);
			}
		}

		private void UpdatePlayerPositions(Player p) {
			var pos = new Point(p.X, p.Y);

			if (PlayerPositions.Count > 0 && pos == PlayerPositions[0]) return;

			PlayerPositions.Insert(0, pos);
			if (PlayerPositions.Count > (MaxLength + 1) * FramesBetweenPositions) {
				PlayerPositions.RemoveAt(PlayerPositions.Count - 1);
			}
		}

		public void Pickup(PresentPickup p) {
			if (Content.Count >= MaxLength) return;

			p.DestroyMe = true;
			Content.Add(new PresentTrailPresent(new Point(p.X, p.Y), p.Graphic));
		}

		public void Draw(Renderer r, Camera c) {
			foreach (var p in Content) {
				p.Draw(r, c);
			}
		}

		public void DrawHitbox(Renderer r, Camera c) {
			r.DrawRect(new Rectangle(PickupZonePos, PickupSize), Color.Pink * 0.5f, c, Camera.Space.Pixel);
		}

	}
}
