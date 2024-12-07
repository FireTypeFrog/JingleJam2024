using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.components;
using Toybox.components.colliders;
using Toybox.maps.tiles;
using Toybox.utils.math;
using static Toybox.maps.tiles.TilemapCollisions;

namespace JingleJam2024.entity.player {
	public class Player {

		public int X, Y;
		public float TrueX, TrueY;
		public Point Size = new Point(8, 5);
		public Vector2 Speed;
		public float Angle = 0;

		public PlayerController Controller = new();
		public PlayerCollider Collider = new();
		public PresentTrail Trail = new();
		public PresentShooter Shooter = new();
		public PlayerBumper Bumper = new();
		public PlayerGraphic1 Graphic1;
		public PlayerGraphic2 Graphic2;
		public static Texture2D Shadow;
		private Point ShadowPos;
		private Point StartingPos;

		public bool GraphicSwap = true;

		public Player() {
			Graphic1 = new PlayerGraphic1(this);
			Graphic2 = new PlayerGraphic2(this);
		}

		public void Init() {
			Graphic1.Init();
			Graphic2.Init();
		}

		public void Draw(Renderer r, Camera c) {
			if (Program.Scene.StageComplete || Program.Scene.StageStarting) {
				var source = Shadow.Bounds;
				var dest = new Rectangle(ShadowPos.X, ShadowPos.Y, source.Width * c.PixelScale, source.Height * c.PixelScale);
				r.Draw(Shadow, dest, source, Color.White, c, Camera.Space.Pixel, SpriteEffects.None);
			}

			bool drawtrailfirst = true;
			if (Angle > Math.PI) {
				drawtrailfirst = false;
			}
			if (drawtrailfirst)	Trail.Draw(r, c);

			if (GraphicSwap) {
				Graphic2.Draw(r, c);
			} else {
				Graphic1.Draw(r, c);
			}

			if (!drawtrailfirst) Trail.Draw(r, c);

			Shooter.Draw(r, c);
		}

		public void Update() {
			if (Program.Scene.StageComplete) {
				if (ShadowPos == Point.Zero) {
					ShadowPos = BumpHitbox.Location - new Point(5 * Resources.Camera.PixelScale);
				}
				Angle += 0.2f;
				Speed = new Vector2(0, Speed.Y - 0.1f);
				Y += (int)Speed.Y;
				return;
			} else if (Program.Scene.StageStarting) {
				if (ShadowPos == Point.Zero) {
					ShadowPos = BumpHitbox.Location - new Point(5 * Resources.Camera.PixelScale);
					StartingPos = new Point(X, Y);
					Y = Program.Scene.CamBounds.Y - 20;
				}
				Angle += 0.2f;
				Speed = new Vector2(0, 6);
				Y += (int)Speed.Y;
				if (Y >= StartingPos.Y) {
					ShadowPos = Point.Zero;
					Program.Scene.StageStarting = false;
					Speed = Vector2.Zero;
				}
				return;
			}

			Controller.Update(this);
			Collider.Move(this, Speed);
			Trail.Update(this);
			Shooter.Update(this);
			Bumper.Update(this);

			if (Program.Input[GameControl.SwapGraphic].Pressed) {
				GraphicSwap = !GraphicSwap;
			}
		}

		public void DrawHitbox(Renderer r, Camera c) {
			var dest = new Rectangle(X, Y, Size.X * c.PixelScale, Size.Y * c.PixelScale);
			var origin = new Vector2((float)r.Blank.Width / 2, (float)r.Blank.Width / 2);
			dest = c.Project(Camera.Space.Pixel, Camera.Space.Render, dest);
			r.Batch.Draw(r.Blank, dest, null, Color.Gray * 0.5f, Angle, origin, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);

			Collider.DrawHitbox(r, c);
			Trail.DrawHitbox(r, c);
			r.DrawRect(BumpHitbox, Color.DarkRed * 0.5f, c, Camera.Space.Pixel);
		}

		public Rectangle BumpHitbox {
			get {
				return new Rectangle(X - Constants.PlayerBumpHitbox / 2, Y - Constants.PlayerBumpHitbox / 2, Constants.PlayerBumpHitbox, Constants.PlayerBumpHitbox);
			}
		}

	}
}
