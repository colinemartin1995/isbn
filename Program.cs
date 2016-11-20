using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ISBN10
{
    class Program
    {

 
        static void Main(string[] args)
        {

            String message;
            String isbn;
            String isbnSaisi;
            bool isIsbnValid;
            String reponse;
            String blocExtrait;
           // int isbnNomnbre;

            // Demander à l'utilisateur de saisir son ISBN10 ou ISBN13
            message = "Veuillez saisir votre ISBN : ";
            Console.WriteLine(message); 

            // Récupération de l'ISBN saisi
            isbnSaisi = Console.ReadLine();
         
            // Suppression des tirets éventuels dans l'ISBN saisi
            isbn = isbnSaisi.Replace("-", "");

            System.Text.RegularExpressions.Regex myRegex = new Regex(@"([0-9]){9}([0-9]|X|x)$");
            isIsbnValid = myRegex.IsMatch(isbn); // retourne true ou false selon la vérification

            if (!isIsbnValid)
            {
                message = "L'ISBN n'est pas correct, veuillez saisir un ISBN valide ex : 2-8769-4033-7";
                Console.WriteLine(message);
                Program.Main(null); // Redemander la saisie de l'ISBN
            }
            else
            {
                int j = 1; // Initialisation du compteur pour la pondération
                int somme = 0; // Initialisation de la somme pour le calcul de la clef
                int reste;
                char clefAttendue;
                char clefSaisie;

                char[] isbnDecompose = isbn.ToCharArray(); // Création du tableau dans lequel on range chaque caractère de l'ISBN saisi

                // Parcourt du tableau du premier jusqu'à l'avant dernier élément 
                for (int i = 0; i < isbnDecompose.Length - 1; i++)
                {
                    int number = (int)Char.GetNumericValue(isbnDecompose[i]);

                    somme = somme + (number * j);
                    j++; // ne pas oublier d'incrémenter le compteur pour la pondération
                }

                reste = (somme % 11);

                if (reste == 10) 
                {
                    clefAttendue = 'X';
                }
                else
                {
                    clefAttendue = Char.Parse(reste.ToString());
                }

                clefSaisie = Char.ToUpper(isbnDecompose[isbnDecompose.Length - 1]);

                if (clefAttendue == clefSaisie)
                {
                    message = "Votre ISBN est valide !";
                    Console.WriteLine(message);
                }
                else
                {
                    message = "Votre ISBN n'est pas valide : clef saisie = " + isbnDecompose[isbnDecompose.Length - 1]
                             + ", clef attendue = " + clefAttendue;
                    Console.WriteLine(message);
                }
            }

            Program.Main(null); // Relance le programme et vérifier un nouveau ISBN
        }

    }
}
