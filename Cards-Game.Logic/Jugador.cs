using System.Collections;
namespace Cards_Game.Logic;

/// <summary>
/// Clase que agrupa a los jugadores ( Virtual y Person )
/// </summary>
public class Jugador 
{
    //propiedades iniciales de un jugador
    //vida
    public double vida = 4000;
    //orbe
    public string orbe;
    //influencia del orbe sobre las cartas
    public double multiplicadorORBE = 1.2;
    //lista que guarda las cartas que cada jugador tiene en la mano
    public List<Cartas> Mano = new(); 
    //parametro para las acciones del jugador Person
    public string action = "";

    /// <summary>
    /// Constructor de la clase
    /// </summary>
    public Jugador() 
    {
        orbe = Random_orb();
    }

    /// <summary>
    /// metodo para designar un nucleo u orbe random
    /// </summary>
    /// <returns></returns>
    public static string Random_orb() 
    {
        string[] elementos = new string[] { "Fuego", "Agua", "Tierra", "Aire" };
        Random r = new Random();
        return elementos[r.Next(0, elementos.Length)];
    }

    /// <summary>
    /// Metodo para jugar del Jugador Virtual
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public virtual string PLAY(Campo status)
    {
        //Si puede usar efectos, procede
        for (int i = 0; i < status.Campos[status.actualturn].Count; i++)
        {

            if (status.Campos[status.actualturn][i].ef == 0 && status.Campos[status.actualturn][i].efecto!="Ninguno")
            {

                return "Efecto" + " " + i;

            }

        }

        //Si puede realizar invocaciones, procede a invocar
        if (status.invocation < 2)
        {
            if (status.Campos[status.actualturn].Count != 5)
            {
                List<Monstruo> op=new List<Monstruo>();
                for (int i = 0; i < Mano.Count; i++)
                {
                    if (Mano[i].category == "Monstruo")
                    {
                        var comprobation = Mano[i] as Monstruo;
                        if (comprobation.element == "Fuego" || comprobation.element == "Agua" || comprobation.element == "Aire" || comprobation.element == "Tierra") op.Add(Mano[i] as Monstruo);
                        else
                        {
                            List<int> list = new List<int>();
                            string[] sacrificios = comprobation.fathers.Split(' ');
                            for (int j = 0; j < sacrificios.Length; j++)
                            {
                                for (int h = 0; h < status.Campos[status.actualturn].Count; h++)
                                {
                                    if (status.Campos[status.actualturn][h].element == sacrificios[j] && !list.Contains(h))
                                    {
                                        list.Add(h);
                                        break;
                                    }
                                }
                            }
                            if (list.Count == 2) op.Add(Mano[i] as Monstruo);
                        }
                    }
                }
                List<Monstruo> result= op.OrderByDescending(x => x.ataque).ToList();
                if(result.Count > 0)
                {
                    int i = 0;
                    foreach(Cartas c in Mano)
                    {
                        if(c.id== result[0].id)
                        {
                            return "Invocar" + " " + i;
                        }
                        i++;
                    }
                }
            }
        }
        
        //Si puede usar magia , procede
        if(status.Campos[status.Find_victim()].Count != 0)
        {
            for (int i = 0; i < Mano.Count; i++)
            {
                if (Mano[i].category == "Magia" && status.Campos[status.actualturn].Count >= 1)
                {

                    return "Magia" + " " + i;

                }

            }

        }
       
        //Si puede atacar, procede
        if (!status.inicial)
        {
            for (int i = 0; i < status.Campos[status.actualturn].Count; i++)
            {
                if (status.Campos[status.actualturn][i].att <= 0)
                {
                    if (status.Campos[status.Find_victim()].Count == 0) return "DAtacar" + " " + i;

                    double maxdmg = int.MinValue;
                    int position = 0;

                    for (int j = 0; j < status.Campos[status.Find_victim()].Count; j++)
                    {
                        if (status.Campos[status.actualturn][i].ataque * status.AttackMultiplicator(status.Campos[status.actualturn][i], status.Campos[status.Find_victim()][j]) - status.Campos[status.Find_victim()][j].defensa * status.DeffenseMultiplicator(status.Campos[status.Find_victim()][j]) > maxdmg)
                        {
                            maxdmg = status.Campos[status.actualturn][i].ataque * status.AttackMultiplicator(status.Campos[status.actualturn][i], status.Campos[status.Find_victim()][j]) - status.Campos[status.Find_victim()][j].defensa * status.DeffenseMultiplicator(status.Campos[status.Find_victim()][j]);
                            position = j;
                        }
                    }

                    return "Atacar" + " " + i + " " + position;
                }
            }
        }
        //Si no puede hacer lo anterior finaliza su turno
        return "Fin";
    }
}

/// <summary>
/// Clase del jugador no virtual
/// </summary>
public class Person : Jugador
{
    //Metodo de juego
    public override string PLAY(Campo status)
    {
        if (action != null) return action;
        else return "";
    }
}