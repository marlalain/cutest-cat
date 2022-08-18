using CutestCat.Models;
using CutestCat.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CutestCat.Services;

public class CatRankingService {
	private readonly IMongoCollection<Cat> _cats;

	public CatRankingService(CatRankingRepository catRankingRepository) {
		_cats = catRankingRepository.GetCatsCollection();
	}

	public IEnumerable<Cat> GetRanking() {
		return _cats.Find(cat => true).ToList();
	}

	public Cat? GetPlace(long ranking) {
		var maybeCat = _cats.Find(cat => cat.Ranking == ranking);

		return maybeCat.CountDocuments().Equals(0) ? null : maybeCat.First();
	}

	public Cat? GetCat(Cat cat) {
		var maybeCat = _cats.Find(db => db.Id.Equals(cat.Id));

		return maybeCat.CountDocuments().Equals(0)
			? null
			: maybeCat.First();
	}

	public ActionResult<VotingResult>? Vote(string winnerId, string loserId) {
		var winnerDatabase = _cats.Find(cat => cat.Id.Equals(winnerId));
		var loserDatabase = _cats.Find(cat => cat.Id.Equals(loserId));

		var notFound = winnerDatabase.CountDocuments().Equals(0)
		               || loserDatabase.CountDocuments().Equals(0);

		return (notFound
			? null
			: new ObjectResult(winnerDatabase.First().WinAgainst(loserDatabase.First())))!;
	}
}