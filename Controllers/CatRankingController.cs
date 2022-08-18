using CutestCat.Models;
using CutestCat.Services;
using Microsoft.AspNetCore.Mvc;

namespace CutestCat.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class CatRankingController : ControllerBase {
	private readonly CatRankingService _cats;
	private readonly ILogger<CatRankingController> _logger;

	public CatRankingController(ILogger<CatRankingController> logger, CatRankingService cats) {
		_logger = logger;
		_cats = cats;
	}

	[HttpGet(Name = "GetRanking")]
	public IEnumerable<Cat> GetRanking() {
		return _cats.GetRanking();
	}

	[HttpGet("/place/{ranking:long}", Name = "GetPlace")]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public ActionResult<Cat> GetPlace(long ranking) {
		var cat = _cats.GetPlace(ranking);

		return cat == null
			? Problem(statusCode: 404, detail: "Could not find a cat on this ranking.")
			: Ok(cat);
	}

	[HttpGet("/vote/{winnerId}/{loserId}")]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public ActionResult<VotingResult> Vote(string winnerId, string loserId) {
		var result = _cats.Vote(winnerId, loserId);

		return result == null
			? Problem(statusCode: 404, detail: "Could not find cat.")
			: Ok(result);
	}
}