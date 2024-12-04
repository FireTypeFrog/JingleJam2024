﻿using Microsoft.Xna.Framework;
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

		public Player() {
		}

		public void Draw(Renderer r, Camera c) {
			Trail.Draw(r, c);

			var dest = new Rectangle(X, Y, Size.X * c.PixelScale, Size.Y * c.PixelScale);
			var origin = new Vector2((float)r.Blank.Width / 2, (float)r.Blank.Height / 2);
			dest = c.Project(Camera.Space.Pixel, Camera.Space.Render, dest);
			r.Batch.Draw(r.Blank, dest, null, Color.White, Angle, origin, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);

			Shooter.Draw(r, c);
		}

		public void Update() {
			Controller.Update(this);
			Collider.Move(this, Speed);
			Trail.Update(this);
			Shooter.Update(this);
			Bumper.Update(this);
		}

		public void DrawHitbox(Renderer r, Camera c) {
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
