using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryCommentObject
    {
        public string Symbol { get; set; }
        public bool IsDescending { get; set; } = true;
    }
}