﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toybox;
using Toybox.graphic;

namespace JingleJam2024.entity {

	public class PresentPickup {

		public const int Size = 4;

		public int X, Y;
		public bool DestroyMe = false;
		public PresentGraphic Graphic;

		public PresentPickup() {
			Graphic = new PresentGraphic();
		}

		public void Draw(Renderer r, Camera c) {
			if (DestroyMe) return;
			Graphic.Draw(r, c, X, Y);
		}

		public Rectangle Bounds {
			get {
				return new Rectangle(X, Y, Size, Size);
			}
		}

	}
}
