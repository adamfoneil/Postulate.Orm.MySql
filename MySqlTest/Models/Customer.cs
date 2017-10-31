using Postulate.Orm.Abstract;
using Postulate.Orm.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySqlTest.Models
{    
    public class Customer : Record<int>
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [Column(TypeName = "Date")]
        public DateTime EffectiveDate { get; set; }

        [DecimalPrecision(3, 2)]
        public decimal DiscountRate { get; set; }

        public override string ToString()
        {
            return $"FirstName = {FirstName}, LastName = {LastName}, Email = {Email}, Phone = {Phone}, EffectiveDate = {EffectiveDate}, DiscountRate = {DiscountRate}";
        }
    }
}
