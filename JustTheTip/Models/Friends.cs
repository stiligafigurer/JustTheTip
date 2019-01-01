using System.ComponentModel.DataAnnotations;

namespace JustTheTip.Models {
    public class Friends
    {
        [Key]
        public virtual User User { get; set; }
        [Key]
        public virtual User Friend { get; set; }
        public virtual Category RelationCategory { get; set; }
    }
}