﻿using JingleJam2024.entity;
using JingleJam2024.entity.player;
using JingleJam2024.scene;
using Microsoft.Xna.Framework;
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

		public Rectangle CamBounds;
		public Player Player;
		public PresentSpawner Spawner;
		public Tilemap GraphicMap;
		public Tilemap FloorMap;
		public Tilemap DoorMap;
		public Tilemap MechMap;
		public List<TiledObject> Spawns;
		public MoneyDisplay MoneyDisplay;
		public DoorDisplay DoorDisplay;
		public LevelDisplay LevelDisplay;
		public List<Car> Cars = new();
		public List<FloatingText> FloatingText = new();

		public GameScene() {
			Player = new Player();
			MoneyDisplay = new MoneyDisplay(Program.Font);
			DoorDisplay = new DoorDisplay(Program.Font);
			LevelDisplay = new LevelDisplay(Program.Font);
		}

		public override void Init() {
			foreach (var spawn in Spawns) {
				spawn.Position = Resources.Camera.Project(Camera.Space.Scaled, Camera.Space.Pixel, spawn.Position);
				spawn.Size = Resources.Camera.Project(Camera.Space.Scaled, Camera.Space.Pixel, spawn.Size);
				if (spawn.Name == "player") {
					Player.TrueX = Player.X = spawn.Position.X;
					Player.TrueY = Player.Y = spawn.Position.Y;
					continue;
				} else if (spawn.Name == "bounds") {
					Spawner = new PresentSpawner(spawn.Bounds);
					CamBounds = spawn.Bounds;
					continue;
				} else if (spawn.Name == "car") {
					//Cars.Add(new Car(spawn.Position));
				}
			}

			var carSpawner = new CarSpawner();
			carSpawner.SpawnCars(this, 50);

			foreach (var car in Cars) {
				car.Init();
			}

			Player.Init();
			DoorDisplay.CountDoors(MechMap);
		}

		public override void Update() {
			Player.Update();
			Spawner.Update();
			foreach (var c in Cars) {
				c.Update();
			}

			var cam = Resources.Camera;
			cam.X = Player.X - cam.Width / 2;
			cam.Y = Player.Y - cam.Height / 2;
			if (cam.X < CamBounds.X) {
				cam.X = CamBounds.X;
			}
			if (cam.Y < CamBounds.Y) {
				cam.Y = CamBounds.Y;
			}
			if (cam.Bounds.Right > CamBounds.Right) {
				cam.X = CamBounds.Right - cam.Bounds.Width;
			}
			if (cam.Bounds.Bottom > CamBounds.Bottom) {
				cam.Y = CamBounds.Bottom - cam.Bounds.Height;
			}

			Program.State.Money -= Constants.MoneyLossPerTick;
			for (int i = 0; i < FloatingText.Count; i++) {
				FloatingText[i].Update();
				if (FloatingText[i].DestroyMe) {
					FloatingText.RemoveAt(i);
					i--;
				}
			}
		}

		public override void PostUpdate() {
		}

		public override void Draw(Renderer r, Camera c) {
			FloorMap.Draw(r, c);
			GraphicMap.Draw(r, c);
			DoorMap.Draw(r, c);
			Spawner.Draw(r, c);
			foreach (var car in Cars) {
				car.Draw(r, c);
			}
			Player.Draw(r, c);
			MoneyDisplay.Draw(r, c);
			DoorDisplay.Draw(r, c);
			LevelDisplay.Draw(r, c);
			foreach (var t in FloatingText) {
				t.Draw(r, c);
			}
		}

		public override void DrawHitboxes(Renderer r, Camera c) {
			MechMap.Draw(r, c);
			base.DrawHitboxes(r, c);
			Player.DrawHitbox(r, c);
			foreach (var car in Cars) {
				car.DrawHitbox(r, c);
			}
		}

		public override void PixelScaleChanged(int prevPixelScale, int newPixelScale) {
			
		}
	}
}
