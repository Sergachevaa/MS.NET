﻿using System.ComponentModel.DataAnnotations;

namespace FlowersShop.DataAccess.Entities;

public abstract class BaseEntity 
{
    [Key] public int Id { get; set; }
    
    public Guid ExternalId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
}