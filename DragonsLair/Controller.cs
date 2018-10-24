using System;
using System.Collections.Generic;
using System.Linq;
using TournamentLib;

namespace DragonsLair
{
    public class Controller
    {
        private TournamentRepo tournamentRepository = new TournamentRepo();

        public void ShowScore(string tournamentName)
        {
            /*
             * TODO: Calculate for each team how many times they have won
             * Sort based on number of matches won (descending)
             */
            //Console.WriteLine("Implement this method!");

            Tournament t;
            t = tournamentRepository.GetTournament(tournamentName);
            Round currentRound;
            List<Team> winningTeams = new List<Team>();
            int rounds = t.GetNumberOfRounds();

            for(int i = 0; i < rounds; i++)
            {
                currentRound = t.GetRound(i);
                winningTeams.Add(currentRound.GetWinningTeams());
            }

            //int j = 1;
            /*for(int i = 0; i < winningTeams.Count; i++)
            {
                if(winningTeams[i] < winningTeams[j])
                {
                    Team holder = winningTeams[i];
                    winningTeams[i] = winningTeams[j];
                    winningTeams[j] = holder;
                }
                j++;
            }*/

            foreach(Team i in winningTeams)
            {
                Console.WriteLine(i);
            }
        }

        public void ScheduleNewRound(string tournamentName, bool printNewMatches = true)
        {
            // Do not implement this method
        }

        public void SaveMatch(string tournamentName, int roundNumber, string team1, string team2, string winningTeam)
        {
            // Do not implement this method
        }
    }
}
