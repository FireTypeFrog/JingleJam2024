using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingleJam2024 {
	public static class SoundPlayer {

		public static Song Music;
		public static SoundEffect Win;
		public static SoundEffect Target;
		public static SoundEffect Spin;
		public static SoundEffect Pickup;
		public static SoundEffect Gameover;
		public static SoundEffect Bump;

		public static void Init() {
			MediaPlayer.Play(Music);
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Volume = 0.2f;
		}

		
	}
}
