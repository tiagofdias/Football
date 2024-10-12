using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Futebol.Controllers
{
    public class JogadoresController : Controller
    {

        //5
        public ActionResult Delete(int? id)
        {

            using (futebolEntities bd = new futebolEntities())
            {
               
                int IDJogador = id ?? -1;

                Jogadores alfa = bd.Jogadores.Find(IDJogador);

                if (alfa != null)
                {

                    bd.Jogadores.Remove(alfa);
                    bd.SaveChanges();

                    return Json(new { msg = "Registo eliminado com sucesso" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { msg = "O registo não foi encontrado" }, JsonRequestBehavior.AllowGet);
            }

        }

        //4
        [HttpPost]
        public ActionResult Edit(Jogadores a)
        {
            try
            {

                using (futebolEntities bd = new futebolEntities())
                {


                    Jogadores alfa = bd.Jogadores.Find(a.IDJogador);

                    if (alfa != null)
                    {
                        alfa.IDClube = a.IDClube;
                        alfa.IDAgente = a.IDAgente;
                        alfa.IDPosicao = a.IDPosicao;
                        alfa.Nome = a.Nome;
                        alfa.Numero = a.Numero;
                        alfa.Altura = a.Altura;
                        alfa.Peso = a.Peso;
                        alfa.DataNascimento = a.DataNascimento;

                        bd.SaveChanges();

                        return RedirectToAction("ListaJogadores", new { msg = "Registo editado com sucesso" });

                    }

                    return RedirectToAction("ListaJogadores", new { msg = "Jogador Inexistente" });
                }

            }
            catch (Exception)
            {

                return RedirectToAction("ListaJogadores", new { msg = "Ocorreu um Erro. Operação cancelada" });
            }
        }

        //4
        public ActionResult Edit(int? id)
        {

            using (futebolEntities bd = new futebolEntities())
            {

                //IDS de outras tabelas
                ViewBag.Clube = new SelectList(bd.Clube.ToList(), "IDClube", "Nome");
                ViewBag.Agente = new SelectList(bd.Agente.ToList(), "IDAgente", "Nome");
                ViewBag.Posicao = new SelectList(bd.Posicao.ToList(), "IDPosicao", "Posicao1");
                
                int IDJogador = id ?? 1;

                Jogadores alfa = bd.Jogadores.Find(IDJogador);

                if (alfa != null) return View(alfa);

                return RedirectToAction("ListaJogadores", new { msg = "Jogador Inexistente" });
            }
        }

        //3
        public ActionResult AdicionarJogadores()
        {
            using (futebolEntities bd = new futebolEntities())
            {

                //IDS de outras tabelas
                ViewBag.Clube = new SelectList(bd.Clube.ToList(),"IDClube","Nome");
                ViewBag.Agente = new SelectList(bd.Agente.ToList(), "IDAgente", "Nome");
                ViewBag.Posicao = new SelectList(bd.Posicao.ToList(), "IDPosicao", "Posicao1");

                Jogadores alfa = new Jogadores();

                return View(alfa);

            }
        }

        //3
        [HttpPost]
        public ActionResult AdicionarJogadores(Jogadores a)
        {
            try
            {
                using (futebolEntities bd = new futebolEntities())
                {

                    bd.Jogadores.Add(a);
                    bd.SaveChanges();

                    return RedirectToAction("ListaJogadores", new { msg = "Registo criado com sucesso" });

                }
            }
            catch (Exception)
            {

                return RedirectToAction("ListaJogadores", new { msg = "Ocorreu um Erro. Operação cancelada" });
            }
        }

        //2
        public ActionResult Consultar(int? id)
        {

            int IDJogador = id ?? 1;

            using (futebolEntities bd = new futebolEntities())
            {
                ViewBag.Clube = bd.Clube.ToList<Clube>();
                ViewBag.Agente = bd.Agente.ToList<Agente>();
                ViewBag.Posicao = bd.Posicao.ToList<Posicao>();

                Jogadores alfa = bd.Jogadores.Where(x => x.IDJogador == IDJogador).FirstOrDefault();

                if (alfa != null) return View(alfa);


                return RedirectToAction("ListaJogadores", new { msg = "Jogador Inexistente" });

            }

        }

        //1
        public ActionResult ListaJogadores(String msg, string ordenar, string search, string filtro, int? page)
        {
            ViewBag.msg = msg;

            using (futebolEntities bd = new futebolEntities())
            {

                //IDClube
                List<Clube> Clube = bd.Clube.ToList<Clube>();
                ViewBag.Clube = Clube;

                //IDAgente
                List<Agente> Agente = bd.Agente.ToList<Agente>();
                ViewBag.Agente = Agente;

                //IDPosicao
                List<Posicao> Posicao = bd.Posicao.ToList<Posicao>();
                ViewBag.Posicao = Posicao;

                List<Jogadores> Jogadores = bd.Jogadores.ToList();

                //Filtro

                if (!string.IsNullOrEmpty(search)) page = 1; else search = filtro;

                ViewBag.filtro = search;

                if (!string.IsNullOrEmpty(search)) Jogadores = Jogadores.Where(x => x.Nome.Contains(search)).ToList();        

                //Ordenação

                ViewBag.ordenar = ordenar;
                ViewBag.ordenarID = (string.IsNullOrEmpty(ordenar)) ? "IDJogadorDesc" : "";
                ViewBag.ordenarNome = (ordenar == "Nome") ? "NomeDesc" : "Nome";
                ViewBag.ordenarIDAgente = (ordenar == "IDAgente") ? "IDAgenteDesc" : "IDAgente";
                ViewBag.ordenarIDClube = (ordenar == "IDClubeDesc") ? "IDClube" : "IDClubeDesc";
                ViewBag.ordenarPosicao = (ordenar == "PosicaoDesc") ? "Posicao" : "PosicaoDesc";
                ViewBag.ordenarIdade = (ordenar == "Idade") ? "IdadeDesc" : "Idade";


                switch (ordenar)
                {
                    // Ordem Descendente
                    case "IDJogadorDesc":

                        Jogadores = Jogadores.OrderByDescending(x => x.IDJogador).ToList();
                        break;

                    case "Nome":

                        Jogadores = Jogadores.OrderBy(x => x.Nome).ToList();
                        break;

                    case "NomeDesc":

                        Jogadores = Jogadores.OrderByDescending(x => x.Nome).ToList();
                        break;

                    case "IDAgente":

                        Jogadores = Jogadores.OrderBy(x => x.IDAgente).ToList();
                        break;

                    case "IDAgenteDesc":

                        Jogadores = Jogadores.OrderByDescending(x => x.IDAgente).ToList();
                        break;

                    case "IDClube":

                        Jogadores = Jogadores.OrderBy(x => x.IDClube).ToList();
                        break;

                    case "IDClubeDesc":

                        Jogadores = Jogadores.OrderByDescending(x => x.IDClube).ToList();
                        break;

                    case "Posicao":

                        Jogadores = Jogadores.OrderBy(x => x.IDPosicao).ToList();
                        break;

                    case "PosicaoDesc":

                        Jogadores = Jogadores.OrderByDescending(x => x.IDPosicao).ToList();
                        break;

                    case "Idade":

                        Jogadores = Jogadores.OrderBy(x => x.Idade).ToList();
                        break;

                    case "IdadeDesc":

                        Jogadores = Jogadores.OrderByDescending(x => x.Idade).ToList();
                        break;


                    //Ordem Ascendente
                    default:

                        Jogadores = Jogadores.OrderBy(x => x.IDJogador).ToList();
                        break;
                }

                //paginação
                int pagesize = 3;
                int pagenumber = (page ?? 1);
                return View(Jogadores.ToPagedList(pagenumber, pagesize));

            }

        }

        public ActionResult Index()
        {
            return View();
        }
    }
}