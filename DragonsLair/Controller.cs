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
            t.SetupTestRounds();
            List<Team> winningTeams;
            Team[] teams = t.GetTeams().ToArray();
            int[] scores = new int[teams.Length];

            for(int i = 0; i < t.GetNumberOfRounds(); i++)
            {
                Round currentRound = t.GetRound(i);
                winningTeams = currentRound.GetWinningTeams();

                for(int j = 0; j < teams.Length; j++)
                {
                    for(int winningTeam = 0; winningTeam < winningTeams.Count; winningTeam++)
                    {
                        if(teams[j].Name == winningTeams[winningTeam].Name)
                        {
                            scores[j]++;
                        }
                    }
                }
            }

            for(int i = scores.Max(); i >= 0; i--)
            {
                for(int j = 0; j < teams.Length; j++)
                {
                    if(scores[j] == i)
                    {
                        Console.WriteLine("Team: " + teams[j] + " - Score: " + scores[j]);
                    }
                }
            }
            Console.WriteLine("\n");
        }

        public TournamentRepo GetTournamentRepository()
        {
            return tournamentRepository;
        }

        private void swap(ref List<Team> list, int i, int j) //Mikkels kode
        {
            Team temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        private List<Team> scramble(List<Team> listToScramble) //Mikkels kode
  		{ 
  			Random randome = new Random(); 
  			for (int i = 0; i<listToScramble.Count; i++) 
  			{ 
  				swap(ref listToScramble, i, randome.Next(0, listToScramble.Count-1)); 
  			} 
  			return listToScramble; 
  		}


        public void ScheduleNewRound(string tournamentName, bool printNewMatches = true)
        {
            // Do not implement this method

            Tournament t;
            t = tournamentRepository.GetTournament(tournamentName);
            int numberOfRounds = t.GetNumberOfRounds();
            List<Team> teams = new List<Team>();
            List<Team> scrambled = new List<Team>();
            Round lastRound;
            bool isRoundFinished;
            Team oldFreerider;
            Team newFreerider = null;

            if (numberOfRounds == 0)
            {
                lastRound = null;
                isRoundFinished = true;
            }
            else
            {
                lastRound = t.GetRound(numberOfRounds - 1);
                isRoundFinished = lastRound.IsMatchesFinished();
            }

            if(isRoundFinished == true)
            {
                if(lastRound == null)
                {
                    teams = t.GetTeams().ToList();
                }
                else
                {
                    teams = lastRound.GetWinningTeams();
                    if (lastRound.FreeRider != null)
                    {
                        teams.Add(lastRound.FreeRider);
                    }
                }

                if(teams.Count >= 2)
                {
                    Round newRound = new Round();
                    scrambled = scramble(teams.ToList());

                    if(scrambled.Count % 2 != 0)
                    {
                        if(numberOfRounds > 0)
                        {
                            oldFreerider = lastRound.FreeRider;
                        }
                        else
                        {
                            oldFreerider = null;
                        }

                        while(newFreerider == oldFreerider)
                        {
                            int i = 0;
                            newFreerider = scrambled[i];
                            i++;
                        }
                        newRound.FreeRider = newFreerider;
                        scrambled.Remove(newFreerider);
                    }

                    for (int i = 0; i < scrambled.Count - 1; i += 2)
                    {
                        Match match = new Match();
                        match.FirstOpponent = scrambled[i];
                        match.SecondOpponent = scrambled[i + 1];
                        newRound.AddMatch(match);
                    }
                    t.AddRound(newRound);
                    if(printNewMatches == true)
                    {
                        ShowScore(tournamentName);
                    }
                }
                else
                {
                    throw new Exception ("Tournament Is Finished");
                }
            }
            else
            {
                throw new Exception("Round Not Finished");
            }
        }

        public void SaveMatch(string tournamentName, int roundNumber, string team1, string team2, string winningTeam)
        {
            // Do not implement this method
        }
    }
}
