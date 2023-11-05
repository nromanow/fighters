using Core.Meta.Fight.Models;

namespace Core.Meta.Fight.Api {
	public interface IFightViewInitializeService {
		void InitFighters (FighterModel redFighter, FighterModel blueFighter);
	}
}
