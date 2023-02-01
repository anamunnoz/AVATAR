using System.Collections;
namespace Cards_Game.Logic;

/// <summary>
/// Clase que agrupa las acciones del game
/// </summary>
public static class Actions 
{
    /// <summary>
    /// metodo para atacar
    /// </summary>
    ///  <param name="sujeto"> Monstruo atacante </param>
    /// <param name="atacada"> Monstruo atacado</param>
    /// <param name="Juego"> estado actual del juego</param>
    /// <returns></returns>
    public static Campo Atacar(Monstruo sujeto, Monstruo atacada, Campo Juego) //metodo que designa los posibles casos de un ataque de un mosntruo a otro
    {      
        if (!Juego.inicial)
        {
            if (sujeto.att <= 0)
            {               
                sujeto.att++;
                //Si el enemigo tiene monstruos en el campo
                if (Juego.Campos[Juego.Find_victim()].Count >= 1)
                {
                    //Multiplicadores de ataque y defensa de los monstruos
                    double multiplicador = Juego.AttackMultiplicator(sujeto, atacada);

                    double multiplicadorDef = Juego.DeffenseMultiplicator(atacada);

                    //Si el monstruo atacante es mas fuerte
                    if (sujeto.ataque * multiplicador > atacada.defensa * multiplicadorDef)
                    {
                        Destroy(atacada, Juego.Find_victim(), Juego);
                        Cambiar_Vida((sujeto.ataque * multiplicador) - (atacada.defensa * multiplicadorDef), Juego.players[Juego.Find_victim()]);
                    }

                    //Si el monstruo atacado es mas fuerte
                    else if (sujeto.ataque * multiplicador < atacada.defensa * multiplicadorDef)
                    {
                        Destroy(sujeto, Juego.actualturn, Juego);
                        atacada.defensa = atacada.defensa * multiplicadorDef - (sujeto.ataque * multiplicador);
                    }

                    //Si ambos monstruos son igual de fuertes
                    else
                    {
                        Destroy(sujeto, Juego.actualturn, Juego);
                        Destroy(atacada, Juego.Find_victim(), Juego);
                    }

                }
                //Si el enemigo no tiene monstruos en el campo
                else
                {
                    if (sujeto.element == Juego.players[Juego.actualturn].orbe || sujeto.fathers.Contains(Juego.players[Juego.actualturn].orbe))
                    {
                        Cambiar_Vida(sujeto.ataque * Juego.players[Juego.actualturn].multiplicadorORBE, Juego.players[Juego.Find_victim()]);
                    }
                    else Cambiar_Vida(sujeto.ataque, Juego.players[Juego.Find_victim()]);

                }

            }

        }

        return Juego;
    }

    /// <summary>
    /// Metodo para modificar la vida del jugador
    /// </summary>
    /// <param name="variacion">variacion de vida </param>
    /// <param name="objetivo">jugador objetivo</param>
    public static void Cambiar_Vida(double variacion, Jugador objetivo) 
    {
        objetivo.vida -= variacion;

    }

    /// <summary>
    /// Metodo para robar carta
    /// </summary>
    /// <param name="Juego"> Estado actual del juego</param>
    /// <returns></returns>
    public static Campo Robar_Carta(Campo Juego)
    {

        Random r = new Random();
        int card = r.Next(0, Juego.Mazo.Count);
        Juego.players[Juego.actualturn].Mano.Add(Juego.Mazo[card]);
        Juego.Mazo.Remove(Juego.Mazo[card]);
        return Juego;
    }

    /// <summary>
    /// Finalizar turno
    /// </summary>
    /// <param name="Juego">Estado actual del juego</param>
    /// <returns></returns>
    public static Campo Fin_Turno(Campo Juego) //metodo para designar el final del turno de un jugador
    {
        foreach (Monstruo M in Juego.Campos[Juego.actualturn])
        {
            if (M.att >= 1) M.att--;
            if (M.ef >= 1) M.ef--;
        }

        if (Juego.actualturn == 0) Juego.actualturn = 1;
        else Juego.actualturn = 0;

        Robar_Carta(Juego);
        Juego.inicial = false;
        Juego.invocation = 0;

        return Juego;
    }

    /// <summary>
    /// Metodo para anadir al campo del jugador la/las carta/cartas que invoque en su turno
    /// </summary>
    /// <param name="aInvocar">Carta a invocar</param>
    /// <param name="Juego">Estado actual del juego</param>
    /// <returns></returns>
    public static Campo Invocar(Cartas aInvocar, Campo Juego) //
    {
        if(Juego.invocation < 2)
        {
            var x = aInvocar as Monstruo;

            if (x.element == "Fuego" || x.element == "Agua" || x.element == "Tierra" || x.element == "Aire")
            {
                if (Juego.Campos[Juego.actualturn].Count == 5)
                {
                    Random r = new Random();
                    Juego.Campos[Juego.actualturn].Remove(Juego.Campos[Juego.actualturn][r.Next(0, 5)]);
                }

                Juego.Campos[Juego.actualturn].Add(x);
                Juego.players[Juego.actualturn].Mano.Remove(x);
                Juego.invocation++;
                return Juego;
            }
            else
            {
                List<int> list = new List<int>();
                string[] sacrificios = x.fathers.Split(' ');
                for (int i = 0; i < sacrificios.Length; i++)
                {
                    for (int j = 0; j < Juego.Campos[Juego.actualturn].Count; j++)
                    {
                        if (Juego.Campos[Juego.actualturn][j].element == sacrificios[i] && !list.Contains(j))
                        {
                            list.Add(j);
                            break;
                        }
                    }
                }
                if (list.Count == 2)
                {
                    Juego.Campos[Juego.actualturn].Remove(Juego.Campos[Juego.actualturn][list[0]]);
                    if (list[1] < list[0])
                    {
                        Juego.Campos[Juego.actualturn].Remove(Juego.Campos[Juego.actualturn][list[1]]);
                    }
                    else
                    {
                        Juego.Campos[Juego.actualturn].Remove(Juego.Campos[Juego.actualturn][list[1] - 1]);
                    }

                    Juego.Campos[Juego.actualturn].Add(x);
                    Juego.players[Juego.actualturn].Mano.Remove(x);
                    Juego.invocation++;

                }
                return Juego;
            }
        }
        return Juego;
    }

    /// <summary>
    /// Metodo para destruir cartas
    /// </summary>
    /// <param name="aDestruir"> Carta a destruir</param>
    /// <param name="jugador"> propietario de la carta a destruir</param>
    /// <param name="Juego"> estado actual del juego</param>
    /// <returns></returns>
    public static Campo Destroy(Cartas aDestruir, int jugador, Campo Juego) //metodo para destruir una carta
    {
        foreach (Monstruo m in Juego.Campos[jugador])
        {
            if (m.id == aDestruir.id)
            {
                Juego.Campos[jugador].Remove(m);

                break;
            } 
        }
        return Juego;
    }
}
