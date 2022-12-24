using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards_Game.Logic.Inter;

namespace Cards_Game.Logic
{
    /// <summary>
    /// Clase que relaciona el estado del juego con el interprete
    /// </summary>
    static class Union
    {
        /// <summary>
        /// Crea un contexto a partir de un estado
        /// </summary>
        /// <param name="camp"> estado actual </param>
        /// <returns></returns>
        public static Context CreateContext(Campo camp)
        {
            Context con = new Context();
            //vida del jugador actual
            con.Add(new TOKENS("ENTERO", "PVida"), camp.players[camp.actualturn].vida);
            //vida del jugador enemigo
            con.Add(new TOKENS("ENTERO", "EVida"), camp.players[camp.Find_victim()].vida);
            //cantidad de monstruos en el campo del jugador actual
            con.Add(new TOKENS("ENTERO", "PCampo"), camp.Campos[camp.actualturn].Count);
            //cantidad de monstruos en el campo del jugador enemigo
            con.Add(new TOKENS("ENTERO", "ECampo"), camp.Campos[camp.Find_victim()].Count);
            //cantidad de cartas en la mano del jugador actual
            con.Add(new TOKENS("ENTERO", "PMano"), camp.players[camp.actualturn].Mano.Count);
            //cantidad de cartas en la mano del jugador enemigo
            con.Add(new TOKENS("ENTERO", "EMano"), camp.players[camp.Find_victim()].Mano.Count);
            //cuantas invocaciones se han realizado en el turno
            con.Add(new TOKENS("ENTERO", "Invocations"), camp.invocation);
            //influencia del orbe del jugador actual sobre sus cartas
            con.Add(new TOKENS("ENTERO", "PMorbe"), camp.players[camp.actualturn].multiplicadorORBE * 10);
            //influencia del orbe del jugador enemigo sobre sus cartas
            con.Add(new TOKENS("ENTERO", "EMorbe"), camp.players[camp.Find_victim()].multiplicadorORBE * 10);
            //variables que identifican si habra que modificar el ataque y defensa de los monstruos del campo
            con.Add(new TOKENS("ENTERO", "PAtaque"), 0);
            con.Add(new TOKENS("ENTERO", "EAtaque"), 0);
            con.Add(new TOKENS("ENTERO", "PDefensa"), 0);
            con.Add(new TOKENS("ENTERO", "EDefensa"), 0);
            //orbe del jugador actual
            switch (camp.players[camp.actualturn].orbe)
            {
                case "Fuego":
                    con.Add(new TOKENS("ENTERO", "PCOrbe"), 1);
                    break;
                case "Agua":
                    con.Add(new TOKENS("ENTERO", "PCOrbe"), 2);
                    break;
                case "Tierra":
                    con.Add(new TOKENS("ENTERO", "PCOrbe"), 3);
                    break;
                case "Aire":
                    con.Add(new TOKENS("ENTERO", "PCOrbe"), 4);
                    break;
                

            }
            //orbe del jugador enemigo
            switch (camp.players[camp.Find_victim()].orbe)
            {
                case "Fuego":
                    con.Add(new TOKENS("ENTERO", "ECOrbe"), 1);
                    break;
                case "Agua":
                    con.Add(new TOKENS("ENTERO", "ECOrbe"), 2);
                    break;
                case "Tierra":
                    con.Add(new TOKENS("ENTERO", "ECOrbe"), 3);
                    break;
                case "Aire":
                    con.Add(new TOKENS("ENTERO", "ECOrbe"), 4);
                    break;

            }
            double pma = 0;
            double pmd = 0;
            double pta = 0;
            double ptd = 0;
            foreach (Monstruo m in camp.Campos[camp.actualturn])
            {
                if(m.ataque >pma) pma = m.ataque;
                if(m.defensa > pmd) pmd = m.defensa;
                pta += m.ataque;
                ptd+=m.defensa;
            }
            double ema = 0;
            double emd = 0;
            double eta = 0;
            double etd = 0;
            foreach (Monstruo m in camp.Campos[camp.Find_victim()])
            {
                if (m.ataque > ema) ema = m.ataque;
                if (m.defensa > emd) emd = m.defensa;
                eta += m.ataque;
                etd += m.defensa;
            }
            //Ataque del monstruo mas fuerte del jugador actual
            con.Add(new TOKENS("ENTERO", "PMA"), pma);
            //Defensa del monstruo mas fuerte del jugador actual
            con.Add(new TOKENS("ENTERO", "PMD"), pmd);
            //ataque total de los monstruos del jugador actual
            con.Add(new TOKENS("ENTERO", "PTA"), pta);
            //defensa total de los monstruos del jugador actual
            con.Add(new TOKENS("ENTERO", "PTD"), ptd);
            //Ataque del monstruo mas fuerte del jugador enemigo
            con.Add(new TOKENS("ENTERO", "EMA"), ema);
            //Defensa del monstruo mas fuerte del jugador enemigo
            con.Add(new TOKENS("ENTERO", "EMD"), emd);
            //ataque total de los monstruos del jugador enemigo
            con.Add(new TOKENS("ENTERO", "ETA"), eta);
            //defensa total de los monstruos del jugador enemigo
            con.Add(new TOKENS("ENTERO", "ETD"), etd);
            //variables que identifican si habra que modificar las veces que atacan y/o activan efectos los monstruos en el turno
            con.Add(new TOKENS("ENTERO", "PA"), 0);
            con.Add(new TOKENS("ENTERO", "PE"), 0);
            con.Add(new TOKENS("ENTERO", "EA"), 0);
            con.Add(new TOKENS("ENTERO", "EE"), 0);
            return con;
        }

        /// <summary>
        /// Interpreta un efecto
        /// </summary>
        /// <param name="scope"> contexto </param>
        /// <param name="effect">efecto en lenguaje de interprete</param>
        /// <returns></returns>
        public static Context InterpretEffect(Context scope, string effect)
        {
            //Analizador lexico
            Lexer lexer = new Lexer(effect);
            //Analizador Semantico
            Analizador an = new Analizador(lexer);
            //Interprete
            Interprete i = new Interprete(an, scope);
            i.interpretar();
            return scope;
        }
    }
}
