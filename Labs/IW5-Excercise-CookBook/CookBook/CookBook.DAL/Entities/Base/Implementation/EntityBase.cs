using System;
using System.ComponentModel.DataAnnotations.Schema;
using CookBook.DAL.Entities.Base.Interface;

namespace CookBook.DAL.Entities.Base.Implementation
{
    public abstract class EntityBase : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}