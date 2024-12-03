using JingleJam2024.entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;

namespace JingleJam2024.scene {
	public class PresentSpawner {

		private const int MaxPresents = 10;
		private const int TimeBetweenSpawns = 100;

		public List<PresentPickup> Content = new();
		public int Timer = 0;
		public Rectangle Bounds;

		public PresentSpawner(Rectangle bounds) {
			Bounds = bounds;
		}

		public void Update() {
			Timer++;
			if (Timer > TimeBetweenSpawns) {
				if (Content.Count < MaxPresents) {
					SpawnPresent();
				}
				Timer = 0;
			}
		}

		public void Draw(Renderer r, Camera c) {
			foreach (var p in Content) {
				p.Draw(r, c);
			}
		}

		public void SpawnPresent() {
			var p = new PresentPickup();
			Content.Add(p);

			p.X = Resources.Random.Next(Bounds.X, Bounds.Right - PresentPickup.Size);
			p.Y = Resources.Random.Next(Bounds.Y, Bounds.Bottom - PresentPickup.Size);
		}

	}
}
