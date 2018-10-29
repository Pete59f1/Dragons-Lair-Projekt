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
            int[] scores = new int[t.GetTeams().ToArray().Length];

            for(int i = 0; i < rounds; i++)
            {
                currentRound = t.GetRound(i);
                winningTeams = currentRound.GetWinningTeams();

                for(int team = 0; team < t.GetTeams().ToArray().Length; team++)
                {
                    for(int winningTeam = 0; winningTeam < winningTeams.Count; winningTeam++)
                    {
                        if(t.GetTeams().ToArray()[team].Name == winningTeams[winningTeam].Name)
                        {
                            scores[team]++;
                        }
                    }
                }
            }

            for(int i = scores.Max(); i >= 0; i--)
            {
                for(int j = 0; j < t.GetTeams().ToArray().Length; j++)
                {
                    if(scores[j] == i)
                    {
                        Console.WriteLine("Team: " + t.GetTeams().ToArray()[j] + " - Score: " + scores[j]);
                    }
                }
            }
            Console.WriteLine("\n");
        }

        public TournamentRepo GetTournamentRepository()
        {
            return tournamentRepository;
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
