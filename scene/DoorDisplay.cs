using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.maps.tiles;
using Toybox.utils.text;

namespace JingleJam2024.scene {
	public class DoorDisplay {

		public Text TextRenderer;

		public DoorDisplay(Font f) {
			TextRenderer = new Text(f);
		}

		public void Draw(Renderer r, Camera c) {
			TextRenderer.Content = $"Presents Delivered: {Program.State.DoorsClosed}/{Program.State.AllDoors}";
			TextRenderer.Position = new Point((c.Width / 2) - (TextRenderer.GetSize().X / 2), 10);
			TextRenderer.Draw(r.Batch, Color.Black);
			TextRenderer.Position -= new Point(1, 1);
			TextRenderer.Draw(r.Batch, Color.White);
		}

		public void CountDoors(Tilemap mechmap) {
			Program.State.AllDoors = 0;
			for (int x = 0; x < mechmap.Columns; x++) {
				for (int y = 0; y < mechmap.Rows; y++) {
					var tile = mechmap.Get(x, y);
					if (!tile.HasValue) continue;
					if (tile.Value.Id == Constants.TargetTile) {
						Program.State.AllDoors++;
					}
				}
			}
			Program.State.DoorsClosed = 0;
		}

	}
}
