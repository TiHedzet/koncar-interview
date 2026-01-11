namespace Koncar.Interview.Server.Presentation.Api.Endpoints;

using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiControllerBase : ControllerBase;
