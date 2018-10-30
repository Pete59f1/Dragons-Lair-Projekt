﻿using System;
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

        public void ScheduleNewRound(string tournamentName, bool printNewMatches = true)
        {
            // Do not implement this method

            Tournament t;
            t = tournamentRepository.GetTournament(tournamentName);
            int numberOfRounds = t.GetNumberOfRounds();
            List<Team> teams = new List<Team>();
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
                    teams = t.GetTeams();
                }
                else
                {
                    teams = lastRound.GetWinningTeams();
                }

                if (lastRound.FreeRider != null)
                {
                    teams.Add(lastRound.FreeRider);
                }

                if(teams.Count >= 2)
                {
                    Round newRound = new Round();

                    if(teams.Count % 2 != 0)
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
                            newFreerider = teams[i];
                            i++;
                        }

                        newRound.FreeRider = newFreerider;
                    }
                    for (int i = 0, j = i + 1; i < teams.Count; i++, j++)
                    {
                        Match match = new Match();
                        match.FirstOpponent = teams[i];
                        match.SecondOpponent = teams[j];
                        newRound.AddMatch(match);
                    }
                    t.AddRound(newRound);
                }
                else
                {
                    throw new Exception ("Turnering er slut");
                }
            }
            else
            {
                throw new Exception("Runde er ikke slut");
            }
        }

        public void SaveMatch(string tournamentName, int roundNumber, string team1, string team2, string winningTeam)
        {
            // Do not implement this method
        }
    }
}
