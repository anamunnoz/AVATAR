using System.Collections;
using Cards_Game.Logic.Inter;
namespace Cards_Game.Logic;

/// <summary>
/// Estado del Juego
/// </summary>
public class Campo 
{
    //jugadores ( virtuales o personas )
    public Jugador[] players = new Jugador[2] { new Jugador(), new Jugador() };
    //campos del juego
    public List<Monstruo>[] Campos = new List<Monstruo>[2] { new List<Monstruo>(), new List<Monstruo>() }; //propiedad que guarda las cartas que cada jugador invoca en sus respectivos campos
    //mazo
    public List<Cartas> Mazo=new List<Cartas>(); //mazo del juego
    //variable que indica de quien es el turno actual
    public int actualturn = 0; 
    //indica si es el primer turno
    public bool inicial=true;
    ///indica cuantas invocaciones se ha hecho en el turno
    public double invocation = 0;

    
    /// <summary>
    /// Constructor de la calse
    /// </summary>
    /// <param name="s">parametro que indica el modo de juego</param>
    public Campo(string s)
    {
        
        BaseDatos db = new();
        db.LeerMonstruos(db.RutaMonstruos);
        db.LeerMagia(db.RutaMagia);
        foreach(Cartas carta in db.Monstruos)
        {
            Mazo.Add(carta);
        }

        foreach (Cartas carta in db.Magicas)
        {
            Mazo.Add(carta);
        }

        if (s == "0")
        {
            players = new Jugador[2] { new Jugador(), new Jugador() };
        }
        if (s == "1")
        {
            players = new Jugador[2] {new Person(),new Person() };
        }
        if (s == "2")
        {
           players = new Jugador[2] { new Person(), new Jugador() };
        }

        for(int u = 0; u < 2; u++)
        {
            actualturn = u;
            for (int m = 0; m < 5; m++)
            {
                Actions.Robar_Carta(this);
            }
        }
        actualturn = 0;
    }


    /// <summary>
    /// metodo para saber sobre que sujeto se ejecutan acciones
    /// </summary>
    /// <returns></returns>
    public int Find_victim() 
    {
        int victima;

        if (actualturn == 0) victima = 1;

        else victima = 0;
    
        return victima;
    }

    /// <summary>
    /// actualiza el estado actual del juego a partir de un contexto
    /// </summary>
    /// <param name="c"> contexto</param>
    /// <returns></returns>
    public Campo Update(Context c)
    {
        Campo Up = this;
        //variables para reconocer modificaciones en el estado
        double mycamp = Up.Campos[Up.actualturn].Count;
        double ecamp = Up.Campos[Up.Find_victim()].Count;
        double myhand = Up.players[Up.actualturn].Mano.Count;
        double ehand = Up.players[Up.Find_victim()].Mano.Count;
        //modificaciones  en el campo del jugador actual
        if (mycamp != c.GetValue("PCampo"))
        {
            int dif = (int)mycamp - (int)c.GetValue("PCampo");
            if (dif > 0)
            {
                for (int i = 0; i < dif; i++)
                {
                    Random rand = new Random();
                    Up = Actions.Destroy(Up.Campos[Up.actualturn][rand.Next(0, Up.Campos[Up.actualturn].Count)], Up.actualturn, Up);
                }
            }
            else
            {
                dif = -1 * dif;
                for (int i = 0; i < dif; i++)
                {
                    for (int j = 0; j < Up.Mazo.Count; j++)
                    {
                        if (Up.Mazo[j].category == "Monstruo" && ((Up.Mazo[j] as Monstruo).element == "Agua" || (Up.Mazo[j] as Monstruo).element == "Fuego" || (Up.Mazo[j] as Monstruo).element == "Tierra" || (Up.Mazo[j] as Monstruo).element == "Aire"))
                        {
                            Up.invocation--;
                            Up = Actions.Invocar(Up.Mazo[j], Up);
                            Up.Mazo.Remove(Up.Mazo[j]);
                            break;
                        }
                    }
                }

            }

        }
        //modificaciones  en el campo del jugador enemigo
        if (ecamp != c.GetValue("ECampo"))
        {
            int dif = (int)ecamp - (int)c.GetValue("ECampo");
            if (dif > 0)
            {
                for (int i = 0; i < dif; i++)
                {
                    Random rand = new Random();
                    Up = Actions.Destroy(Up.Campos[Up.Find_victim()][rand.Next(0, Up.Campos[Up.Find_victim()].Count)], Up.Find_victim(), Up);
                }
            }
            else
            {
                dif = -1 * dif;
                for (int i = 0; i < dif; i++)
                {
                    for (int j = 0; j < Up.Mazo.Count; j++)
                    {
                        if (Up.Mazo[j].category == "Monstruo" && ((Up.Mazo[j] as Monstruo).element == "Agua" || (Up.Mazo[j] as Monstruo).element == "Fuego" || (Up.Mazo[j] as Monstruo).element == "Tierra" || (Up.Mazo[j] as Monstruo).element == "Aire"))
                        {
                            if (Up.Campos[Up.Find_victim()].Count != 5)
                            {
                                Up.Campos[Up.Find_victim()].Add(Up.Mazo[j] as Monstruo);
                                break;
                            }
                            else
                            {
                                Random rand = new Random();
                                Up = Actions.Destroy(Up.Campos[Up.Find_victim()][rand.Next(0, Up.Campos[Up.Find_victim()].Count)], Up.Find_victim(), Up);
                                Up.Campos[Up.Find_victim()].Add(Up.Mazo[j] as Monstruo);
                                break;
                            }
                        }
                    }
                }

            }


        }
        //modificaciones  en la mano del jugador actual
        if (myhand != c.GetValue("PMano"))
        {
            
            int dif = (int)myhand - (int)c.GetValue("PMano");
            
            if (dif > 0)
            {
                for (int i = 0; i < dif; i++)
                {
                    Random r = new Random();
                    Up.players[Up.actualturn].Mano.Remove(Up.players[Up.actualturn].Mano[r.Next(0, Up.players[Up.actualturn].Mano.Count)]);

                }
            }
            else
            {
                int a = -dif;
                for (int i = 0; i < a; i++)
                {
                    Up = Actions.Robar_Carta(Up);
                    
                }
            }

        }
        //modificaciones  en la mano del jugador enemigo
        if (ehand != c.GetValue("EMano"))
        {
            int dif = (int)ehand - (int)c.GetValue("EMano");
            if (dif > 0)
            {
                for (int i = 0; i < dif; i++)
                {
                    Random r = new Random();
                    Up.players[Up.Find_victim()].Mano.Remove(Up.players[Up.Find_victim()].Mano[r.Next(0, Up.players[Up.Find_victim()].Mano.Count)]);

                }

            }
            else
            {
                for (var i = 0; i < -1 * dif; i++)
                {
                    if (Up.Mazo.Count > 1)
                    {
                        Up.players[Up.Find_victim()].Mano.Add(Up.Mazo[0]);
                        Up.Mazo.Remove(Up.Mazo[0]);
                    }

                }


            }


        }
        //modificaciones en el att de los monstruos del jugador actual
        if(c.GetValue("PA") != 0)
        {
            foreach(Monstruo m in Up.Campos[Up.actualturn])
            {
                m.att += (int)c.GetValue("PA");
            }
        }
        //modificaciones en el ef de los monstruos del jugador actual
        if (c.GetValue("PE") != 0)
        {
            foreach (Monstruo m in Up.Campos[Up.actualturn])
            {
                m.ef += (int)c.GetValue("PE");
            }
        }
        //modificaciones en el att de los monstruos del jugador enemigo
        if (c.GetValue("EA") != 0)
        {
            foreach (Monstruo m in Up.Campos[Up.Find_victim()])
            {
                m.att += (int)c.GetValue("EA");
            }
        }
        //modificaciones en el ef de los monstruos del jugador enemigo
        if (c.GetValue("EE") != 0)
        {
            foreach (Monstruo m in Up.Campos[Up.Find_victim()])
            {
                m.ef += (int)c.GetValue("EE");
            }
        }
        //modificaciones en el ataque de los monstruos del campo actual
        if (c.GetValue("PAtaque") != 0)
        {
            foreach(Monstruo m in Up.Campos[Up.actualturn])
            {
                m.ataque += c.GetValue("PAtaque");
            }
        }
        //modificaciones en el ataque de los monstruos del campo enemigo
        if (c.GetValue("EAtaque") != 0)
        {
            foreach (Monstruo m in Up.Campos[Up.Find_victim()])
            {
                m.ataque += c.GetValue("EAtaque");
            }
        }
        //modificaciones en la defensa de los monstruos del campo actual
        if (c.GetValue("PDefensa") != 0)
        {
            foreach (Monstruo m in Up.Campos[Up.actualturn])
            {
                m.defensa += c.GetValue("PDefensa");
            }
        }
        //modificaciones en la defensa de los monstruos del campo actual
        if (c.GetValue("EDefensa") != 0)
        {
            foreach (Monstruo m in Up.Campos[Up.Find_victim()])
            {
                m.defensa += c.GetValue("EDefensa");
            }
        }
        //modificaciones en los orbes del jugador actual y el jugador enemigo
        if (c.GetValue("PCOrbe") != 0)
        {
            int orb = (int)c.GetValue("PCOrbe");
            if (orb == 1) Up.players[Up.actualturn].orbe = "Fuego";
            if (orb == 2) Up.players[Up.actualturn].orbe = "Agua";
            if (orb == 3) Up.players[Up.actualturn].orbe = "Tierra";
            if (orb == 4) Up.players[Up.actualturn].orbe = "Aire";
            if (orb == 5) Up.players[Up.actualturn].orbe = Jugador.Random_orb();

        }
        if (c.GetValue("ECOrbe") != 0)
        {
            int orb = (int)c.GetValue("ECOrbe");
            if (orb == 1) Up.players[Up.Find_victim()].orbe = "Fuego";
            if (orb == 2) Up.players[Up.Find_victim()].orbe = "Agua";
            if (orb == 3) Up.players[Up.Find_victim()].orbe = "Tierra";
            if (orb == 4) Up.players[Up.Find_victim()].orbe = "Aire";
            if (orb == 5) Up.players[Up.Find_victim()].orbe = Jugador.Random_orb();

        }
        //modificaciones en la vida del jugador actual
        Up.players[Up.actualturn].vida = c.GetValue("PVida");
        //modificaciones en la vida del jugador enemigo
        Up.players[Up.Find_victim()].vida = c.GetValue("EVida");
        //modificaciones en la cantidad de invocaciones del turno
        Up.invocation = c.GetValue("Invocations");
        //modificaciones en la influencia del orbe actual y enemigo sobre los monstruos
        Up.players[Up.actualturn].multiplicadorORBE = c.GetValue("PMorbe") /10;
        Up.players[Up.Find_victim()].multiplicadorORBE = c.GetValue("EMorbe") /10;
        return Up;
    }

    //array que almacena todos los elementos y combinaciones de las cartas tipo monstruos
    public static string[] Elements = new string[] { "Flama", "Llamarada", "Lava", "Hielo", "Planta", "Ciclon", "Terremoto", "Fuego", "Agua", "Tierra", "Aire" }; 

    //matriz que almacena el multiplicador de ataque y defensa de los elementos segun sea el elemento atacado y el elemento atacante
    public static double[,] Multiplicadores = new double[11, 11] { { 0.5, 0.5, 0.5, 2, 2, 1, 0.5, 1, 1, 1, 2 },    
                                                      { 1, 1, 0.5, 1.5, 1.5, 1.5, 0.5, 1, 0.5, 0.5, 2 } ,
                                                      { 1.5, 1, 0.5, 1.5, 1.5, 0.5, 0.5, 1.5, 1, 0.5, 0.5} ,
                                                      { 0.5, 0.5, 0.5, 1, 1.5, 0.5, 1.5, 0.5, 1.5, 1, 1 } ,
                                                      { 0.5, 0.5, 0.5, 0.5, 1, 0.5, 0.5, 0.5, 2, 2, 1 } ,
                                                      { 1, 1, 1, 1.5, 2, 1, 0.5, 1, 2, 1, 1.5 } ,
                                                      { 1.5, 1.5, 1.5, 0.5, 1.5, 0.5, 0.5, 1.5, 1.5, 0.5, 0.5} ,
                                                      { 0.5, 0.5, 0.5, 1.5, 1.5, 1, 0.5, 1, 0.5, 0.5, 1.5 } ,
                                                      { 1.5, 1, 0.5, 0.5, 0.5, 1, 1, 2, 0.5, 1.5, 0.5 } ,
                                                      { 1.5, 1.5, 0.5, 1, 0.5, 0.5, 0.5, 1.5, 1, 0.5, 0.5 } ,
                                                      { 0.5, 0.5, 0.5, 1, 1.5, 1, 0.5, 0.5, 1.5, 0.5, 1 }
    };


    /// <summary>
    /// metodo para identificar la propiedad padre de cada carta tipo monstruo
    /// </summary>
    /// <param name="element">elemento</param>
    /// <returns></returns>
    public static string FatherIdentificator(string element)
    {
        if (element == "Agua" || element == "Fuego" || element == "Tierra" || element == "Aire") return element;
        if (element == "Flama") return "Fuego Fuego";
        if (element == "Llamarada") return "Fuego Aire";
        if (element == "Lava") return "Fuego Tierra";
        if (element == "Hielo") return "Agua Aire";
        if (element == "Planta") return "Tierra Agua";
        if (element == "Ciclon") return "Aire Aire";
        if (element == "Terremoto") return "Tierra Tierra";
        return "";
    }


    /// <summary>
    /// metodo que determina el multiplicador del ataque segun la carta atacada
    /// </summary>
    /// <param name="A"></param>
    /// <param name="D"></param>
    /// <returns></returns>
    public double AttackMultiplicator(Monstruo A, Monstruo D)
    {
        double multiplicador;
        int Atype = 0;
        int Dtype = 0;

        for (int i = 0; i < Elements.Length; i++)
        {
            if (Elements[i] == A.element) Atype = i;
            if (Elements[i] == D.element) Dtype = i;
        }

        multiplicador= Multiplicadores[Atype, Dtype];

        if (A.element == players[actualturn].orbe || A.fathers.Contains(players[actualturn].orbe))
        {
            multiplicador *= players[actualturn].multiplicadorORBE;
        }

        return multiplicador;
    }


    /// <summary>
    ///  metodo que determina el multiplicador de defensa segun la carta atacada
    /// </summary>
    /// <param name="D"></param>
    /// <returns></returns>

    public double DeffenseMultiplicator(Monstruo D)
    {
        double multiplicadorDef = 1;

        if (D.element == players[Find_victim()].orbe || D.fathers.Contains(players[Find_victim()].orbe))
        {
            multiplicadorDef *= players[Find_victim()].multiplicadorORBE;
        }

        return multiplicadorDef;
    }

}


