using Microsoft.AspNetCore.Mvc;

namespace Poc.Api.Configurations;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
}