﻿using System;
using System.Collections.Generic;

namespace Dal.models;

public partial class Address
{
    public int Id { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public int HouseNumber { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
