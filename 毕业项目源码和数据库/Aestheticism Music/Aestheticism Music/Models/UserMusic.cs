//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aestheticism_Music.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserMusic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserMusic()
        {
            this.Discuss = new HashSet<Discuss>();
            this.MusicList = new HashSet<MusicList>();
        }
    
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserLoginName { get; set; }
        public string UserLoginPwd { get; set; }
        public string UserSongSheet { get; set; }
        public string UserSex { get; set; }
        public string UserPhone { get; set; }
        public string UserBirthday { get; set; }
        public string UserEmail { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Discuss> Discuss { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MusicList> MusicList { get; set; }
    }
}
