using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heber_Camping.Models.db
{
    interface IRepositoryBenutzer : IDbBase
    {
        bool Insert(Benutzer user);
        bool Delete(int id);
        bool UpdateUserData(int id, Benutzer neueBenutzerDaten);
        Benutzer GetUser(int id);
        Benutzer Login(BenutzerLogin user);
    }
}
