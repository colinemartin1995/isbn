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
            string typeIsbn = "10";
      

            // Demander à l'utilisateur de saisir son ISBN10 ou ISBN13
            message = "Veuillez saisir votre ISBN : ";
            Console.WriteLine(message); 

            // Récupération de l'ISBN saisi
            isbnSaisi = Console.ReadLine();
         
            // Suppression des tirets éventuels dans l'ISBN saisi
            isbn = isbnSaisi.Replace("-", "");

            System.Text.RegularExpressions.Regex myRegex = new Regex(@"^([0-9]){9}([0-9]|X|x)$");
            isIsbnValid = myRegex.IsMatch(isbn); // retourne true ou false selon la vérification

            // Si l'ISBN saisi n'est pas du type ISBN10 alors ici on regarde s'il est du type ISBN13 
            if (!isIsbnValid) 
            {
                myRegex = new Regex(@"^(978|979)([0-9]){10}$");
                isIsbnValid = myRegex.IsMatch(isbn); // Retourne true ou false selon la vérification
                typeIsbn = "13";
            }

            if (!isIsbnValid)
            {
                message = "L'ISBN n'est pas correct, veuillez saisir un ISBN valide ex : 2-8769-4033-7 (ISBN10) ou 978-2-86889-006-1 (ISBN13)";
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
                
                // Si l'ISBN saisi est du type ISBN10
                if (typeIsbn == "10")
                {
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
                }                
                else // Sinon on est dans le cas de l'ISBN 13
                {
                    // Parcourt du tableau du premier jusqu'à l'avant dernier élément 
                    for (int i = 0; i < isbnDecompose.Length - 1; i = i+2) //i+2 permet de n'avoir que les chiffres impaires
                    {
                        int number = (int)Char.GetNumericValue(isbnDecompose[i]); // je récupère la valeur numérique que je convertis en int
                        int number2 = (int)Char.GetNumericValue(isbnDecompose[i+1]);

                        somme = somme + (number + 3 * number2);
                    }

                    reste = (10 - (somme % 10)) % 10;
                }

                clefAttendue = Char.Parse(reste.ToString());

                // On met la clé en majuscule pour gérer le cas où on a renseigné une clé "X" en minuscule
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
