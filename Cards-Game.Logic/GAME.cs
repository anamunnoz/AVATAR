using System.Collections;
using Cards_Game.Logic.Inter;
namespace Cards_Game.Logic;

/// <summary>
/// Motor del Juego
/// </summary>
public class GAME : IEnumerable<Campo>
{
    /// <summary>
    /// GetEnumerators
    /// </summary>
    /// <returns></returns>
    public IEnumerator<Campo> GetEnumerator()
    {
        IEnumerable<Campo> actual = Actual();
        return actual.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    //propiedad que almacena el status del juego
    public Campo Camp;
   

    /// <summary>
    /// constructor de game que almacena el status del juego
    /// </summary>
    /// <param name="s"></param>
    public GAME(string s)
    { 
        Camp = new Campo(s);

    }
    /// <summary>
    /// Formula del Enumerator
    /// </summary>
    /// <returns></returns>
    IEnumerable<Campo> Actual()
    {
        Campo Status = Camp;
        //Variaciones del estado
        while (Status.players[0].vida > 0 && Status.players[1].vida > 0 && Status.Mazo.Count != 0)
        {  
            //accion que se quiere ejecutar
            string action = Status.players[Status.actualturn].PLAY(Status);
            //array que almacena los parametros de esas acciones
            string[] decision = action.Split(' ');

            switch (decision[0])
            {
                case "Invocar":
                    Actions.Invocar(Status.players[Status.actualturn].Mano[int.Parse(decision[1])], Status);
                    break;
                case "Efecto":
                    if(Status.Campos[Status.actualturn][int.Parse(decision[1])].ef < 1)
                    {
                        Context con = Union.InterpretEffect(Union.CreateContext(Status), Status.Campos[Status.actualturn][int.Parse(decision[1])].code);
                        Status = Status.Update(con);
                        Status.Campos[Status.actualturn][int.Parse(decision[1])].ef++;
                    }
                    break;
                case "DAtacar":
                    Status = Actions.Atacar(Status.Campos[Status.actualturn][int.Parse(decision[1])], Status.Campos[Status.actualturn][int.Parse(decision[1])], Status);
                    break;
                case "Atacar":
                    Status = Actions.Atacar(Status.Campos[Status.actualturn][int.Parse(decision[1])], Status.Campos[Status.Find_victim()][int.Parse(decision[2])], Status);
                    break;
                case "Fin":
                    Status = Actions.Fin_Turno(Status);
                    break;
                case "Magia":
                    Context cont = Union.InterpretEffect(Union.CreateContext(Status), Status.players[Status.actualturn].Mano[int.Parse(decision[1])].code);
                    Status = Status.Update(cont);
                    Status.players[Status.actualturn].Mano.Remove(Status.players[Status.actualturn].Mano[int.Parse(decision[1])]);
                    break;
            }
            yield return Status;
        }
    }
}
