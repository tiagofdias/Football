using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Futebol.Models
{
    public class Utilitarios
    {
        public static string mapa (int IDEstadio, List<Estadio> estadios)
        {

            string resp = "";
            Estadio alfa = estadios.Find(m => m.IDEstadio == IDEstadio);

            if (alfa != null) resp = alfa.Nome;

            return resp;

        }

        public static string mapaClubeJ(int IDClube, List<Clube> clubes)
        {

            string resp = "";
            Clube alfa = clubes.Find(m => m.IDClube == IDClube);

            if (alfa != null) resp = alfa.Nome;

            return resp;

        }

        public static string mapaAgenteJ(int IDAgente, List<Agente> Agente)
        {

            string resp = "";
            Agente alfa = Agente.Find(m => m.IDAgente == IDAgente);

            if (alfa != null) resp = alfa.Nome;

            return resp;

        }

        public static string mapaPosicaoJ(int IDPosicao, List<Posicao> Posicao)
        {

            string resp = "";
            Posicao alfa = Posicao.Find(m => m.IDPosicao == IDPosicao);

            if (alfa != null) resp = alfa.Posicao1;

            return resp;

        }

        public static string mapaAgenteT(int IDAgente, List<Agente> Agente)
        {

            string resp = "";
            Agente alfa = Agente.Find(m => m.IDAgente == IDAgente);

            if (alfa != null) resp = alfa.Nome;

            return resp;

        }

        public static string mapaClubeT(int IDClube, List<Clube> Clube)
        {

            string resp = "";
            Clube alfa = Clube.Find(m => m.IDClube == IDClube);

            if (alfa != null) resp = alfa.Nome;

            return resp;

        }

    }
}