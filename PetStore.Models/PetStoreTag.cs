﻿using System.Text.Json.Serialization;

namespace PetStore.Models
{
    public class PetStoreTag
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}