namespace BookDemo.Models
{
    public class Book
    {
        public int Id { get; set; } =default(int);
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
    }
}
