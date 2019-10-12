using System;
using System.Collections.Generic;
using System.Text;

namespace LLA.Core
{
    public abstract class CEntity : IEntity
    {
        public long Id { get; }
        public int Version { get; }
        public DateTime CreatedAt { get; }
        public long CreatedBy { get; }
        public DateTime UpdatedAt { get; }
        public long UpdatedBy { get; }
        public EEntityState EntityState { get; }
    }
}
