using System.Collections.Generic;
using System.IO;
namespace TournamentLib
{
    public class TournamentRepo
    {
        //private Tournament winterTournament = new Tournament("Vinter Turnering");

        public Tournament GetTournament(string name)
        {
            //if (name == "Vinter Turnering")
            //{
            //    return winterTournament;
            //}
            //return null;

            StreamReader txtReader = new StreamReader("Gem turneringer.txt");
            string line = txtReader.ReadLine();

            while (line != null) 
            {
                if (line == name) 
                {
                    Tournament winterTournament = new Tournament(name);
                    return winterTournament;
                }
                txtReader.Close();
            }
            return null;
        }
    }
}
    
