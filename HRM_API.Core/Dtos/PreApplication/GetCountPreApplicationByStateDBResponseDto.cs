namespace HRM_API.Core.Dtos.PreApplication
{
    public class GetCountPreApplicationByStateDBResponseDto
    {
        public string State { get; set; } = string.Empty;
        public int? Count { get; set; } = 0;
    }
}
