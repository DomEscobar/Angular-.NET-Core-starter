using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicTimeAPI.Models
{
    public class TEntity
    {
        public long Id { get; set; }
        public DateTime LastModified { get; set; }

        [NotMapped]
        public string ClientId { get; set; }
    }
}
