using Core.Gameplay.Fight.Models;

namespace Core.Gameplay.Fight.Api {
	public interface IFightViewInitializeService {
		void InitFighters (FighterModel redFighter, FighterModel blueFighter);
	}
}
