using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Repository.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        ICarRepository Car { get; }
        ISP_Call SP_Call { get; }
    }
}
