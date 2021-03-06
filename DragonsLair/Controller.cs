﻿using System;
using System.Collections.Generic;
using System.IO;
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
                    //scrambled = scramble(teams.ToList());
                    scrambled = teams.ToList();

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

        public void SaveMatch(string tournamentName, int roundNo, string winningTeamName)
        {
            // Do not implement this method
            Tournament t = tournamentRepository.GetTournament(tournamentName);
            Round r = t.GetRound(roundNo);
            Match m = r.GetMatch(winningTeamName);
            Team w = null;

            if (m != null && m.Winner == null)
            {
                w = t.GetTeam(winningTeamName);
                Console.WriteLine("Succes: Kampen mellem " + m.FirstOpponent + " og " + m.SecondOpponent + " i runde " + roundNo + " i turnering " + tournamentName + " er nu afviklet. Vinderen blev " + winningTeamName);
                m.Winner = w;
            }
            else
            {
                Console.WriteLine("Fejl: Holdet " + winningTeamName + " kan ikke være vinder i runde " + roundNo + ", da holdet enten ikke deltager i runde " + roundNo + " eller kampen allerede er registreret med en vinder");
            }
        }
        public void SaveTournament(string tournamentName)
        {
            StreamWriter txtWriter = new StreamWriter("Gem turneringer.txt", true);
            txtWriter.WriteLine(tournamentName);
            txtWriter.Close();
        }

        public void SaveTeam(string teamName)
        {
            StreamWriter txtWriter = new StreamWriter("Gem hold.txt", true);
            txtWriter.WriteLine(teamName);
            txtWriter.Close();
        }

        public void changeTournamentName(string tournamentName, string newTournamentName)
        {
            string[] lines = File.ReadAllLines("Gem turneringer.txt");
            bool found = false;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].ToLower() == tournamentName.ToLower())
                {
                    lines[i] = newTournamentName;
                    found = true;

                    StreamWriter txtWriter = new StreamWriter("Gem turneringer.txt");
                    foreach (string line in lines)
                    {
                        txtWriter.WriteLine(line);
                    }
                    txtWriter.Close();
                }
            }
            if (found == false)
            {
                throw new Exception("Kunne ikke finde turnering");
            }
        }

        public void changeTeamName(string teamName, string newTeamName)
        {
            string[] lines = File.ReadAllLines("Gem hold.txt");
            bool found = false;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].ToLower() == teamName.ToLower())
                {
                    lines[i] = newTeamName;
                    found = true;

                    StreamWriter txtWriter = new StreamWriter("Gem hold.txt");
                    foreach (string line in lines)
                    {
                        txtWriter.WriteLine(line);
                    }
                    txtWriter.Close();
                }
            }
            if (found == false)
            {
                throw new Exception("Kunne ikke finde hold");
            }
        }
    }
}
