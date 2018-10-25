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
            Tournament t;
            t = tournamentRepository.GetTournament(tournamentName);
            Round currentRound;
            List<Team> winningTeams = new List<Team>();
            int rounds = t.GetNumberOfRounds();

            for(int i = 0; i < rounds; i++)
            {
                currentRound = t.GetRound(i);
                winningTeams.Add(currentRound.GetWinningTeams()[i]);
            }
            
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
