using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gossip
{
    public interface iRepository<Type>
    {
        int add(Type obj);
        int edit(Type obj);
        int delete(string id);
        int delete(int id);
        Type getById(string id);
        Type getById(int id);
        Type getByEmail(string email);
        Type getByName(string name);
        List<Type> getAll();
        List<Type> search(string str);
    }
}
