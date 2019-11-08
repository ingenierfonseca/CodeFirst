using LogicalData.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalData.Repository
{
    public class AccountRepository: Repository<Usuario>
    {

        public Usuario ValidateUser(string email, string password)
        {
            var account = DbSet.Where(x => x.Email.Equals(email) && x.Contrasenya.Equals(password)).FirstOrDefault();
            return account;
        }

        public string Register(Usuario user)
        {
            var userExist = (from u in DbSet.ToList()
                             where u.Identificacion.Equals(user.Identificacion)
                             select u).FirstOrDefault();
            if (userExist == null)
            {
                Insert(user);
                return "Exito";
            }
            else
            {
                return "Ya existe un usuario con la identificacion:" + user.Identificacion;
            }
        }
    }
}
