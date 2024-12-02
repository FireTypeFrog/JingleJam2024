using JingleJam2024.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.scenes;

namespace JingleJam2024 {
	public class GameScene:Scene {

		public Player Player;

		public GameScene() {
			Player = new Player() { X = 200, Y = 200 };
		}

		public override void Init() {
		}

		public override void Update() {
			Player.Update();
		}

		public override void PostUpdate() {
		}

		public override void Draw(Renderer r, Camera c) {
			Player.Draw(r, c);
		}

		public override void DrawHitboxes(Renderer r, Camera c) {
			base.DrawHitboxes(r, c);
		}

		public override void PixelScaleChanged(int prevPixelScale, int newPixelScale) {
			
		}
	}
}
