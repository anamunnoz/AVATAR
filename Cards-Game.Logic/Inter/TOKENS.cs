using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards_Game.Logic.Inter;

/// <summary>
/// ATOMO
/// </summary>
public class TOKENS
{
    //tipo de atomo
    public string type;
    //valor
    public string value;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public TOKENS(string type,string value)
    {
        this.type = type;
        this.value = value;
    }   
}


