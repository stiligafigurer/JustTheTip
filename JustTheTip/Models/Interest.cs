using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustTheTip.Models {
    public class Interest {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public virtual int InterestID { get; set; }
        public virtual string Description { get; set; }
    }
}