using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustTheTip.Models {
    public class Friends
    {
        [ForeignKey("User")]
        public virtual User UserID { get; set; }
        [ForeignKey("User")]
        public virtual User FriendID { get; set; }
        public virtual Category RelationCategory { get; set; }
    }
}