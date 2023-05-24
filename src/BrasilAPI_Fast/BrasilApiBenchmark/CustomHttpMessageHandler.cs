using System.Net;

namespace BrasilApiBenchmark;

public class CustomHttpMessageHandler : HttpMessageHandler
{
	public required string Content { get; init; }
	
	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Accepted)
		{
			Content = new StringContent(Content)
		});
	}
}
