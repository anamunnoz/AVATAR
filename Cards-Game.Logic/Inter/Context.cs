using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards_Game.Logic.Inter;
/// <summary>
/// Contexto
/// </summary>
public class Context
{
    //tokens en uso
    private Dictionary<TOKENS, double> scope;
    /// <summary>
    /// Constructor
    /// </summary>
    public Context()
    {
        scope = new Dictionary<TOKENS, double>();
    }

    /// <summary>
    /// Mostrar las variables en uso
    /// </summary>
    public void Show()
    {
        foreach (var item in scope)
        {
            Console.WriteLine(item.Key.value + " " + item.Value);
        }
    }
    /// <summary>
    /// Agregar tokens al scope
    /// </summary>
    /// <param name="token"></param>
    /// <param name="value"></param>
    public void Add(TOKENS token, double value)
    {
        scope.Add(token, value);
    }

    /// <summary>
    /// comprobar si el scope contiene un token
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool ContainsId(string id)
    {
        foreach (var item in scope)
        {
            if (item.Key.value == id)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// obtener el valor de un token del scope
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public double GetValue(string id)
    {
        foreach (var item in scope)
        {
            if (item.Key.value == id)
            {
                return item.Value;
            }
        }
        return 0;
    }
    /// <summary>
    /// obtener el tipo de un token
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string? GetType(string id)
    {
        foreach (var item in scope)
        {
            if (item.Key.value == id)
            {
                return item.Key.type;
            }
        }
        return null;
    }
    /// <summary>
    /// cambiar el valor de un token del scope
    /// </summary>
    /// <param name="id"></param>
    /// <param name="value"></param>
    public void Update(string id, double value)
    {     
        foreach (var item in scope)
        {
            if (item.Key.value == id)
            {
                scope[item.Key] = value;
                return;
            }
        }
    }
}

