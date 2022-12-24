using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards_Game.Logic.Inter;
/// <summary>
/// Analizador Lexico
/// </summary>
public class Lexer
{
    //texto
    string text;
    //posicion del analizador
    int pos;
    //char actual que se analiza
    string current_char;
    //palabras reservadas
    Dictionary<string, TOKENS> Reserverd_Keywords = new Dictionary<string, TOKENS>();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="text"></param>
    public Lexer(string text)
    {
        this.text = text;
        pos = 0;
        current_char = text[pos].ToString();
        Reserverd_Keywords.Add("BEGIN", new TOKENS("BEGIN", "BEGIN"));
        Reserverd_Keywords.Add("END", new TOKENS("END", "END"));
    }
    /// <summary>
    /// devuelve como atomo del termino actual analizado
    /// </summary>
    /// <returns></returns>
    TOKENS id()
    {
        string resultado = "";
        while (current_char != "" && char.IsLetterOrDigit(current_char.ToCharArray()[0]))
        {
            resultado += current_char;
            avance();
        }
        switch (resultado)
        {
            case "BEGIN":
                return new TOKENS("BEGIN", "BEGIN");
            case "END":
                return new TOKENS("END", "END");
            case "IF":
                return new TOKENS("IF", "IF");
            case "WHILE":
                return new TOKENS("WHILE", "WHILE");
            default:
                return new TOKENS("ID", resultado);
        }
    }
    /// <summary>
    /// Hace que el analisis se centre en el char siguiente
    /// </summary>
    void avance()
    {
        pos++;
        if (pos > text.Length - 1) current_char = "";
        else current_char = text[pos].ToString();
    }
    /// <summary>
    /// Para analizar el siguiente char sin moverse a el
    /// </summary>
    /// <returns></returns>
    string peek()
    {
        int peek_pos = pos + 1;
        if (peek_pos > text.Length - 1) return "";
        else return text[peek_pos].ToString();
    }
    /// <summary>
    /// Para ignorar espacios en blanco
    /// </summary>
    void skipwhitespace()
    {
        while (current_char != "" && string.IsNullOrWhiteSpace(current_char))
        {
            avance();
        }
    }
    /// <summary>
    /// devuelve el numero multidigito actual en forma de string
    /// </summary>
    /// <returns></returns>
    string numero()
    {
        string resultado = "";
        while (current_char != "" && char.IsDigit(current_char[0]))
        {
            resultado += current_char;
            avance();
        }
        return resultado;
    }
    /// <summary>
    /// Metodo mas importante del analizador lexico, se usa para obtener el token siguiente 
    /// </summary>
    /// <returns></returns>
    public TOKENS gnt()
    {
        while (current_char != "")
        {
            if (string.IsNullOrWhiteSpace(current_char))
            {
                skipwhitespace();
                continue;
            }
            if (char.IsLetter(current_char[0]))
            {
                return id();
            }
            if (current_char == ":" && peek() == "=")
            {
                avance();
                avance();
                return new TOKENS("ASSIGN", ":=");
            }
            if (current_char == "=" && peek() == "=")
            {
                avance();
                avance();
                return new TOKENS("IGUAL", "==");
            }

            if (current_char == ";")
            {
                avance();
                return new TOKENS("SEMI", ";");

            }
            if (current_char == ".")
            {
                avance();
                return new TOKENS("DOT", ".");
            }

            if (char.IsDigit(current_char[0]))
            {
                return new TOKENS("ENTERO",numero());
            }
            if (current_char == "+")
            {
                avance();
                return new TOKENS("PLUS", "+");
            }
            if (current_char == "-")
            {
                avance();
                return new TOKENS("MINUS", "-");
            }
            if (current_char == "*")
            {
                avance();
                return new TOKENS("MULT", "*");
            }
            if (current_char == "/")
            {
                avance();
                return new TOKENS("DIV", "/");
            }
            if (current_char == "(")
            {
                avance();
                return new TOKENS("LPAREN", "(");
            }
            if (current_char == ")")
            {
                avance();
                return new TOKENS("RPAREN", ")");
            }
            if (current_char == "&")
            {
                avance();
                return new TOKENS("AND", "&");
            }
            if (current_char == "|" && peek()=="|")
            {
                avance();
                avance();
                return new TOKENS("OR", "||");
            }
            if (current_char == ">")
            {
                avance();
                return new TOKENS("MAYOR", ">");
            }
            if (current_char == "<")
            {
                avance();
                return new TOKENS("MENOR", "<");
            }
            if (current_char == "{")
            {
                avance();
                return new TOKENS("BEGIN", "{");
            }
            if (current_char == "}")
            {
                avance();
                return new TOKENS("END", "}");
            }
        }
        return new TOKENS("EOF", " ");
    }

    

}

