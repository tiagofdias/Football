using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Futebol.Controllers
{
    public class TreinadoresController : Controller
    {

        //5
        public ActionResult Delete(int? id)
        {

            using (futebolEntities bd = new futebolEntities())
            {

                int IDTreinador = id ?? -1;

                Treinador alfa = bd.Treinador.Find(IDTreinador);

                if (alfa != null)
                {

                    bd.Treinador.Remove(alfa);
                    bd.SaveChanges();

                    return Json(new { msg = "Registo eliminado com sucesso" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { msg = "O registo não foi encontrado" }, JsonRequestBehavior.AllowGet);
            }

        }

        //4
        [HttpPost]
        public ActionResult Edit(Treinador a)
        {
            try
            {

                using (futebolEntities bd = new futebolEntities())
                {


                    Treinador alfa = bd.Treinador.Find(a.IDTreinador);

                    if (alfa != null)
                    {
                        alfa.IDTreinador = a.IDTreinador;
                        alfa.IDAgente = a.IDAgente;
                        alfa.IDClube = a.IDClube;
                        alfa.Nome = a.Nome;
                        alfa.Idade = a.Idade;
                        alfa.Nacionalidade = a.Nacionalidade;

                        bd.SaveChanges();

                        return RedirectToAction("ListaTreinadores", new { msg = "Registo editado com sucesso" });

                    }

                    return RedirectToAction("ListaTreinadores", new { msg = "Treinador Inexistente" });
                }

            }
            catch (Exception)
            {

                return RedirectToAction("ListaTreinadores", new { msg = "Ocorreu um Erro. Operação cancelada" });
            }
        }

        //4
        public ActionResult Edit(int? id)
        {

            using (futebolEntities bd = new futebolEntities())
            {

                ViewBag.agente = new SelectList(bd.Agente.ToList(), "IDAgente", "Nome");
                ViewBag.clube = new SelectList(bd.Clube.ToList(), "IDClube", "Nome");

                int IDTreinador = id ?? 1;

                Treinador alfa = bd.Treinador.Find(IDTreinador);

                if (alfa != null) return View(alfa);

                return RedirectToAction("ListaTreinadores", new { msg = "Treinador Inexistente" });
            }
        }

        //3
        public ActionResult AdicionarTreinador()
        {
            using (futebolEntities bd = new futebolEntities())
            {
                //IDAgente
                ViewBag.agente = new SelectList(bd.Agente.ToList(), "IDAgente", "Nome");
                //IDClube
                ViewBag.clube = new SelectList(bd.Clube.ToList(), "IDClube", "Nome");

                Treinador alfa = new Treinador();

                return View(alfa);

            }
        }

        //3
        [HttpPost]
        public ActionResult AdicionarTreinador(Treinador a)
        {
            try
            {
                using (futebolEntities bd = new futebolEntities())
                {

                    bd.Treinador.Add(a);
                    bd.SaveChanges();

                    return RedirectToAction("ListaTreinadores", new { msg = "Registo criado com sucesso" });

                }
            }
            catch (Exception)
            {

                return RedirectToAction("ListaTreinadores", new { msg = "Ocorreu um Erro. Operação cancelada" });
            }
        }

        //2
        public ActionResult Consultar(int? id)
        {

            int IDTreinador = id ?? 1;

            using (futebolEntities bd = new futebolEntities())
            {
                ViewBag.agente = bd.Agente.ToList<Agente>();
                ViewBag.clube = bd.Clube.ToList<Clube>();

                Treinador alfa = bd.Treinador.Where(x => x.IDTreinador == IDTreinador).FirstOrDefault();

                if (alfa != null) return View(alfa);


                return RedirectToAction("ListaTreinadores", new { msg = "Treinador Inexistente" });

            }

        }

        //1
        public ActionResult ListaTreinadores(String msg, string ordenar, string search, string filtro, int?page)
        {
            ViewBag.msg = msg;

            using (futebolEntities bd = new futebolEntities())
            {
                //IDAgente
                List<Agente> Agente = bd.Agente.ToList<Agente>();
                ViewBag.agente = Agente;

                //IDClube
                List<Clube> Clube = bd.Clube.ToList<Clube>();
                ViewBag.clube = Clube;

                List<Treinador> Treinador = bd.Treinador.ToList();

                //Filtro

                if (!string.IsNullOrEmpty(search))
                {

                    page = 1;

                }
                else
                {

                    search = filtro;

                }

                ViewBag.filtro = search;

                if (!string.IsNullOrEmpty(search))
                {

                    Treinador = Treinador.Where(x => x.Nome.Contains(search)).ToList();

                }

                //Ordenação

                ViewBag.ordenar = ordenar;
                ViewBag.ordenarID = (string.IsNullOrEmpty(ordenar)) ? "IDTreinadordesc": "";
                ViewBag.ordenarNome = (ordenar == "Nome") ? "NomeDesc" : "Nome";
                ViewBag.ordenarIDAgente = (ordenar == "IDAgente") ? "IDAgenteDesc" : "IDAgente";
                ViewBag.ordenarIDClube = (ordenar == "IDClubeDesc") ? "IDClube" : "IDClubeDesc";
                ViewBag.ordenarIdade = (ordenar == "Idade") ? "IdadeDesc" : "Idade";
                ViewBag.ordenarNacionalidade = (ordenar == "Nacionalidade") ? "NacionalidadeDesc" : "Nacionalidade";

                switch (ordenar)
                {
                    // Ordem Descendente
                    case "IDTreinadordesc":

                        Treinador = Treinador.OrderByDescending(x => x.IDTreinador).ToList();
                        break;

                    case "Nome":

                        Treinador = Treinador.OrderBy(x => x.Nome).ToList();
                        break;

                    case "NomeDesc":

                        Treinador = Treinador.OrderByDescending(x => x.Nome).ToList();
                        break;

                    case "IDAgente":

                        Treinador = Treinador.OrderBy(x => x.IDAgente).ToList();
                        break;

                    case "IDAgenteDesc":

                        Treinador = Treinador.OrderByDescending(x => x.IDAgente).ToList();
                        break;

                    case "IDClube":

                        Treinador = Treinador.OrderBy(x => x.IDClube).ToList();
                        break;

                    case "IDClubeDesc":

                        Treinador = Treinador.OrderByDescending(x => x.IDClube).ToList();
                        break;

                    case "Idade":

                        Treinador = Treinador.OrderBy(x => x.Idade).ToList();
                        break;

                    case "IdadeDesc":

                        Treinador = Treinador.OrderByDescending(x => x.Idade).ToList();
                        break;

                    case "Nacionalidade":

                        Treinador = Treinador.OrderBy(x => x.Nacionalidade).ToList();
                        break;

                    case "NacionalidadeDesc":

                        Treinador = Treinador.OrderByDescending(x => x.Nacionalidade).ToList();
                        break;

                    //Ordem Ascendente
                    default:
                        Treinador = Treinador.OrderBy(x => x.IDTreinador).ToList();
                        break;
                }

                //paginação
                int pagesize = 3;
                int pagenumber = (page ?? 1);
                return View(Treinador.ToPagedList(pagenumber, pagesize));

            }

        }

        public ActionResult Index()
        {
            return View();
        }
    }
}