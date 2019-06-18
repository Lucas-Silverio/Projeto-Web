using ProjetoListaMusicas.Model.Database;
using ProjetoListaMusicas.Model.Database.Model;
using ProjetoListaMusicas.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoListaMusicas.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (LoginUtils.Instance.Usuario != null)
            {
                var musicas = ListarMusicas();

                return View(musicas);
            }

            return RedirectToAction("CadastrarUsuario");
        }



        // Usuario

        public ActionResult Logar(String usuario, String senha)
        {
            LoginUtils.Instance.Logar(usuario, senha);

            return RedirectToAction("Index");
        }

        public ActionResult Deslogar()
        {
            LoginUtils.Instance.Deslogar();

            return RedirectToAction("Index");
        }

        public ActionResult Perfil()
        {
            return View(LoginUtils.Instance.Usuario);
        }

        public ActionResult CadastrarUsuario()
        {
            var usuario = new Usuario()
            {
                Ativo = true
            };

            return View(usuario);
        }

        public PartialViewResult EdtPerfil(Guid id)
        {
            var usuario = DbFactory.Instance.UsuarioRepository.FindFirstById(id);

            return PartialView("_EdtPerfil", usuario);
        }

        public ActionResult GravarUsuario(Usuario usuario)
        {
            usuario.Ativo = true;
            usuario = DbFactory.Instance.UsuarioRepository.SaveOrUpdate(usuario);
            LoginUtils.Instance.Logar(usuario.Apelido, usuario.Senha);
            return RedirectToAction("Index");
        }

        public PartialViewResult EditarPerfil(Usuario usuario)
        {
            usuario.Ativo = true;
            usuario = DbFactory.Instance.UsuarioRepository.SaveOrUpdate(usuario);

            return PartialView("_tblPerfil", usuario);
        }

        public ActionResult DelUsuario()
        {
            var usuario = DbFactory.Instance.UsuarioRepository.FindFirstById(LoginUtils.Instance.Usuario.Id);
            if (usuario != null)
            {
                usuario.Ativo = false;
                DbFactory.Instance.UsuarioRepository.Update(usuario);
                LoginUtils.Instance.Deslogar();
            }

            return RedirectToAction("Index");
        }

        //Musica

        public PartialViewResult ExibirAddMusica()
        {

            var musica = new Musica()
            {
                Usuario = LoginUtils.Instance.Usuario
            };

            return PartialView("_AddMusica", musica);
        }

        public PartialViewResult GravarMusica(Musica musica)
        {
            musica.Usuario = LoginUtils.Instance.Usuario;
            DbFactory.Instance.MusicaRepository.SaveOrUpdate(musica);

            var musicas = ListarMusicas();

            return PartialView("_TblMusicas", musicas);
        }

        public PartialViewResult DeletarMusica(Guid id)
        {
            var musica = DbFactory.Instance.MusicaRepository.FindFirstById(id);
            if (musica != null)
            {
                DbFactory.Instance.MusicaRepository.Delete(musica);
            }

            var musicas = ListarMusicas();

            return PartialView("_TblMusicas", musicas);
        }

        public PartialViewResult EditarMusica(Guid id)
        {
            var musica = DbFactory.Instance.MusicaRepository.FindFirstById(id);
            if (musica != null)
            {
                return PartialView("_AddMusica", musica);
            }

            var musicas = ListarMusicas();

            return PartialView("_TblMusicas",musicas);
        }

        public IList<Musica> ListarMusicas()
        {
            var usuario = DbFactory.Instance.UsuarioRepository.FindFirstById(LoginUtils.Instance.Usuario.Id);
            var musicas = usuario.Musicas;

            return musicas;
        }
    }
}