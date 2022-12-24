using System.Collections;
using System.Text.Json;

namespace Cards_Game.Logic;

/// <summary>
/// Se encarga de procesar los json que contienen las cartas del juego y agregarlas al mazo
/// </summary>
public class BaseDatos
{
    public BaseDatos() //constructor vacio que lo necesita el json
    {

    }

    //ruta donde se encuentran los json que almacenan las cartas
    public string RutaMonstruos = @"../MonstruosAuto.json";
    public string RutaMagia = @"../MagicasAuto.json";
    //ruta donde se encuentran los txt que contienen los ultimos id agregados a los json para que las cartas no se repitan
    public string idMagia = @"../idMagics.txt";
    public string idMonstruo = @"../idMonstruos.txt";

    //listas donde se van a almacenar las cartas con sus propiedades
    public List<Monstruo> Monstruos = new List<Monstruo>();
    public List<Magic> Magicas = new List<Magic>();

    /// <summary>
    /// leer las propiedades de las monstruos
    /// </summary>
    /// <param name="rutaMonstruos"></param>
    public void LeerMonstruos(string rutaMonstruos)
    {
        //verifica si existe la ruta pasada 
        if (File.Exists(rutaMonstruos))
        {
            //lee el contenido del json y lo almacena
            string jsonString = File.ReadAllText(RutaMonstruos);
            //si el json  contiene informacion agrega los monstruos a la lista monstruo
            if (jsonString != "")
            {
                Monstruos = JsonSerializer.Deserialize<List<Monstruo>>(jsonString);
            }
       }
    }

    /// <summary>
    /// leer las propiedades de los cartas magicas
    /// </summary>
    /// <param name="rutaMagia"></param>
    public void LeerMagia(string rutaMagia)
    {
        //verifica si existe la ruta psada
        if (File.Exists(rutaMagia))
        {
            //lee el contenido del json y lo almacena
            string jsonString = File.ReadAllText(rutaMagia);
            //si el json  contiene informacion agrega las cartas magicas a la lista de magia
            if (jsonString != "")
            {
                Magicas = JsonSerializer.Deserialize<List<Magic>>(jsonString);
            }
        }
    }

    /// <summary>
    /// determinar si un monstruo existe en la lista que los almacena
    /// </summary>
    /// <param name="monster"></param>
    /// <returns></returns>
    public bool ExisteMonstruo(Monstruo monster)
    {
        //recorre todos los monstruos en la lista
        foreach (Monstruo mons in Monstruos)
        {
            //si los id son iguales existe el monstruo
            if (mons.Id == monster.Id)
            {
                return true;
            }
        }
        //si el id no coincide el monstruo no existe
        return false;
    }

    /// <summary>
    /// determinar si una carta de magia ya existe
    /// </summary>
    /// <param name="magia"></param>
    /// <returns></returns>
    public bool ExisteMagia(Magic magia)
    {
        //recorre todos las cartas magicas en la lista
        foreach (Magic mag in Magicas)
        {
            //si los id son iguales existe la carta magica
            if (mag.Id == magia.Id)
            {
                return true;
            }
        }
        //si el id no coincide la carta magica no existe
        return false;
    }

    /// <summary>
    /// guardar monstruos en una lista para luego agregarlos a un json
    /// </summary>
    /// <param name="monstruo"></param>

    public void GuardarMonstruo(Monstruo monstruo)
    {
        //lee los monstruos que hay en el json de la ruta
        LeerMonstruos(RutaMonstruos);

        //si el monstruo no existe lo agrega
        if (!ExisteMonstruo(monstruo))
            Monstruos.Add(monstruo);

        //deserializar el json y almacenar los objetos en una lista de monstruos
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(Monstruos, options);

        //si existe la ruta agrega el contenido al json
        if (File.Exists(RutaMonstruos)) 
            File.WriteAllText(RutaMonstruos, jsonString);
    }

    /// <summary>
    /// guardar magia en una lista para luego agregarlos a un json
    /// </summary>
    /// <param name="magica"></param>
    public void GuardarMagia(Magic magica)
    {
        //lee las cartas magicas que hay en el json de la ruta
        LeerMagia(RutaMagia);

        //si la carta magica no existe lo agrega
        if (!ExisteMagia(magica))
        {
            Magicas.Add(magica);
        }

        //deserializar el json y almacenar los objetos en una lista de magia
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(Magicas, options);

        //si existe la ruta agrega el contenido al json
        if (File.Exists(RutaMagia))
            File.WriteAllText(RutaMagia, jsonString);
    }

    /// <summary>
    /// cargar el ultimo id de cartas creadas
    /// </summary>
    /// <param name="ruta"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public int CargarId(string ruta)
    {
        //verifica si existe la ruta 
        if (File.Exists(ruta))
        {
            //guarda el numero que existe en el txt
            int id = int.Parse(File.ReadAllText(ruta));
            int nuevoid = id + 2;

            //reescribe el txt con un nuevo id
            File.WriteAllText(ruta, nuevoid.ToString());

            return id;
        }

        //si no existe la ruta lanza una excepcion
        throw new ArgumentException("la ruta del txt no existe");
    }


}
