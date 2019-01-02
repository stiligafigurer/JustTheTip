using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustTheTip.Models {
    public class Interest {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int InterestID { get; set; }
        public virtual string Description { get; set; }
    }
}