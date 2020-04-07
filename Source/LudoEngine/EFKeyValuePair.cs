using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace LudoEngine
{
    public class EFKeyValuePair<TKey, TValue>
    {
        [Key]
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}
