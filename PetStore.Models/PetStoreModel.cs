using System.Text.Json.Serialization;

namespace PetStore.Models
{
    public class PetStoreModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        
        [JsonPropertyName("category")]
        public PetStoreCategory Category { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("photoUrls")]
        public List<string> PhotoUrls { get; set; }
        [JsonPropertyName("tags")]
        public List<PetStoreTag> Tags { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
    public enum PetStatus { available, pending, sold}

}