using System.Collections.Generic;
using System.IO;
namespace TournamentLib
{
    public class TournamentRepo
    {
        public Tournament GetTournament(string name)
        {
            StreamReader txtReader = new StreamReader("Gem turneringer.txt");
            string line;

            while ((line = txtReader.ReadLine()) != null) 
            {
                if (line.ToLower() == name.ToLower())
                {
                    Tournament tournament = new Tournament(name);
                    txtReader.Close();
                    return tournament;
                }
            }
            txtReader.Close();
            return null;
        }
    }
}
    
