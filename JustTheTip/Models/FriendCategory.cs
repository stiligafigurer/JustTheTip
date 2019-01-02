using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustTheTip.Models {
    public class FriendCategory {
        [Key]
        public virtual int FriendshipId { get; set; }
        public virtual User FriendId { get; set; }
        public virtual Category CategoryId { get; set; }
    }
}