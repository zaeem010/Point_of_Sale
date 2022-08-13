using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Areas.Pos.Data
{
    public class PosDbContext: DbContext
    {
        public PosDbContext(DbContextOptions<PosDbContext> options)
            : base(options)
        {
        }

    }
}
