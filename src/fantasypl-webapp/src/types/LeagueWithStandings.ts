import LeagueInfo from "./LeagueInfo";
import Standing from "./Standing";

export default interface LeagueWithStandings {
  league: LeagueInfo;
  standing: Standing[];
}
