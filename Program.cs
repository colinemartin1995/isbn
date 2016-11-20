using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISBN10
{
    class Program
    {

 
        static void Main(string[] args)
        {

            String message;
            String isbn;
            String reponse;
            String blocExtrait;
           // int isbnNomnbre;


            message = "Veuillez saisir votre ISBN : ";
            Console.WriteLine(message);
            isbn = Console.ReadLine();
            reponse = "Votre ISBN est : " + isbn;

         
            blocExtrait = isbn.Replace("-", "");
            Console.WriteLine(blocExtrait);



            if(blocExtrait.Length == 10)
            {
               
                String messagevalide;
                messagevalide = "Votre ISBN est valide";
                Console.WriteLine(messagevalide);

                //isbnNomnbre = int.Parse(blocExtrait);

                char[] array = blocExtrait.ToCharArray();
                for (int i = 0; i < array.Length; i++)
                {                  
                    char number = array[i];
                    Console.WriteLine(number);
                }




            }

        

            else
             {
                    Console.WriteLine(message);

             }


        
       

            //Char delimiter = '-';
            //String[] substrings = reponse.Split(delimiter);
            //foreach (var blocExtrait in substrings)
            //Console.WriteLine(blocExtrait);




        }
    }
}
