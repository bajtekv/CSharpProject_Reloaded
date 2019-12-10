/*!
 * \mainpage Převaděč soustav
 * \section intro_sec Úvod
 *
 * Program slouží k převodu z desítkové soustavy do Binářní, Osmičkové a Hexadecimální.
 *
 * \subsection motiv Jak program funguje?
 *
 * Z uživatelského hlediska:
 *  - spustíme EXE soubor
 *  - zadáme vstup v desítkové soustavě
 *  - vybere soustavu, do které budeme vstup převádět
 *  - Následně se objeví výstup a nabídka, zda chceme převádět znovu (y), ukončit program (n) nebo provádět testování (t)
 *
 * Z programátorského hlediska
 *  - pomocí knihovny Colorful console se vykreslují barvy
 *  - Je definovaná třída ToSystem, která je schopna převést desítkovou soustavy do kterékoliv jiné
 *  - Funkce typeText() vypisuje uživateli text s "Typewriter efektem", Funkce prompt() zobrazuje kurzor
 *  - Vstup od uživatele se zkontroluje a uloží do proměnné
 *  - Dle systémové funkce switch se zobrazí požadovaná hodnota
 *  - Podle přečtené klávesy se program vrací na návěští nebo se ukončí (y/n)
 *  - Při stisknutí (t) se spustí testování programu
 *  - Jsou definované 3 funkce, které vrací požadovanou hodnotu a je tak možné srovnat hodnotu s přechozím výsledkem
 *  
 * \author Václav Bajtek, Martin Šmotek, Jiří Šrytr
 * \version 1.1
 * \date 2019-12-5
 */

using System;
//using System.Collections.Generic; //makes some bugs in colors
using System.Drawing;
using Console = Colorful.Console;
using System.Threading;

namespace soustavy2
{
    /*!
     * \brief Hlavní třída Program
     */
    class Program
    {
        static void Main(string[] args)
        {
            typeText("Welcome to System Converter", 20, Color.White);
            start:
            Colorful.Console.ReplaceAllColorsWithDefaults();
            long userInputDecimal = 0;  //!< Vstup od uživatele v desítkové soustavě
            bool userInputBool = false; //!< Kontrolní proměnná vstupu
            int option = 0;             //!< Výběr soustavy
            int userOptionTests = 0;    //!< Výběr funkce
            typeText("Enter a number in decimal numeral system", 8, Color.White);
            userInputDecimal = parseUserInput();
            Console.Clear();
            
            userInputBool = false;
            do //parsing menu option
            {
                displayMenu();
                prompt(">> ");
                userInputBool = int.TryParse(Console.ReadLine(), out option);
                if (userInputBool == false || option > 3)
                {
                    Console.WriteLine("Invalid input.", Color.Red);
                }
                else
                {
                    Console.WriteLine("Saved!", Color.Green);
                    Thread.Sleep(1000);
                }
                switch (option)
                {
                    case 1: //konvertování do binární soustavy
                        ToSystem binary = new ToSystem("");
                        binary.Convert(userInputDecimal, 2);
                        Console.WriteLine($"Input: {userInputDecimal}", Color.Orange);
                        Console.Write("Output: ");
                        typeText(binary.Output, 80, Color.Cyan);
                        break;
                    case 2: //konvertování do osmičkové soustavy
                        ToSystem octa = new ToSystem("");
                        octa.Convert(userInputDecimal, 8);
                        Console.WriteLine($"Input: {userInputDecimal}", Color.Orange);
                        Console.Write("Output: ");
                        typeText(octa.Output, 100, Color.Cyan);
                        break;
                    case 3: //konvertování do hexa soustavy
                        ToSystem hexa = new ToSystem("");
                        hexa.Convert(userInputDecimal, 16);
                        Console.WriteLine($"Input: {userInputDecimal}", Color.Orange);
                        Console.Write("Output: ");
                        typeText(hexa.Output, 150, Color.Cyan);
                        break;
                    default: //vrací se na začátek
                        Thread.Sleep(1500);
                        userInputBool = false;
                        Console.Clear();
                        break;
                }
            } while (userInputBool == false);
            Console.WriteLine();
            yesOrNo:
            Console.WriteLine("Continue ? press [y/n] or press [t] for unit tests",Color.LightPink);
            ConsoleKeyInfo info = Console.ReadKey(); //!< načtení stisk. klávesy
            if (info.Key == ConsoleKey.Y)
            {
                Console.Clear();
                Thread.Sleep(500);
                goto start; //vrací zpět na začátek
            }
            if (info.Key == ConsoleKey.N)
                System.Environment.Exit(0);
            if (info.Key == ConsoleKey.T)
            {
                Console.Clear();
                Colorful.Console.ReplaceAllColorsWithDefaults(); //resetuje barvy
                Console.WriteAscii("UNIT TESTS", Color.FromArgb(63,224,208));
                typeText("UNIT TESTER", 30, Color.LightSteelBlue);
                Console.WriteLine("Functions: ");
                String[] optionsTest = new String[3] { "1) [convertBin()]", "2) [convertToOct()]", "3) [convertToHex()]" };
                for (int i = 0; i < optionsTest.Length; i++) //výpis nabídky funkcí
                {
                    Console.WriteLine(optionsTest[i], Color.LightPink);
                }
                do //ošetřené parsování volby
                {
                    prompt("[test]$ ");
                    userInputBool = int.TryParse(Console.ReadLine(), out userOptionTests);
                    if (userInputBool == false || userOptionTests > 3)
                    {
                        Console.WriteLine("Invalid input.", Color.Red);
                        userInputBool = false;
                    }
                    else
                        Console.WriteLine("OK",Color.Green);
                } 
                while (userInputBool == false);
                Console.WriteLine();
                Console.WriteLine("Enter decimal value:", Color.LightPink);
                prompt("[test]$ ");
                userInputDecimal = parseUserInput();
                Console.WriteLine("----------------------------", Color.White); //výpis volby
                Console.WriteLine("SUMMARY: ", Color.LightPink);
                Console.WriteLine($"input: {userInputDecimal}", Color.White);
                Console.WriteLine($"using function: {optionsTest[userOptionTests-1]}", Color.White); //[userOptionTests-1] needs -1 because of the array
                Console.WriteLine("----------------------------", Color.White);
                switch (userOptionTests) //volání požadovaného unit testu
                {
                    case 1:
                        Console.WriteLine($"Output: {convertToBin(userInputDecimal)}",Color.Yellow);
                        break;
                    case 2:
                        Console.WriteLine($"Output: {convertToOct(userInputDecimal)}",Color.Yellow);
                        break;
                    case 3:
                        Console.WriteLine($"Output: {convertToHex(userInputDecimal)}",Color.Yellow);
                        break;
                    default:
                        break;
                }
                goto yesOrNo;
            }
            else
            {
                Console.WriteLine("Invalid input.", Color.Red);
                goto yesOrNo;
            }
        }
        //functions:
        /// <summary>
        /// Toto funkce vypisuje text "Typewriter effektem"
        /// </summary>
        /// <param name="x">Řetězec, který má být vypsán</param>
        /// <param name="y">Rychlost výpisu</param>
        /// <param name="c">Barva výpisu</param>
        static public void typeText(string x, int y, Color c = new Color())
        {

            for (int i = 0; i < x.Length; i++)
            {
                Console.Write(x[i], c);
                Thread.Sleep(y);
            }
            Console.WriteLine();
        }
        /// <summary>
        /// Kurzor uživatele
        /// </summary>
        /// <param name="prompter">Forma kurzoru</param>
        static public void prompt(string prompter)
        {
            Console.Write(prompter, Color.Orange);
        }
        /// <summary>
        /// Funkce pro vstup desítkový vstup od uživatele
        /// </summary>
        /// <returns>Vstup od uživatele</returns>
        static public long parseUserInput()
        {
            long userInputDecimal = 0;
            bool userInputBool = false;
            do //parsing input in decimal
            {
                prompt(">> ");
                userInputBool = long.TryParse(Console.ReadLine(), out userInputDecimal);
                if (userInputBool == false || userInputDecimal < 0 || userInputDecimal > 9223372036854775807)
                {
                    Console.WriteLine("Invalid input.", Color.Red);
                    userInputBool = false;
                }

                else
                {
                    Console.WriteLine("Saved!", Color.Green);
                    Thread.Sleep(1000);
                }
            } while (userInputBool == false);
            return userInputDecimal;
        }
        /// <summary>
        /// Zobrazuje menu, uživatel vybírá soustavu, do které se bude převádět
        /// </summary>
        static public void displayMenu()
        {
            int r = 100;
            int g = 210;
            int b = 255;
            String[] options = new String[4] { "Choose your option: ", "1) To Binary", "2) To Octa", "3) To Hexadecimal" };
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine(options[i], Color.FromArgb(r, g, b));
                g -= 25;
                b -= 9;
                Thread.Sleep(30);
            }
        }
        /// <summary>
        /// Unit test funkce - převádí vstup v desítkové soustavě do binární soustavy
        /// </summary>
        /// <param name="dec">Hodnota uživatele v desítkové soustavě</param>
        /// <returns>Hodnota v binární soustavě</returns>
        static public string convertToBin(long dec)
        {
            long remain = 0;
            string Out = "";
            while (dec > 0)
            {
                remain = dec % 2;
                Console.WriteLine($"{dec} / 2 = {dec / 2} | {remain}");
                Out = remain.ToString() + " " + Out;
                dec = dec / 2;
            }
            return Out;
        }
        /// <summary>
        /// Unit test funkce - převádí vstup v desítkové soustavě do osmičkové soustavy
        /// </summary>
        /// <param name="dec">Hodnota uživatele v desítkové soustavě</param>
        /// <returns>Hodnota v osmičkové soustavě</returns>
        static public string convertToOct(long dec)
        {
            long remain = 0;
            string Out = "";
            while (dec > 0)
            {
                remain = dec % 8;
                Console.WriteLine($"{dec} / 8 = {dec / 8} | {remain}");
                Out = remain.ToString() + " " + Out;
                dec = dec / 8;
            }
            return Out;
        }
        /// <summary>
        /// Unit test funkce - převádí vstup v desítkové soustavě do hexadecimální soustavy
        /// </summary>
        /// <param name="dec">Hodnota uživatele v desítkové soustavě</param>
        /// <returns>Hondota v hexadecimální soustavě</returns>
        static public string convertToHex(long dec)
        {
            long remain = 0;
            string Out = "";
            while (dec > 0)
            {
                remain = dec % 16;
                Console.WriteLine($"{dec} / 16 = {dec / 16} | {remain}");
                if (remain > 9)
                {
                    switch (remain)
                    {
                        case 10:
                            Out = "A" + " " + Out;
                            break;
                        case 11:
                            Out = "B" + " " + Out;
                            break;
                        case 12:
                            Out = "C" + " " + Out;
                            break;
                        case 13:
                            Out = "D" + " " + Out;
                            break;
                        case 14:
                            Out = "E" + " " + Out;
                            break;
                        case 15:
                            Out = "F" + " " + Out;
                            break;

                        default:
                            break;
                    }
                }
                else
                    Out = remain.ToString() + " " + Out;
                dec = dec / 16;
            }
            return Out;
        }
    }
}
