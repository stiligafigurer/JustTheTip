using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustTheTip.Models {
    public class Category {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int CategoryID { get; set; }
        public virtual string Description { get; set; }
    }
}