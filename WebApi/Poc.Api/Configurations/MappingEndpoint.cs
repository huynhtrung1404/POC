using Poc.Api.Endpoints;

namespace Poc.Api.Configurations;

public static class MappingEndpoint
{
    public static WebApplication MapEndpointDisplay(this WebApplication web)
    {
        web.MapAwsOrganizationEndpoint();
        web.MapAwsAccountEndpoint();
        return web;
    }
}