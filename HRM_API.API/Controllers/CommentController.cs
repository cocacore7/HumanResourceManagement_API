using HRM_API.Application.Services;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.General;
using HRM_API.Core.Dtos.Comment;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController(CommentService commentService) : ControllerBase
    {
        private readonly CommentService _commentService = commentService;

        [HttpGet("GetComments")]
        public async Task<IActionResult> GetComments([FromQuery] int? preApplicationId, [FromQuery] string estado = "")
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            var response = await _commentService.GetCommentsAsync(preApplicationId, estado);
            if (response == null) { BadRequest(ApiResponses.Fail("COMMENTS_NOT_FOUND", "No se encontraron comentarios asociados al estado o pre aplicación solicitada")); }
            return Ok(ApiResponses.Ok(response, "OK", "COMMENTS_FOUND"));
        }

        [HttpPost("SetComments")]
        public async Task<IActionResult> SetComments([FromBody] SetCommentsRequestDto request)
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
                return Unauthorized(ApiResponses.Fail("UNAUTHORIZED", "Token inválido"));

            LoginDBResponseDto user = new()
            {
                IdUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                Name = User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                RoleId = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty
            };

            var response = await _commentService.SetCommentsAsync(request, user);
            if (response == null) { BadRequest(ApiResponses.Fail("COMMENT_NOT_SAVED", "Error al registrar comentario")); }
            return Ok(ApiResponses.Ok(response, "OK", "COMMENT_NOT_SAVED"));
        }
    }
}
