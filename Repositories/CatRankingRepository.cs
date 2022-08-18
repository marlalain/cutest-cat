using CutestCat.Configs;
using CutestCat.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CutestCat.Repositories;

public class CatRankingRepository {
	private readonly IMongoCollection<Cat> _cats;

	public CatRankingRepository(IOptions<CatRankingDatabaseConfig> config) {
		var client = new MongoClient(config.Value.ConnectionString);
		var database = client.GetDatabase(config.Value.DatabaseName);
		_cats = database.GetCollection<Cat>(config.Value.CollectionName);
	}

	public IMongoCollection<Cat> GetCatsCollection() {
		return _cats;
	}
}