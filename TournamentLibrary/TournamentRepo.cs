using System.Collections.Generic;
using System.IO;
namespace TournamentLib
{
    public class TournamentRepo
    {
        private Tournament winterTournament = new Tournament("Vinter Turnering");

        public Tournament GetTournament(string name)
        {
            StreamReader txtReader = new StreamReader("Gem turneringer.txt");
            string line = txtReader.ReadLine();

            while (!txtReader.EndOfStream) 
            {
                if (line.ToLower() == name.ToLower())
                {
                    if (line == "vinter turnering")
                    {
                        return winterTournament;
                    }
                    else
                    {
                        Tournament tournament = new Tournament(name);
                        return tournament;
                    }
                }
            }
            txtReader.Close();
            return null;
        }
    }
}
    
