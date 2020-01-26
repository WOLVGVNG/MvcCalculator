using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcCalculator.Models;

namespace MvcCalculator.Data
{
    public class MvcCalculatorContext : IdentityDbContext
    {
        public MvcCalculatorContext(DbContextOptions<MvcCalculatorContext> options)
            : base(options)
        {
        }
        public DbSet<Calculation> Calculation { get; set; }
    }
}