﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.DomainClasses.Entity;
namespace WebMarket.DomainClasses.Configuraion
{
    public class UserConfig:EntityTypeConfiguration <ApplicationUser>
    {
        public UserConfig ()
        {
            this.Property(a => a.Name).IsRequired().HasMaxLength(50);
            this.Property(a => a.Family).IsRequired().HasMaxLength(50);
        }
    }
}
