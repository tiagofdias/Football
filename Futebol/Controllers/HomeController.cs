using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Futebol.Controllers
{
    public class HomeController : Controller
    {
        //5
        public ActionResult Delete(int? id)
        {

            using (futebolEntities bd = new futebolEntities())
            {

                int IDEstadio = id ?? -1;

                Estadio alfa = bd.Estadio.Find(IDEstadio);

                if (alfa != null)
                {

                    bd.Estadio.Remove(alfa);
                    bd.SaveChanges();

                    return Json(new { msg = "Registo eliminado com sucesso" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { msg = "O registo não foi encontrado" }, JsonRequestBehavior.AllowGet);
            }

        }

        //4
        [HttpPost]
        public ActionResult Edit(Estadio a)
        {
            try
            {
                using (futebolEntities bd = new futebolEntities())
                {

                    Estadio alfa = bd.Estadio.Find(a.IDEstadio);

                    if (alfa != null)
                    {

                        alfa.Nome = a.Nome;
                        alfa.Localizacao = a.Localizacao;
                        alfa.Capacidade = a.Capacidade;
                        alfa.Arquiteto = a.Arquiteto;
                        alfa.Inauguracao = a.Inauguracao;
                        alfa.CustoConstrucao = a.CustoConstrucao;

                        bd.SaveChanges();

                        return RedirectToAction("ListaEstadios", new { msg = "Registo editado com sucesso" });

                    }

                    return RedirectToAction("ListaEstadios", new { msg = "Estádio Inexistente" });
                }
            }
            catch (Exception)
            {

                return RedirectToAction("ListaEstadios", new { msg = "Ocorreu um Erro. Operação cancelada" });
            }
        }

        //4
        public ActionResult Edit(int? id)
        {

            using (futebolEntities bd = new futebolEntities())
            {

                int IDEstadio = id ?? 1;

                Estadio alfa = bd.Estadio.Find(IDEstadio);

                if (alfa != null) return View(alfa);

                return RedirectToAction("ListaEstadios", new { msg = "Estádio Inexistente" });
            }
        }

        //2
        public ActionResult Consultar(int? id)
        {

            int IDEstadio = id ?? 1;

            using (futebolEntities bd = new futebolEntities())
            {

                Estadio alfa = bd.Estadio.Where(x => x.IDEstadio == IDEstadio).FirstOrDefault();

                if (alfa != null) return View(alfa);


                return RedirectToAction("ListaEstadios", new { msg = "Estádio Inexistente" });

            }

        }

        //3
        public ActionResult AdicionarEstadio()
        {
            using (futebolEntities bd = new futebolEntities())
            {

                Estadio alfa = new Estadio();

                return View(alfa);

            }
        }

        //3
        [HttpPost]
        public ActionResult AdicionarEstadio(Estadio a)
        {
            try
            {
                using (futebolEntities bd = new futebolEntities())
                {

                    bd.Estadio.Add(a);
                    bd.SaveChanges();

                    return RedirectToAction("ListaEstadios", new { msg = "Registo criado com sucesso" });

                }
            }
            catch (Exception)
            {

                return RedirectToAction("ListaEstadios", new { msg = "Ocorreu um Erro. Operação cancelada" });
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        //1
        public ActionResult ListaEstadios(String msg, string ordenar, string search, string filtro, int? page)
        {
            ViewBag.msg = msg;

            using( futebolEntities bd = new futebolEntities())
            {

                List<Estadio> Estadios = bd.Estadio.ToList();

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

                    Estadios = Estadios.Where(x => x.Nome.Contains(search)).ToList();

                }

                //Ordenação

                ViewBag.ordenar = ordenar;
                ViewBag.ordenarID = (string.IsNullOrEmpty(ordenar)) ? "IDEstadioDesc" : "";
                ViewBag.ordenarNome = (ordenar == "Nome") ? "NomeDesc" : "Nome";          

                switch (ordenar)
                {
                    // Ordem Descendente
                    case "IDEstadioDesc":

                        Estadios = Estadios.OrderByDescending(x => x.IDEstadio).ToList();
                        break;

                    case "Nome":

                        Estadios = Estadios.OrderBy(x => x.Nome).ToList();
                        break;

                    case "NomeDesc":

                        Estadios = Estadios.OrderByDescending(x => x.Nome).ToList();
                        break;

                    //Ordem Ascendente
                    default:
                        Estadios = Estadios.OrderBy(x => x.IDEstadio).ToList();
                        break;
                }

                //paginação
                int pagesize = 3;
                int pagenumber = (page ?? 1);
                return View(Estadios.ToPagedList(pagenumber, pagesize));

            }
           
        }
    }
}