namespace ServiceLifetimeDemonstration;

public class GuidTrimmer : IGuidTrimmer
{
	private readonly GuidService _guidService;

	public GuidTrimmer(GuidService guidService)
	{
		_guidService = guidService;
	}

	public string TrimmedGuid()
	{
		var guid = _guidService.GetGuid();
		var trimmed = guid.Replace("-", string.Empty);
		return trimmed;
	}
}
