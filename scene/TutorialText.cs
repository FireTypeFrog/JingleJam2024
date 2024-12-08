using JingleJam2024.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.utils.text;

namespace JingleJam2024.scene {
	public class TutorialText {

		public static Texture2D Graphic;
		private string Text0 = "Use WASD to move.";
		private string Text1 = "Santa's elves are airdropping presents\r\nPick them up.";
		private string Text2 = "Throw presents with Left Mouse.\r\nDeliver them to every open door.";
		private string Text3 = "Complete your deliveries before Santa runs out of money.";
		private Rectangle DownArrow = new Rectangle(0, 0, 5, 7);
		private Rectangle UpArrow = new Rectangle(5, 0, 5, 7);
		private Rectangle Circle = new Rectangle(10, 0, 7, 7);
		public int Step = 0;
		public Point Pos;
		private float Opacity = 0;
		private bool FadeIn = true;
		private bool FadeOut = false;
		private const float FadeRate = 0.05f;
		public static Text TextRenderer;
		private int Timer = 0;
		private bool NoValidTarget = false;

		public void Update() {
			if (!Program.State.ShowTutorial) return;

			if (FadeIn) {
				Opacity += FadeRate;
				if (Opacity >= 1) {
					FadeIn = false;
				}
			} else if (FadeOut) {
				Opacity -= FadeRate;
				if (Opacity <= 0) {
					FadeOut = false;
					Step++;
					if (Step < 4) {
						FadeIn = true;
					} else {
						Program.State.ShowTutorial = false;
					}
					return;
				}
			}

			if (Step == 0) {
				Pos = new Point(Program.Scene.Player.X, Program.Scene.Player.Y);
				if (Program.Input[GameControl.Up].Down) {
					FadeOut = true;
				}
			} else if (Step == 1) {
				if (Program.Scene.Player.Trail.Content.Count > 0) {
					FadeOut = true;
				} else {
					PresentPickup p = null;
					foreach (var present in Program.Scene.Spawner.Content) {
						var bounds = Resources.Camera.Bounds;
						bounds.Inflate(-30, -30);
						if (bounds.Contains(present.Bounds.Location)) {
							p = present;
							break;
						}
					}
					if (p != null) {
						Pos = p.Bounds.Location;
						NoValidTarget = false;
					} else {
						NoValidTarget = true;
					}
				}
			} else if (Step == 2) {
				if (Program.State.DoorsClosed > 0) {
					FadeOut = true;
				} else {
					NoValidTarget = true;
					for (int x = 0; x < Program.Scene.MechMap.Columns; x++) {
						for (int y = 0; y < Program.Scene.MechMap.Rows; y++) {
							var tile = Program.Scene.MechMap.Get(x, y);
							if (!tile.HasValue) continue;
							if (tile.Value.Id != Constants.TargetTile) continue;
							var pos = new Point(x * Program.Scene.MechMap.TileWidth * Resources.Camera.PixelScale, y * Program.Scene.MechMap.TileHeight * Resources.Camera.PixelScale);
							var bounds = Resources.Camera.Bounds;
							bounds.Inflate(-20, -20);
							if (!bounds.Contains(pos)) continue;
							Pos = pos;
							NoValidTarget = false;
							break;
						}
					}
					
				}
			} else if (Step == 3) {
				Pos = new Point(Resources.Camera.Width / 2, 80);
				Timer++;
				if (Timer > 200) {
					FadeOut = true;
				}
			}
		}

		public void Draw(Renderer r, Camera c) {
			if (!Program.State.ShowTutorial) return;
			if (NoValidTarget) return;
			if (Step > 3) return;
			Point pos = Pos;
			string text = "";

			if (Step == 0) {
				text = Text0;
				pos = c.Project(Camera.Space.Pixel, Camera.Space.Render, pos);
			} else if (Step == 1) {
				var source = Circle;
				var dest = new Rectangle(pos.X - 2 * c.PixelScale, pos.Y, source.Width * c.PixelScale, source.Height * c.PixelScale);
				r.Draw(Graphic, dest, source, Color.White * Opacity, c, Camera.Space.Pixel, SpriteEffects.None);
				text = Text1;
				pos = c.Project(Camera.Space.Pixel, Camera.Space.Render, pos);
			} else if (Step == 2) {
				var source = DownArrow;
				var dest = new Rectangle(pos.X + 5 * c.PixelScale, pos.Y, source.Width * c.PixelScale, source.Height * c.PixelScale);
				r.Draw(Graphic, dest, source, Color.White * Opacity, c, Camera.Space.Pixel, SpriteEffects.None);
				text = Text2;
				pos = c.Project(Camera.Space.Pixel, Camera.Space.Render, pos);
			} else if (Step == 3) {
				var source = UpArrow;
				var dest = new Rectangle(pos.X + 5 * c.PixelScale, pos.Y - 12 * c.PixelScale, source.Width * c.PixelScale, source.Height * c.PixelScale);
				r.Draw(Graphic, dest, source, Color.White * Opacity, c, Camera.Space.Render, SpriteEffects.None);
				text = Text3;
			}
			var size = TextRenderer.GetSize(text);
			pos.X -= size.X / 2;
			pos.Y -= size.Y;

			if (pos.X < 5) pos.X = 5;
			if (pos.Y < 50) pos.Y = 50;
			if (pos.X + size.X > c.Width) pos.X = c.Width - size.X;
			if (pos.Y + size.Y > c.Height) pos.Y = c.Height - size.Y;

			TextRenderer.Draw(r.Batch, Color.Black * Opacity, pos, text);
			pos -= new Point(1, 1);
			TextRenderer.Draw(r.Batch, Color.White * Opacity, pos, text);
		}
	}
}
