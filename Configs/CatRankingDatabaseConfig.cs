namespace CutestCat.Configs; 

public class CatRankingDatabaseConfig {
	public string ConnectionString { get; set; } = null!;
	public string DatabaseName { get; set; } = null!;
	public string CollectionName { get; set; } = null!;
}