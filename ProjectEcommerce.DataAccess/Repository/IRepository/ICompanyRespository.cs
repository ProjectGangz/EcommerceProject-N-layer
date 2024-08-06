using ProjectEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEcommerce.DataAccess.Repository.IRepository
{
    public interface ICompanyRespository:IRepository<Company>
    {
        void Update(Company obj);
      
    }
}
