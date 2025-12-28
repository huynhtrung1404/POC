using Microsoft.AspNetCore.Mvc;
using Poc.Mediator;

namespace Poc.Api.Configurations;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
}