namespace WebApi.Entities
{
    public class Photo
    {
        public string  Id { get; set; }
        public string VenueId { get; set; }
        public byte[] Image { get; set; }
        public string ImageCredit { get; set; }
    }
}