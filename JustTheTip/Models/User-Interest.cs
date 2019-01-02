using System.ComponentModel.DataAnnotations;

namespace JustTheTip.Models {
    public class User_Interest
    {
        [Key]
        public virtual User UserID { get; set; }
        [Key]
        public virtual Interest InterestID { get; set; }
    }
}