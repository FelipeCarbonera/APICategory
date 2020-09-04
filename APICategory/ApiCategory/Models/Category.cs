using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiCategory.Models
{
    public class Category
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool InUse { get; set; }

        /*
            utilizando o '[JsonIgnore]' para não ser necessário informar nos parâmetros, pois esse campo é gerado automaticamente pelo banco e só é utilizado como consulta
        */
        [JsonIgnore]
        public DateTime Date { get; set; }
    }
}
