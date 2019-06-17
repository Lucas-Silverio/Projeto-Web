using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoListaMusicas.Model.Database.Model
{
    public class Musica
    {
        public virtual Guid Id { get; set; }
        [Required(ErrorMessage = "O nome da musica é obrigatório.")]
        public virtual String Nome { get; set; }
        public virtual String Banda { get; set; }
        public virtual String Genero { get; set; }

        public virtual Usuario Usuario { get; set; }

    }
    public class MusicaMap : ClassMapping<Musica>
    {
        public MusicaMap()
        {
            Id(x => x.Id, m => {
                m.Generator(Generators.Guid);
            });

            Property(x => x.Nome);
            Property(x => x.Banda);
            Property(x => x.Genero);

            ManyToOne(x => x.Usuario, m => {
                m.Column("idUsuario");
            });

        }
    }
}
