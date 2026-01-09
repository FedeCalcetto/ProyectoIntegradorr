using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.Dtos
{
    public class MpOAuthTokenDto
    {
        [JsonPropertyName("access_token")]
        public string access_token { get; set; } = string.Empty;

        [JsonPropertyName("token_type")]
        public string token_type { get; set; } = string.Empty;

        [JsonPropertyName("expires_in")]
        public int expires_in { get; set; }

        [JsonPropertyName("refresh_token")]
        public string refresh_token { get; set; } = string.Empty;

        [JsonPropertyName("scope")]
        public string scope { get; set; } = string.Empty;

        [JsonPropertyName("user_id")]
        public long user_id { get; set; }
    }
}
