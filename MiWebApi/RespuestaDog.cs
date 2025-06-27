// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class RespuestaDog
    {
        public List<string> message { get; set; }
        public string status { get; set; }
    }