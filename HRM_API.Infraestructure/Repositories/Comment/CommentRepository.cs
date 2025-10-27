using Dapper;
using HRM_API.Core.Dtos.Comment;
using HRM_API.Core.Interfaces.Comment;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HRM_API.Infraestructure.Repositories.Comment
{
    public class CommentRepository(IConfiguration configuration) : ICommentRepository
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<List<GetCommentsDBResponseDto>?> GetCommentsAsync(int? preApplicationId, string estado)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));
            if (!string.IsNullOrEmpty(estado))
            {
                var sql = @"SELECT c.IdComment [IdComment], u.Name [Author], r.RoleName [Role], c.CommentText [Text], c.CreatedAt [DateCreated]
                        FROM HRM_DB.reclutamiento.Comment c
                        INNER JOIN HRM_DB.reclutamiento.Users u ON u.IdUser = c.AuthorUserId
                        INNER JOIN HRM_DB.reclutamiento.Role r ON r.IdRole = u.RoleId
                        WHERE c.PreApplicationId = @preApplicationId
                        AND c.CommentStatus = @estado";
                var result = await connection.QueryAsync<GetCommentsDBResponseDto?>(sql, new { preApplicationId, estado });
                return [.. result];
            }
            else
            {
                var sql = @"SELECT c.IdComment [IdComment], u.Name [Author], r.RoleName [Role], c.CommentText [Text], c.CreatedAt [DateCreated]
                        FROM HRM_DB.reclutamiento.Comment c
                        INNER JOIN HRM_DB.reclutamiento.Users u ON u.IdUser = c.AuthorUserId
                        INNER JOIN HRM_DB.reclutamiento.Role r ON r.IdRole = u.RoleId
                        WHERE c.PreApplicationId = @preApplicationId";
                var result = await connection.QueryAsync<GetCommentsDBResponseDto?>(sql, new { preApplicationId, estado });
                return [.. result];
            }
        }

        public async Task<int?> SetCommentsAsync(SetCommentsDBRequestDto request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("localDB"));

            var sql = @"INSERT INTO HRM_DB.reclutamiento.Comment
                        (PreApplicationId, AuthorUserId, CommentText, CommentStatus, CreatedAt)
                        VALUES
                        (@PreApplicationId,@AuthorUserId,@CommentText,@CommentStatus,@CreatedAt);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
            var result = await connection.QueryFirstOrDefaultAsync<int?>(sql, new 
            { 
                request.PreApplicationId, 
                request.AuthorUserId, 
                request.CommentText, 
                request.CommentStatus, 
                request.CreatedAt
            });

            return result;
        }
    }
}