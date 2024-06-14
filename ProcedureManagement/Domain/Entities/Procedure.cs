using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Procedure : BaseAuditableEntity
    {
        public static Procedure Create()
        {
            return new Procedure();
        }
    }
}
