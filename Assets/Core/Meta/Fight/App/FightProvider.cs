using Core.Meta.Fight.Api;
using Core.Meta.Fight.Models;
using System;
using UniRx;

namespace Core.Meta.Fight.App {
	public class FightProvider : IFightProvider, IDisposable {
		private const float KICK_DISTANCE = 0.2f;
		private const int DELTA_SCORE = 10;
		private const float WIN_DISTANCE = 2.6f;
		
		private const float RED_START_POS = -1.9f;
		private const float BLUE_START_POS = 1.9f;
		
		public ReactiveCommand win { get; } = new();
		
		public IReadOnlyReactiveProperty<int> redScore => _redScore;
		public IReadOnlyReactiveProperty<int> blueScore => _blueScore;

		private readonly ReactiveProperty<int> _redScore = new(0);
		private readonly ReactiveProperty<int> _blueScore = new(0);

		private FighterModel _redFighter;
		private FighterModel _blueFighter;

		public void RegisterRedFighter (FighterModel redFighter) {
			_redFighter = redFighter;
			_redFighter.InitStartPosition(RED_START_POS);
			
			_redScore.Value = 0;
		}
		
		public void RegisterBlueFighter (FighterModel blueFighter) {
			_blueFighter = blueFighter;
			_blueFighter.InitStartPosition(BLUE_START_POS);
			
			_blueScore.Value = 0;
		}
		
		public void RegisterRedKick () {
			_redScore.Value += (int)(KICK_DISTANCE * DELTA_SCORE);

			_redFighter.UpdatePositionByStep(KICK_DISTANCE);
			_blueFighter.UpdatePositionByStep(KICK_DISTANCE);
			
			if (_redFighter.position.Value >= WIN_DISTANCE) {
				win.Execute();
			}
		}

		public void RegisterBlueKick () {
			_blueScore.Value += (int)(KICK_DISTANCE * DELTA_SCORE);

			_redFighter.UpdatePositionByStep(-KICK_DISTANCE);
			_blueFighter.UpdatePositionByStep(-KICK_DISTANCE);
			
			if (_blueFighter.position.Value <= -WIN_DISTANCE) {
				win.Execute();
			}
		}

		public void Dispose () {
			win?.Dispose();
			_redScore?.Dispose();
			_blueScore?.Dispose();
			_redFighter?.Dispose();
			_blueFighter?.Dispose();
		}
	}
}
