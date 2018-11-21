using System;

namespace DragonsLair
{
    public class Menu
    {
        private Controller control = new Controller();
        
        public void Show()
        {
            bool running = true;
            do
            {
                ShowMenu();
                string choice = GetUserChoice();
                switch (choice)
                {
                    case "0":
                        running = false;
                        break;
                    case "1":
                        ShowScore();
                        break;
                    case "2":
                        ScheduleNewRound();
                        break;
                    case "3":
                        SaveMatch();
                        break;
                    case "4":
                        SaveTournament();
                        break;
                    case "5":
                        SaveTeam();
                        break;
                    case "6":
                        changeTournamentName();
                        break;
                    case "7":
                        changeTeamName();
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg.");
                        Console.ReadLine();
                        break;
                }
            } while (running);
        }

        private void ShowMenu()
        {
            Console.WriteLine("Dragons Lair");
            Console.WriteLine();
            Console.WriteLine("1. Præsenter turneringsstilling");
            Console.WriteLine("2. Planlæg runde i turnering");
            Console.WriteLine("3. Registrér afviklet kamp");
            Console.WriteLine("4. Opret ny turnering");
            Console.WriteLine("5. Opret nyt hold");
            Console.WriteLine("6. Angiv nyt navn til turnering");
            Console.WriteLine("7. Angiv nyt navn til hold");
            Console.WriteLine("");
            Console.WriteLine("0. Exit");
        }

        private string GetUserChoice()
        {
            Console.WriteLine();
            Console.Write("Indtast dit valg: ");
            return Console.ReadLine();
        }
        
        private void ShowScore()
        {
            Console.Write("Angiv navn på turnering: ");
            string tournamentName = Console.ReadLine();
            Console.Clear();
            control.ShowScore(tournamentName);
        }

        private void ScheduleNewRound()
        {
            Console.Write("Angiv navn på turnering: ");
            string tournamentName = Console.ReadLine();
            Console.Clear();
            control.ScheduleNewRound(tournamentName);
        }

        private void SaveMatch()
        {
            Console.Write("Angiv navn på turnering: ");
            string tournamentName = Console.ReadLine();
            Console.Write("Angiv runde: ");
            int round = int.Parse(Console.ReadLine());
            Console.Write("Angiv vinderhold: ");
            string winner = Console.ReadLine();
            Console.Clear();
            control.SaveMatch(tournamentName, round, winner);
        }
        private void SaveTournament()
        {
            Console.Write("Angiv navn til ny turnering: ");
            string tournamentName = Console.ReadLine();
            Console.Clear();
            control.SaveTournament(tournamentName);
        }

        private void SaveTeam()
        {
            Console.WriteLine("Angiv navn til nye hold: ");
            string teamName = Console.ReadLine();
            Console.Clear();
            control.SaveTeam(teamName);
        }

        private void changeTournamentName()
        {
            Console.WriteLine("Angiv navn på den turnering du vil have ændret: ");
            string tournamentName = Console.ReadLine();
            Console.WriteLine("Hvad skal turneringen hedde nu?: ");
            string newTournamentName = Console.ReadLine();
            Console.Clear();
            control.changeTournamentName(tournamentName, newTournamentName);
        }

        private void changeTeamName()
        {
            Console.WriteLine("Angiv navn på det hold du vil have ændret: ");
            string teamName = Console.ReadLine();
            Console.WriteLine("Hvad skal holdet hedde nu?: ");
            string newTeamName = Console.ReadLine();
            Console.Clear();
            control.changeTeamName(teamName, newTeamName);
        }
    }
}