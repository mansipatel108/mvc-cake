namespace cake_assignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SpongeCake
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CakeID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string CakeName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(150)]
        public string CakesDesc { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal Rate { get; set; }

        public virtual Cake Cake { get; set; }
    }
}
