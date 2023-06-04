namespace Morpion
{
    internal class Program
    {
        enum EtatCase
        {
            vide,
            rond,
            croix
        }
        static EtatCase[,] grille;//grille de [3,3]
        static Random generator;
        static void Main(string[] args)
        {
            //message d'acceuil
            Console.WriteLine("Welcome to Morpion Game!!");

            //initialisation variable
            bool gameOver = false;
            grille = new EtatCase[3, 3];
            int caseVide = 9;
            generator = new Random();

            //afficher grille
            affichageGrille();

            //boucle principal
            while (!gameOver) 
            {
                //jeu utilisateur
                userMove();
                caseVide--;

                //affichage de la grille
                affichageGrille();
                
                //jeu gagnant?
                bool win = IsWinner(EtatCase.croix);
                if (win)
                {
                    Console.WriteLine("Congratulations, you win!!!!");
                    gameOver = true;
                }

                

                //jeu ordinateur
                if(!gameOver && caseVide > 0)
                {
                    computerMove();
                    caseVide--;

                    //affichage de la grille
                    affichageGrille();

                    //jeu gagnant?
                    bool lost = IsWinner(EtatCase.rond);
                    if (lost)
                    {
                        Console.WriteLine("Sorry, you loose!!!");
                        gameOver = true;
                    }
                    
                }


                //match nul
                if (caseVide == 0)
                {
                    Console.WriteLine(" Il y a eu Match Nul!!");
                    gameOver = true;
                    
                }
            }   

            //fin de jeu
            Console.WriteLine("Appuyer sur une touche pour quitter le jeu....");
            Console.ReadKey();
        }
        /// <summary>
        /// a function for testing a combinaison of equal of column line and diagonal
        /// </summary>
        /// <param name="etat">croix or rond</param>
        /// <returns></returns>
        private static bool IsWinner(EtatCase etat)
        {
            //winning with line
            for(int ligne = 0; ligne < 3; ligne++)
                if (grille[ligne, 0] == etat && grille[ligne, 1] == etat && grille[ligne, 2] == etat) return true;
            //wining with column
            for(int colonne=0;colonne<3;colonne++)
                if (grille[0, colonne] == etat && grille[1,colonne]==etat && grille[2,colonne]==etat) return true;
            //winning with two diagonals
            if (grille[0, 0] == etat && grille[1, 1] == etat && grille[2, 2] == etat) return true;
            if (grille[0, 2] == etat && grille[1, 1] == etat && grille[2, 0] == etat) return true;

            return false;
        }

        /// <summary>
        /// a function makes computer choosing a case by random
        /// </summary>
        private static void computerMove()
        {
            Console.WriteLine("Computer played:");
            bool rightChoice = false;
            while (!rightChoice)
            {
                int ligne = generator.Next(0, 3);
                int colonne = generator.Next(0, 3);
                if (grille[ligne, colonne] == EtatCase.vide)
                {
                    grille[ligne, colonne] = EtatCase.rond;
                    rightChoice = true;
                }
            }
        }

        /// <summary>
        /// a function which makes user move his turn
        /// </summary>
        private static void userMove()
        {
            bool rightChoice = false;
            //test if user choosed the right case
            while (!rightChoice)
            {

                Console.WriteLine("Choose a case:");
                String choice = Console.ReadLine();
                if(int.TryParse(choice, out int numberChoosed) && numberChoosed>=0 && numberChoosed<=8)
                {
                    //take the line and collumn that the users choosed
                    int ligne = numberChoosed / 3;
                    int colonne = numberChoosed % 3;
                    if (grille[ligne,colonne]== EtatCase.vide)
                    {
                        grille[ligne, colonne] = EtatCase.croix;
                        rightChoice = true;
                    }
                    
                }
            }
        }

        /// <summary>
        /// affiche grille
        /// </summary>
        private static void affichageGrille()
        {
            String dessinGrille = "";
            //trait du haut
            dessinGrille += "*******\n";

            for(int ligne=0; ligne < 3; ligne++)
            {
                dessinGrille += "|";
                for(int colonne = 0; colonne < 3; colonne++)
                {
                    switch (grille[ligne, colonne])
                    {
                        case EtatCase.vide:
                            dessinGrille += ligne * 3 + colonne;
                            break;
                        case EtatCase.rond:
                            dessinGrille += "O";
                            break;
                        case EtatCase.croix:
                            dessinGrille += "x";
                            break;
                    }
                    dessinGrille += "|";
                }
                dessinGrille += "\n*******\n";
            }
            Console.WriteLine(dessinGrille);
        }
    }
}