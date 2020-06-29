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
    
    public partial class MusicList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MusicList()
        {
            this.Discuss = new HashSet<Discuss>();
            this.MusicListType = new HashSet<MusicListType>();
        }
    
        public int MusicListID { get; set; }
        public string MusicListName { get; set; }
        public string MusicListFileSize { get; set; }
        public string MusicListPath { get; set; }
        public Nullable<int> SingerID { get; set; }
        public Nullable<int> UserID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Discuss> Discuss { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MusicListType> MusicListType { get; set; }
        public virtual Singer Singer { get; set; }
        public virtual UserMusic UserMusic { get; set; }
    }
}
