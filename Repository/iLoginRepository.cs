using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gossip
{
    public interface iLoginRepository<Type>
    {
        int Add(Type obj);
        int ChangePassword(string username, string password);
        int UpdateEmail(string username, string email);
        int Delete(string id);
        Type VerifyByUsername(string username, string password);
        Type VerifyByEmail(string email, string password);

    }
}
