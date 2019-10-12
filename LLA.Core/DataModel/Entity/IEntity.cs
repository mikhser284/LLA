using System;
using System.Collections.Generic;
using System.Text;

namespace LLA.Core
{
    public interface IEntity
    {
        Int64 Id { get; }

        Int32 Version { get; }

        DateTime CreatedAt { get; }

        Int64 CreatedBy { get; }

        DateTime UpdatedAt { get; }

        Int64 UpdatedBy { get; }

        EEntityState EntityState { get; }
    }
}
