using System;

namespace WebApp.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Header { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public DateTime Date { get; set; }
        

    }
}
