using System.Collections;
namespace Cards_Game.Logic;

/// <summary>
/// Clase que agrupa a las cartas de tipo monstruo y magia
/// </summary>
public abstract class Cartas 
{
    //nombre
    public string name;
    //categoria ( magia o monstruo )
    public string category;
    //efecto
    public string efecto;
    //efecto en el lenguaje del interprete
    public string code;
    //id unico para cada carta
    public int id;
    //imagen
    public string imagen;

}
/// <summary>
/// Clase de los Monstruos;
/// </summary>
public class Monstruo : Cartas
{
    //ataque
    public double ataque;
    //defensa
    public double defensa;
    //elemento
    public string element;
    //fathers del elemento
    public string fathers;
    //cantidad de veces que ha atacado en el turno actual
    public int att = 0;
    //cantidad de veces que ha activado el efecto en el turno actual
    public int ef = 0;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public double Ataque
    {
        get { return ataque; }
        set { ataque = value; }
    }

    public double Defensa
    {
        get { return defensa; }
        set { defensa = value; }
    }

    public string Element
    {
        get { return element; }
        set { element = value; }
    }

    public string Fathers
    {
        get { return fathers; }
        set { fathers = value; }
    }

    public int Att
    {
        get { return att; }
        set { att = 0; }
    }

    public int Ef
    {
        get { return ef; }
        set { ef = 0; }
    }

    public string Efecto
    {
        get { return efecto; }
        set { efecto = value; }
    }

    public string Category
    {
        get { return category; }
        set { category = value; }
    }

    public string Imagen
    {
        get { return imagen; }
        set { imagen = value; }
    }
    
    public string Code
    {
        get { return code; }
        set { code = value; }
    }
    
    /// <summary>
    /// Constructor de la clase
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ataque"></param>
    /// <param name="defensa"></param>
    /// <param name="element"></param>
    /// <param name="efecto"></param>
    /// <param name="id"></param>
    /// <param name="imagen"></param>
    /// <param name="code"></param>
    public Monstruo(string name, double ataque, double defensa, string element, string efecto, int id, string imagen, string code)
    {
        this.name = name;
        this.id = id;
        this.category = "Monstruo";
        this.ataque = ataque;
        this.defensa = defensa;
        this.element = element;
        this.efecto = efecto;
        this.Fathers = Campo.FatherIdentificator(element);
        this.Att = att;
        this.Ef = ef;
        this.imagen = imagen;
        this.code = code;
    }

    public Monstruo() //constructor vacio que necesita el json
    {

    }
}
/// <summary>
/// Clase de las cartas magicas
/// </summary>
public class Magic : Cartas
{
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string Category
    {
        get { return category; }
        set { category = value; }
    }
 
    public string Efecto
    {
        get { return efecto; }
        set { efecto = value; }
    }

    public string Imagen
    {
        get { return imagen; }
        set { imagen = value; }
    }
    
    public string Code
    {
        get { return code; }
        set { code = value; }
    }
    public Magic() //constructor vacio que necesita el json
    {

    }
    /// <summary>
    /// Constructor de la clase
    /// </summary>
    /// <param name="name"></param>
    /// <param name="id"></param>
    /// <param name="efecto"></param>
    /// <param name="imagen"></param>
    /// <param name="code"></param>
    public Magic(string name, int id, string efecto, string imagen, string code)
    {
        this.id = id;
        this.name = name;
        category = "Magia";
        this.efecto = efecto;
        this.Imagen = imagen;
        this.code =code;
    }

}



