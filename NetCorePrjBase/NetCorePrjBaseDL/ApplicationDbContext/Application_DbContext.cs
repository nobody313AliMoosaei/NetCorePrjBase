using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePrjBaseDL.ApplicationDbContext
{
    public partial class Application_DbContext : DbContext
    {
        public Application_DbContext(DbContextOptions<Application_DbContext>option)
            :base(option)
        {
        }
    }
}
