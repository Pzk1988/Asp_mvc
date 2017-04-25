namespace SportsStore.Domain.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public override bool Equals(object obj)
        {
            Product product = obj as Product;

            if(ProductID != product.ProductID)
            {
                return false;
            }
            else if(Name != product.Name)
            {
                return false;
            }
            else if (Description != product.Description)
            {
                return false;
            }
            else if (Price != product.Price)
            {
                return false;
            }
            else if (Category != product.Category)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override int GetHashCode()
        {
            return 33 * ProductID.GetHashCode();
        }
    }
}
