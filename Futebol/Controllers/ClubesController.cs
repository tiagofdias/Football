using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Futebol.Controllers
{
    public class ClubesController : Controller
    {

        //5
        public ActionResult Delete(int? id)
        {

            using (futebolEntities bd = new futebolEntities())
            {

                int IDClube = id ?? -1;

                Clube alfa = bd.Clube.Find(IDClube);

                if (alfa != null)
                {

                    bd.Clube.Remove(alfa);
                    bd.SaveChanges();

                    return Json(new { msg = "Registo eliminado com sucesso" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { msg = "O registo não foi encontrado" }, JsonRequestBehavior.AllowGet);
            }

        }

        //4
        [HttpPost]
        public ActionResult Edit(Clube a)
        {
            try
            {

                using (futebolEntities bd = new futebolEntities())
                {


                    Clube alfa = bd.Clube.Find(a.IDClube);

                    if (alfa != null)
                    {
                        alfa.IDEstadio = a.IDEstadio;
                        alfa.Nome = a.Nome;
                        alfa.Presidente = a.Presidente;
                        alfa.Morada = a.Morada;
                        alfa.CodigoPostal = a.CodigoPostal;
                        alfa.Telefone = a.Telefone;
                        alfa.Email = a.Email;

                        bd.SaveChanges();

                        return RedirectToAction("ListaClubes", new { msg = "Registo editado com sucesso" });

                    }

                    return RedirectToAction("ListaClubes", new { msg = "Clube Inexistente" });
                }

            }
            catch (Exception)
            {

                return RedirectToAction("ListaClubes", new { msg = "Ocorreu um Erro. Operação cancelada" });
            }
        }

    //4
    public ActionResult Edit(int? id)
        {

            using (futebolEntities bd = new futebolEntities())
            {

                ViewBag.estadios = new SelectList(bd.Estadio.ToList(), "IDEstadio", "Nome");

                int IDClube = id ?? 1;

                Clube alfa = bd.Clube.Find(IDClube);

                if (alfa != null) return View(alfa);

                return RedirectToAction("ListaClubes", new { msg = "Clube Inexistente" });
            }
        }

        //3
        public ActionResult AdicionarClube()
        {
            using (futebolEntities bd = new futebolEntities())
            {
                //IDEstadio
                ViewBag.estadios = new SelectList(bd.Estadio.ToList(), "IDEstadio", "Nome");

                Clube alfa = new Clube();

                return View(alfa);

            }
        }

        //3
        [HttpPost]
        public ActionResult AdicionarClube(Clube a)
        {
            try
            {
                using (futebolEntities bd = new futebolEntities())
                {

                    bd.Clube.Add(a);
                    bd.SaveChanges();

                    return RedirectToAction("ListaClubes", new { msg = "Registo criado com sucesso" });

                }
            }
            catch (Exception)
            {

                return RedirectToAction("ListaClubes", new { msg = "Ocorreu um Erro. Operação cancelada" });
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        //1
        public ActionResult ListaClubes(String msg, string ordenar, string search, string filtro, int? page)
        {
            ViewBag.msg = msg;

            using (futebolEntities bd = new futebolEntities())
            {
                //IDESTAGIOOO
                List<Estadio> Estadio = bd.Estadio.ToList<Estadio>();
                ViewBag.estadios = Estadio;

                List<Clube> Clubes = bd.Clube.ToList();

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

                    Clubes = Clubes.Where(x => x.Nome.Contains(search)).ToList();

                }

                //Ordenação

                ViewBag.ordenar = ordenar;
                ViewBag.ordenarID = (string.IsNullOrEmpty(ordenar)) ? "IDClubeDesc" : "";
                ViewBag.ordenarNome = (ordenar == "Nome") ? "NomeDesc" : "Nome";
                ViewBag.ordenarIDEstadio = (ordenar == "IDEstadio") ? "IDEstadioDesc" : "IDEstadio";

                switch (ordenar)
                {
                    // Ordem Descendente
                    case "IDClubeDesc":

                        Clubes = Clubes.OrderByDescending(x => x.IDClube).ToList();
                        break;

                    case "Nome":

                        Clubes = Clubes.OrderBy(x => x.Nome).ToList();
                        break;

                    case "NomeDesc":

                        Clubes = Clubes.OrderByDescending(x => x.Nome).ToList();
                        break;

                    case "IDEstadio":

                        Clubes = Clubes.OrderBy(x => x.IDEstadio).ToList();
                        break;

                    case "IDEstadioDesc":

                        Clubes = Clubes.OrderByDescending(x => x.IDEstadio).ToList();
                        break;
                  
                    //Ordem Ascendente
                    default:
                        Clubes = Clubes.OrderBy(x => x.IDClube).ToList();
                        break;
                }

                //paginação
                int pagesize = 3;
                int pagenumber = (page ?? 1);
                return View(Clubes.ToPagedList(pagenumber, pagesize));

            }

        }

        //2
        public ActionResult Consultar(int? id)
        {

            int IDClube = id ?? 1;

            using (futebolEntities bd = new futebolEntities())
            {
                ViewBag.estadios = bd.Estadio.ToList<Estadio>();

                Clube alfa = bd.Clube.Where(x => x.IDClube == IDClube).FirstOrDefault();

                if (alfa != null) return View(alfa);


                return RedirectToAction("ListaClubes", new { msg = "Clube Inexistente" });

            }

        }
    }
}