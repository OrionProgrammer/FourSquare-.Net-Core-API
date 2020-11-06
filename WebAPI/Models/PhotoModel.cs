namespace WebApi.Models
{
    public class PhotoModel
    {

        public string Id { get; set; }
        public string VenueId { get; set; }
        public string VenueName { get; set; }
        public byte[] Image { get; set; }
        public string ImageCredit { get; set; }
        public int LocationId { get; set; }
    }
}
