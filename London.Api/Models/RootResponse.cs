﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace London.Api.Models
{
  public class RootResponse : Resource
  {
    public Link Rooms { get; set; }
    public Link Info { get; set; }
  }
}
