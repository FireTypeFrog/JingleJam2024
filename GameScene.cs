using JingleJam2024.entity.player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.maps;
using Toybox.maps.tiles;
using Toybox.scenes;
using Toybox.tiled;

namespace JingleJam2024
{
    public class GameScene:Scene {

		public Player Player;
		public Tilemap GraphicMap;
		public List<TiledObject> Spawns;

		public GameScene() {
			Player = new Player();
		}

		public override void Init() {
			foreach (var spawn in Spawns) {
				spawn.Position = Resources.Camera.Project(Camera.Space.Scaled, Camera.Space.Pixel, spawn.Position);
				if (spawn.Name == "player") {
					Player.TrueX = Player.X = spawn.Position.X;
					Player.TrueY = Player.Y = spawn.Position.Y;
					continue;
				}
			}
		}

		public override void Update() {
			Player.Update();

			Resources.Camera.X = Player.X - Resources.Camera.Width / 2;
			Resources.Camera.Y = Player.Y - Resources.Camera.Height / 2;
		}

		public override void PostUpdate() {
		}

		public override void Draw(Renderer r, Camera c) {
			GraphicMap.Draw(r, c);
			Player.Draw(r, c);
		}

		public override void DrawHitboxes(Renderer r, Camera c) {
			base.DrawHitboxes(r, c);
		}

		public override void PixelScaleChanged(int prevPixelScale, int newPixelScale) {
			
		}
	}
}
