using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CutestCat.Models;

public class Cat {
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; }

	public long Ranking { get; set; }

	public int Elo { get; set; } = 1000;

	public int TimesVoted { get; set; } = 0;

	private void _winAgainst(Cat cat) {
		Elo += 25 * (1 - _expectedRatingAgainst(cat));
	}

	private void _loseAgainst(Cat cat) {
		Elo += 25 * (0 - _expectedRatingAgainst(cat));
	}

	private int _expectedRatingAgainst(Cat cat) {
		return 1 / 1 + 10 * int.Parse(cat.Ranking - Ranking + "") / 400;
	}

	public VotingResult WinAgainst(Cat cat) {
		_winAgainst(cat);
		cat._loseAgainst(this);

		return new VotingResult {
			loser = cat,
			winner = this
		};
	}

	public VotingResult LoseAgainst(Cat cat) {
		_loseAgainst(cat);
		cat._winAgainst(this);

		return new VotingResult {
			loser = this,
			winner = cat
		};
	}
}