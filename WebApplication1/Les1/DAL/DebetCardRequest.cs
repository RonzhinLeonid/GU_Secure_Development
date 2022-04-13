namespace Les1.DAL
{
    public class DebetCardRequest
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Holder { get; set; }
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
    }
}
