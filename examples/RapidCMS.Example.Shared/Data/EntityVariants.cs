using System;
using System.ComponentModel.DataAnnotations;
using RapidCMS.Core.Abstractions.Data;

namespace RapidCMS.Example.Shared.Data
{
    public class EntityVariantBase : IEntity, ICloneable
    {
        public int Id { get; set; }
        
        [Required]
        public string? Name { get; set; }

        string? IEntity.Id { get => Id.ToString(); set => Id = int.Parse(value ?? "0"); }

        public object Clone()
        {
            return this switch
            {
                EntityVariantA a => new EntityVariantA
                {
                    Id = a.Id,
                    Name = a.Name,
                    NameA = a.NameA
                },
                EntityVariantB b => new EntityVariantB
                {
                    Id = b.Id,
                    Name = b.Name,
                    NameA = b.NameA,
                    NameB = b.NameB
                },
                EntityVariantC c => new EntityVariantC
                {
                    Id = c.Id,
                    Name = c.Name,
                    NameA = c.NameA,
                    NameB = c.NameB,
                    NameC = c.NameC
                }
            };
        }
    }

    public class EntityVariantA : EntityVariantBase
    {
        public string? NameA { get; set; }
    }

    public class EntityVariantB : EntityVariantBase
    {
        public string? NameA { get; set; }
        public string? NameB { get; set; }
    }

    public class EntityVariantC : EntityVariantBase
    {
        public string? NameA { get; set; }
        public string? NameB { get; set; }
        public string? NameC { get; set; }
    }
}
