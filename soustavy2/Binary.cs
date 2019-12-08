using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soustavy2
{
    /*! \brief Ukázka Binární třídy, dále se nepoužívá
     */
    /// <summary>
    /// Ukázka Binární třídy, dále se nepoužívá
    /// </summary>
    class Binary
    {
        string x; //Backing field vlastnosti Output
        /// <summary>
        /// Vlastnost output
        /// </summary>
        public string Output
        {
            get
            {
                return this.x;
            }
        }
        /// <summary>
        /// Kontruktor
        /// </summary>
        /// <param name="x">Ze začátku prázdný string</param>
        public Binary(string x)
        {
            this.x = x;
        }
        /// <summary>
        /// Funkce převádějící desítkovou soustavu do binární
        /// </summary>
        /// <param name="dec">Vstup od uživatele v desítkové soustavě</param>
        /// <returns>Hodnota binární soustavy</returns>
        public string ToBinary(long dec)
        {
            while(dec > 0)
            {
                if (dec % 2 == 1)
                    x = "1" + x;
                else
                    x = "0" + x;
                dec = dec / 2;
            }
            return x;
        }
    }
    /*!
     * \warning This class in not used
     */
}
