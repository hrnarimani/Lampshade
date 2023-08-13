namespace ShopManagement.Application.Contracts.ProductCategory;

public class ProductCategoryViewModel
{
    public string Name { get;  set; }
    public string Picture { get;  set; }
    public long Id { get;  set; }
    public string CreationDate { get; set; }
    public long ProductsCount { get; set; }
}