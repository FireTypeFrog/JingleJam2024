using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.utils.text;

namespace JingleJam2024.scene {
	public class TitleScreen {

		public static Text TextRenderer;
		private string Text1 = "Santa-Dwarf on a Budget";
		private string Text2 = "Save Christmas by delivering all the presents\r\nbefore you go bankrupt.\r\n \r\nPress Enter to Start :)";
		private string Text3 = "Created by FireTypeFrog & Mattda";
		private string Text4 = "Music by Denys Kyshchuk: https://www.audiocoffee.net/";
		private Point Pos1 = new Point(60, 80);
		private Point Pos2 = new Point(40, 150);
		private Point Pos3 = new Point(100, 300);
		private Point Pos4 = new Point(100, 320);

		public void Draw(Renderer r, Camera c) {
			r.DrawRect(new Rectangle(0, 0, c.Width, c.Height), Color.Black, c, Camera.Space.Pixel);

			TextRenderer.Draw(r.Batch, Color.White, Pos1, Text1, 4);
			TextRenderer.Draw(r.Batch, Color.Red, Pos2, Text2, 2);
			TextRenderer.Draw(r.Batch, Color.Black, Pos1 - new Point(1, 1), Text1, 4);
			TextRenderer.Draw(r.Batch, Color.LightGreen, Pos2 - new Point(1, 1), Text2, 2);

			TextRenderer.Draw(r.Batch, Color.White, Pos3, Text3);
			TextRenderer.Draw(r.Batch, Color.White, Pos4, Text4);
		}

		public void Update() {
			if (Program.Input[GameControl.Enter].Pressed) {
				Program.ShowTitle = false;
			}
		}
	}
}
