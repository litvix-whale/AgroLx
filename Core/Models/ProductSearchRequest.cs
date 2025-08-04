namespace Core.DTOs;

public class ProductSearchRequest
{
    public string? SearchQuery { get; set; }
    public int? CategoryId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public Guid? UserId { get; set; }
    
    // sorting
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
    
    // pagination
    public int Skip { get; set; }
    public int Take { get; set; }

}