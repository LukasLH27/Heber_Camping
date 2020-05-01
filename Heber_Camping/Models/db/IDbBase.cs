using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Heber_Camping.Models.db
{
   public interface IDbBase
    {

        void Open();

        void Close();

    }
}
