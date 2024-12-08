using JingleJam2024.scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JingleJam2024 {
	public class GameState {

		public const int StartingMoney = 100;

		public float Money = StartingMoney;
		public int DoorsClosed;
		public int AllDoors;
		public int StageNum = 0;
		public int CheerLevel = 1;

		public CheerMeter CheerMeter = new();

		public bool GameComplete = false;

		public bool ShowTutorial = true;

	}
}
