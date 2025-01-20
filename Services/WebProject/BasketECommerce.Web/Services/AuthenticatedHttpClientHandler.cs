using BasketECommerce.Web.Services.AuthenticationService;
using System.Net.Http.Headers;

namespace BasketECommerce.Web.Services;

public class AuthenticatedHttpClientHandler : DelegatingHandler
{
    private readonly ITokenProvider _tokenProvider;

    public AuthenticatedHttpClientHandler(IHttpContextAccessor httpContextAccessor, ITokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        //var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

        var token = _tokenProvider.GetToken();

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
