using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNotes.Models
{
    public class EmailModel
    {
        public string Subject { get; set; }
        public EmailModelAddress From { get; set; }
        public EmailModelAddress To { get; set; }
        public string Text { get; set; }
        public string TextAsHtml { get; set; }
        public DateTime Date { get; set; }
    }

    public class EmailModelAddress
    {
        public string Text { get; set; }
    }
}