using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;

namespace JingleJam2024.scene {
	public class CarSpawner {

		public void SpawnCars(GameScene s, int num) {
			List<Point> spawnPoints = new();
			int gridSize = s.MechMap.TileWidth;
			for (int x = 0; x < s.MechMap.Columns; x++) {
				for (int y = 0; y < s.MechMap.Rows; y++) {
					var tile = s.MechMap.Get(x, y);
					if (!tile.HasValue) continue;
					if (tile.Value.Id == Constants.UpTile || tile.Value.Id == Constants.DownTile || tile.Value.Id == Constants.LeftTile || tile.Value.Id == Constants.RightTile) {
						spawnPoints.Add(new Point(x * gridSize, y * gridSize));
					}
				}
			}

			for (int i = 0; i < num; i++) {
				Spawn(s, spawnPoints);
			}
		}

		private void Spawn(GameScene s, List<Point> points) {
			var num = Resources.Random.Next(points.Count);
			var pos = points[num];
			pos = Resources.Camera.Project(Camera.Space.Scaled, Camera.Space.Pixel, pos);
			s.Cars.Add(new entity.Car(pos));
			points.RemoveAt(num);
		}

	}
}
