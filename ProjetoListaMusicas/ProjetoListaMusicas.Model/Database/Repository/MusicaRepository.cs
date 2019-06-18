using ProjetoListaMusicas.Model.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace ProjetoListaMusicas.Model.Database.Repository
{
    public class MusicaRepository : RepositoryBase<Musica>
    {
        public MusicaRepository(ISession session) : base(session)
        {
        }
    }
}
