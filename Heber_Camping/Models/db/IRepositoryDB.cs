using Heber_Camping.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heber_Camping.Models
{
    interface IRepositoryDB : IDbBase
    {
        bool Insert(Request request);

        List<Request> GetRequests();

        bool Edit(int id);

        bool Delete(int id);

    }
}
