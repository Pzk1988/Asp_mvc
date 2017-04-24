using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Models
{
    public class CartViewModel
    {
        public string returnUrl { get; set; }
        public Cart cart { get; set; }
    }
}