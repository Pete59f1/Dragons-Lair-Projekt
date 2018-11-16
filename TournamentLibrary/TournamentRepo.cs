﻿using System.Collections.Generic;
using System.IO;
namespace TournamentLib
{
    public class TournamentRepo
    {
        private Tournament winterTournament = new Tournament("Vinter Turnering");

        public Tournament GetTournament(string name)
        {
            StreamReader txtReader = new StreamReader("Gem turneringer.txt");
            string line;

            while ((line = txtReader.ReadLine()) != null) 
            {
                if (line.ToLower() == name.ToLower())
                {
                    if (line == "vinter turnering")
                    {
                        txtReader.Close();
                        return winterTournament;
                    }
                    else
                    {
                        Tournament tournament = new Tournament(name);
                        txtReader.Close();
                        return tournament;
                    }
                }
            }
            txtReader.Close();
            return null;
        }
    }
}
    
