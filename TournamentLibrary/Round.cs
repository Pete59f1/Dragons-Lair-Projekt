using System.Collections.Generic;

namespace TournamentLib
{
    public class Round
    {
        private List<Match> matches = new List<Match>();
        
        public void AddMatch(Match m)
        {
            matches.Add(m);
        }

        public Match GetMatch(string teamName1, string teamName2)
        {
            Match mat = new Match();
            foreach(Match m in matches)
            {
                if (m.FirstOpponent.ToString() == teamName1 && m.SecondOpponent.ToString() == teamName2)
                {
                    mat = m;
                }
            }
            return mat;
        }

        public bool IsMatchesFinished()
        {
            // TODO: Implement this method
            for(int i = 0; i < matches.Count; i++)
            {
                if(matches[i].Winner == null)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Team> GetWinningTeams()
        {
            // TODO: Implement this method
            List<Team> winners = new List<Team>();

            for(int i = 0; i < matches.Count; i++)
            {
                winners.Add(matches[i].Winner);
            }
            return winners;
        }

        public List<Team> GetLosingTeams()
        {
            // TODO: Implement this method
            List<Team> losers = new List<Team>();
            List<Team> winners = new List<Team>();

            foreach(Match m in matches)
            {
                if(m.Winner == m.FirstOpponent)
                {
                    winners.Add(m.FirstOpponent);
                    losers.Add(m.SecondOpponent);
                }
                else
                {
                    winners.Add(m.SecondOpponent);
                    losers.Add(m.FirstOpponent);
                }
            }
            return losers;
        }
    }
}
