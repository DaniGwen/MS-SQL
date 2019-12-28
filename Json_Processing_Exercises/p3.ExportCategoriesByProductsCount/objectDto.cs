using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace p3.ExportCategoriesByProductsCount
{
    public class objectDto
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "productsCount")]
        public int ProductsCount { get; set; }

        [JsonProperty(PropertyName = "averagePrice")]
        public decimal AveragePrice { get; set; }

        [JsonProperty(PropertyName = "totalRevenue")]
        public decimal TotalRevenue { get; set; }
    }
}
