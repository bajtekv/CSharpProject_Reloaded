using System;
using System.Drawing;
using Console = Colorful.Console;

namespace soustavy2
{
    /*!
     * \brief Třída soustavy
     * \section ToSystem
     * Tato třída obsahuje funkce pro převod soustav
     */
    class ToSystem
    {
        string x;
        public string Output //! Veřejná vlastnost třídy ToSystem, ze které lze číst požadovanou hodnotu z funkce
        {
            get
            {
                return this.x;
            }
        }
        /// <summary>
        /// Kontruktor
        /// </summary>
        /// <param name="x"></param>
        public ToSystem(string x)
        {
            this.x = x;
        }
        /// <summary>
        /// Funkce konvertující soustavy
        /// </summary>
        /// <param name="dec">Desítková soustava</param>
        /// <param name="system">Cílová soustava</param>
        /// <returns>Cílová soustava</returns>
        public string Convert(long dec, int system)
        {
            long remain = 0;
            if (system == 16)
            {
                while (dec > 0)
                {
                    remain = dec % system;
                    if (remain > 9)
                    {
                        switch (remain)
                        {
                            case 10:
                                this.x = "A" + " " + this.x;
                                break;
                            case 11:
                                this.x = "B" + " " + this.x;
                                break;
                            case 12:
                                this.x = "C" + " " + this.x;
                                break;
                            case 13:
                                this.x = "D" + " " + this.x;
                                break;
                            case 14:
                                this.x = "E" + " " + this.x;
                                break;
                            case 15:
                                this.x = "F" + " " + this.x;
                                break;

                            default:
                                break;
                        }
                    }
                    else
                        this.x = remain.ToString() + " " + this.x;
                    dec = dec / system;
                }
            }
            else
            {
                while (dec > 0)
                {
                    remain = dec % system;
                    this.x = remain.ToString() + " " + this.x;
                    dec = dec / system;
                }
            }
            
            return this.x;
        }
    }
}
