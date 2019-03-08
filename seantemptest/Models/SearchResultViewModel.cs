using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace seantemptest.Models
{
    [JsonConverter(typeof(Helpers.JsonPathConverter))]
    public class SearchResultViewModel
    {
        [JsonProperty("text")]
        public string Tweet { get; set; }
        [JsonProperty("user.name")]
        public string Author { get; set; }
        [JsonProperty("user.profile_image_url")]
        public string Picture { get; set; }
        [JsonProperty("created_at")]
        public string TimeStamp { get; set; }
        [JsonProperty("retweet_count")]
        public int TotalRetweet { get; set; }
        [JsonProperty("favorite_count")]
        public int TotalFavorate { get; set; }
        [JsonProperty("entities.urls")]
        public List<UrlViewModel> Urls { get; set; }
    }
    public class UrlViewModel
    {
        [JsonProperty("expanded_url")]
        public string TweetUrl { get; set; }
    }
    public class RootNodeViewModel
    {
        public List<SearchResultViewModel> statuses { get; set; }
    }
}