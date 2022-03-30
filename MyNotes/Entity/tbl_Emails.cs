using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyNotes.Entity
{
    public partial class tbl_Emails
    {
        public Guid ID { get; set; }
        [Column(TypeName = "nvarchar")]
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        [Column(TypeName = "nvarchar")]
        public string Text { get; set; }
        [Column(TypeName = "nvarchar")]
        public string Html { get; set; }
        public DateTime Date { get; set; }
    }
}
