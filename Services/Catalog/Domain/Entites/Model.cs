﻿using Base.Persistence.Repositories;

namespace Domain.Entites;

public class Model : BaseEntity<Guid>
{
    public Guid BrandId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public virtual Brand? Brand { get; set; }
    public virtual ICollection<Brand> Brands { get; set; }

}
