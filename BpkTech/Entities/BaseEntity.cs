using BpkTech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BpkTech.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public virtual Guid Id { get; set; }
    }
}
