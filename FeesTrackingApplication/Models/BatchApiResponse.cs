namespace FeesTrackingApplication.Models
{
    public class BatchApiResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public List<Batches> Data { get; set; }
    }
}
