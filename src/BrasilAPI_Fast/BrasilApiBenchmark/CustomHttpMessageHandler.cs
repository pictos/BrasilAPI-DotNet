using System.Net;

namespace BrasilApiBenchmark;

internal class CustomHttpMessageHandler : HttpMessageHandler
{
	internal required string Content { get; init; }
	
	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Accepted)
		{
			Content = new StringContent(Content)
		});
	}
}
