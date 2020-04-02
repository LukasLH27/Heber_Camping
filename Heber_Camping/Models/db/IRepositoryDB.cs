using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heber_Camping.Models
{
    interface IRepositoryDB
    {
        void Open();

        void Close();

        bool Insert(Request request);

    }
}
