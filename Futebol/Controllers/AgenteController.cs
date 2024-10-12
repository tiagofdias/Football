using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Futebol.Controllers
{
    public class AgenteController : Controller
    {

        //5
        public ActionResult Delete(int? id)
        {

            using (futebolEntities bd = new futebolEntities())
            {

                int IDAgente = id ?? -1;

                Agente alfa = bd.Agente.Find(IDAgente);

                if (alfa != null)
                {

                    bd.Agente.Remove(alfa);
                    bd.SaveChanges();

                    return Json(new { msg = "Registo eliminado com sucesso" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { msg = "O registo não foi encontrado" }, JsonRequestBehavior.AllowGet);
            }

        }

        //4
        [HttpPost]
        public ActionResult Edit(Agente a)
        {
            try
            {
                using (futebolEntities bd = new futebolEntities())
                {

                    Agente alfa = bd.Agente.Find(a.IDAgente);

                    if (alfa != null)
                    {

                        alfa.Nome = a.Nome;
                        alfa.Morada = a.Morada;
                        alfa.CodigoPostal = a.CodigoPostal;
                        alfa.Telefone = a.Telefone;

                        bd.SaveChanges();

                        return RedirectToAction("ListaAgente", new { msg = "Registo editado com sucesso" });

                    }

                    return RedirectToAction("ListaAgente", new { msg = "Agente Inexistente" });
                }
            }
            catch (Exception)
            {

                return RedirectToAction("ListaAgente", new { msg = "Ocorreu um Erro. Operação cancelada" });
            }
        }

        //4
        public ActionResult Edit(int? id)
        {

            using (futebolEntities bd = new futebolEntities())
            {

                int IDAgente = id ?? 1;

                Agente alfa = bd.Agente.Find(IDAgente);

                if (alfa != null) return View(alfa);

                return RedirectToAction("ListaAgente", new { msg = "Agente Inexistente" });
            }
        }

        //3
        public ActionResult AdicionarAgente()
        {
            using (futebolEntities bd = new futebolEntities())
            {

                Agente alfa = new Agente();

                return View(alfa);

            }
        }

        //3
        [HttpPost]
        public ActionResult AdicionarAgente(Agente a)
        {
            try
            {
                using (futebolEntities bd = new futebolEntities())
                {

                    bd.Agente.Add(a);
                    bd.SaveChanges();

                    return RedirectToAction("ListaAgente", new { msg = "Registo criado com sucesso" });

                }
            }
            catch (Exception)
            {

                return RedirectToAction("ListaAgente", new { msg = "Ocorreu um Erro. Operação cancelada" });
            }
        }

        //2
        public ActionResult Consultar(int? id)
        {

            int IDAgente = id ?? 1;

            using (futebolEntities bd = new futebolEntities())
            {

                Agente alfa = bd.Agente.Where(x => x.IDAgente == IDAgente).FirstOrDefault();

                if (alfa != null) return View(alfa);


                return RedirectToAction("ListaAgente", new { msg = "Agente Inexistente" });

            }

        }

        //1
        public ActionResult ListaAgente(String msg, string ordenar, string search, string filtro, int? page)
        {
            ViewBag.msg = msg;

            using (futebolEntities bd = new futebolEntities())
            {

                List<Agente> Agentes = bd.Agente.ToList();

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

                    Agentes = Agentes.Where(x => x.Nome.Contains(search)).ToList();

                }

                //Ordenação

                ViewBag.ordenar = ordenar;
                ViewBag.ordenarID = (string.IsNullOrEmpty(ordenar)) ? "IDAgenteDesc" : "";
                ViewBag.ordenarNome = (ordenar == "Nome") ? "NomeDesc" : "Nome";

                switch (ordenar)
                {
                    // Ordem Descendente
                    case "IDAgenteDesc":

                        Agentes = Agentes.OrderByDescending(x => x.IDAgente).ToList();
                        break;

                    case "Nome":

                        Agentes = Agentes.OrderBy(x => x.Nome).ToList();
                        break;

                    case "NomeDesc":

                        Agentes = Agentes.OrderByDescending(x => x.Nome).ToList();
                        break;

                    //Ordem Ascendente
                    default:
                        Agentes = Agentes.OrderBy(x => x.IDAgente).ToList();
                        break;
                }

                //paginação
                int pagesize = 3;
                int pagenumber = (page ?? 1);
                return View(Agentes.ToPagedList(pagenumber, pagesize));

            }

        }

        public ActionResult Index()
        {
            return View();
        }
    }
}